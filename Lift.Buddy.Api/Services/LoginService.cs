using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.DB;
using Lift.Buddy.Core.DB.Models;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lift.Buddy.API.Services
{
    public class LoginService : ILoginService
    {
        // Repository al posto di DBContext
        private readonly DBContext _context;

        public LoginService(DBContext context)
        {
            _context = context;
        }

        public async Task<Response<SecurityQuestions>> GetSecurityQuestions(string username)
        {
            var response = new Response<SecurityQuestions>();
            var securityQuestions = new List<SecurityQuestions>();
            try
            {
                var user = (await _context.Users.Where(x => x.UserName == username).ToListAsync())
                    .FirstOrDefault();

                if (user == null) throw new Exception("User doesn't exist");

                var securityQuestion = new SecurityQuestions
                {
                    Answers = user.Answers.Split(",").ToList(),
                    Questions = user.Questions.Split(",").ToList()
                };
                securityQuestions.Add(securityQuestion);
                response.Result = true;
                response.Body = securityQuestions;
            }
            catch (Exception ex)
            {
                // QUESTION: perchè ritornare all'utente il nome di funzione e metodo? 
                // dovrebbe essere roba solo visibile nei log del backend
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetSecurityQuestions), ex);
            }

            // non usato
            var t = JsonSerializer.Serialize(response);
            return response;
        }

        public bool CheckCredentials(LoginCredentials credentials)
        {
            var username = credentials.Username;
            var password = credentials.Password;
            // QUESTION: non dovrebbe essere anche qua async?
            var user = _context.Users.Where(x => x.UserName == username).ToList().FirstOrDefault();

            if (user == null || password == null) // metodo in LoginCredentials, o evitare null
            {
                return false;
            }

            var hashedPwd = Utils.HashString(password); // userei un servizio separato che si occupa solo di hash e fare il controllo
            if (hashedPwd != user.Password)
            {
                return false;
            }

            return true;
        }

        public async Task<Response<RegistrationCredentials>> RegisterUser(RegistrationCredentials credentials)
        {
            var response = new Response<RegistrationCredentials>();

            try
            {
                var user = new User
                {
                    UserName = credentials.Username,
                    Name = credentials.Name,
                    Surname = credentials.Surname,
                    Email = credentials.Email,
                    Password = Utils.HashString(credentials.Password),
                    Questions = string.Join(",", credentials.Questions),
                    Answers = string.Join(",", credentials.Answers)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(RegisterUser), ex);
            }

            return response;
        }

        public async Task<Response<LoginCredentials>> ChangePassword(LoginCredentials loginCredentials)
        {
            var response = new Response<LoginCredentials>();
            try
            {
                var user = (await _context.Users.Where(x => x.UserName == loginCredentials.Username).ToListAsync()).FirstOrDefault();

                if (user == null) throw new Exception("The user doens't exist in the database");

                //QUESTION: qua vengono lanciate eccezioni ed in CheckCredentials si ritorna un Result.False.
                //Sarebbe da unificare
                if (loginCredentials.Password == null) throw new Exception("Trying to change password to null");

                user.Password = Utils.HashString(loginCredentials.Password);
                _context.Users.Update(user);

                // in RegisterUser non viene fatto questo check
                if ((await _context.SaveChangesAsync()) == 0)
                {
                    throw new Exception("No changes on database");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(ChangePassword), ex);
            }

            return response;
        }

        #region User Data
        public async Task<Response<UserData>> GetUserData(string username)
        {
            var response = new Response<UserData>();
            var userData = new UserData();

            try
            {
                var user = (await _context.Users.Where(x => x.UserName == username).ToListAsync()).FirstOrDefault();

                if (user == null) throw new Exception($"No user was found with username {username}.");

                //TODO: mapper da User (entità) a UserData (model)
                userData.Username = user.UserName ?? "";
                userData.Name = user.Name ?? "";
                userData.Surname = user.Surname ?? "";
                userData.Email = user.Email ?? "";

                response.Body.Add(userData);
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUserData), ex);
            }

            return response;
        }

        public async Task<Response<UserData>> UpdateUserData(UserData userData)
        {
            var response = new Response<UserData>();

            try
            {
                var user = (await _context.Users.Where(x => x.UserName == userData.Username).ToArrayAsync()).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception($"No user was found with username {userData.Username}.");
                }

                user.Surname = userData.Surname;
                user.Name = userData.Name;
                user.Email = userData.Email;

                _context.Users.Update(user);

                if ((await _context.SaveChangesAsync()) < 1)
                {
                    throw new Exception($"Failed to update database.");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(UpdateUserData), ex);
            }

            return response;
        }
        #endregion
    }
}
