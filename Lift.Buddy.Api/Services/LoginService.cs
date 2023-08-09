using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.DB;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lift.Buddy.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly DBContext _context;

        public LoginService(
            DBContext context    
        )
        {
            _context = context;
        }

        public async Task<Response> GetSecurityQuestions(string username)
        {
            var response = new Response();
            var securityQuestions = new SecurityQuestions();
            try
            {
                var user = (await _context.Users.Where(x => x.UserName == username).ToListAsync()).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("User doesnt exist");
                }
                securityQuestions.Answers = user.Answers.Split(",").ToList();
                securityQuestions.Questions = user.Questions.Split(",").ToList();
                response.result = true;
                response.body = JsonSerializer.Serialize(securityQuestions);
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = ex.Message;
            }
            return response;
        }

        public bool CheckCredentials(LoginCredentials credentials)
        {
            var username = credentials.Username;
            var password = credentials.Password;
            var user = _context.Users.Where(x => x.UserName == username).ToList().FirstOrDefault();
            if ( user == null || password == null)
            {
                return false;
            }
            var hashedPwd = Utils.HashString(password);
            if ( hashedPwd != user.Password ) 
            {
                return false;
            }

            return true;
        }

        public async Task<Response> RegisterUser(RegistrationCredentials credentials)
        {
            var response = new Response();

            try
            {
                var user = new User();
                user.UserName = credentials.Username;
                user.Name = credentials.Name;
                user.Surname = credentials.Surname;
                user.Email = credentials.Email;
                user.Password = Utils.HashString(credentials.Password);
                user.Questions = credentials.Questions ?? "";
                user.Answers = credentials.Answers ?? "";
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                response.result = true;
            }
            catch (Exception ex) 
            {
                response.result = false;
                response.notes = ex.Message;
            }

            return response;
        }

        public async Task<Response> ChangePassword(LoginCredentials loginCredentials)
        {
            var response = new Response();
            try
            {
                var user = (await _context.Users.Where(x => x.UserName == loginCredentials.Username).ToListAsync()).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception("The user doens't exist in the database");
                }
                if (loginCredentials.Password == null)
                {
                    throw new Exception("Trying to change password to null");
                }
                user.Password = Utils.HashString(loginCredentials.Password);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                response.result = true;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.notes = Utils.ErrorMessage(nameof(ChangePassword), ex);
            }

            return response;
        }

    }
}
