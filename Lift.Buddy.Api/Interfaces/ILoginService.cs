using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface ILoginService
    {

        Task<Response<SecurityQuestions>> GetSecurityQuestions(string username);
        bool CheckCredentials(LoginCredentials credentials);
        Task<Response<RegistrationCredentials>> RegisterUser(RegistrationCredentials registerCredentials);

        Task<Response<LoginCredentials>> ChangePassword(LoginCredentials loginCredentials);
    }
}
