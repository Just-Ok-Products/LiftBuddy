using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Lift.Buddy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Lift.Buddy.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly LiftBuddyContext _context;
        private readonly IDatabaseMapper _mapper;

        public LoginService(LiftBuddyContext context, IDatabaseMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<SecurityQuestionDTO>> GetSecurityQuestions(string username)
        {
            var response = new Response<SecurityQuestionDTO>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

                if (user == null) throw new KeyNotFoundException($"User '{username}' doesn't exist");

                var securityQuestion = user.SecurityQuestions?
                    .Select(q => new SecurityQuestionDTO(q.Question, q.Answer));

                response.Result = true;
                response.Body = securityQuestion ?? Enumerable.Empty<SecurityQuestionDTO>();
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetSecurityQuestions), ex);
            }

            return response;
        }

        public async Task<(Guid UserId, bool IsValid)> CheckCredentials(Credentials credentials)
        {
            if (!credentials.HasValues())
                return (Guid.Empty, false);

            var username = credentials.Username;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null) throw new KeyNotFoundException($"User '{username}' doesn't exist");

            var hashedPwd = Utils.HashString(credentials.Password); // userei un servizio separato che si occupa solo di hash e fare il controllo
            return (user.UserId, hashedPwd == user.Password);
        }

        public async Task<Response<UserDTO>> RegisterUser(UserDTO user)
        {
            var response = new Response<UserDTO>();

            try
            {
                var newUser = _mapper.Map(user);
                newUser.UserId = Guid.NewGuid();

                await _context.Users.AddAsync(newUser);
                if ((await _context.SaveChangesAsync()) == 0)
                {
                    throw new Exception($"User '{user.Credentials.Username}' was not added to the database.");
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(RegisterUser), ex);
            }

            return response;
        }

        public async Task<Response<Credentials>> ChangePassword(Credentials credentials)
        {
            var response = new Response<Credentials>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == credentials.Username);

                if (user == null) throw new KeyNotFoundException($"User '{credentials.Username}' doesn't exist");

                if (credentials.Password == null) throw new Exception("Trying to change password to null");

                user.Password = Utils.HashString(credentials.Password);
                _context.Users.Update(user);

                if ((await _context.SaveChangesAsync()) == 0)
                {
                    throw new Exception($"Password for user '{credentials.Username}' could not updated in the database.");
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
        public async Task<Response<UserDTO>> GetUserData(Guid userId)
        {
            var response = new Response<UserDTO>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (user == null) throw new KeyNotFoundException($"User '{userId}' doesn't exist");

                var userData = _mapper.Map(user);

                response.Body = new UserDTO[] { userData };
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Notes = Utils.ErrorMessage(nameof(GetUserData), ex);
            }

            return response;
        }

        public async Task<Response<UserDTO>> UpdateUserData(UserDTO userData)
        {
            var response = new Response<UserDTO>();

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Username == userData.Credentials.Username);

                if (user == null) throw new KeyNotFoundException($"User '{userData.Credentials.Username}' doesn't exist");

                // TODO check che i campi non diventino vuoti
                user.Username = userData.Userame;
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
