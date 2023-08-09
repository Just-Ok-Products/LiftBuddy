using Lift.Buddy.Core.Models;

namespace Lift.Buddy.API.Interfaces
{
    public interface ILoginService
    {

        Task<Response> GetSecurityQuestions(string username);
        bool CheckCredentials(LoginCredentials credentials);
        Task<Response> RegisterUser(RegistrationCredentials registerCredentials);

        Task<Response> ChangePassword(LoginCredentials loginCredentials);
    }
}
