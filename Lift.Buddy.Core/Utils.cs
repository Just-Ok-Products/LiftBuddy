using Lift.Buddy.Core.DB.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Lift.Buddy.Core
{
    public class Utils
    {
        public static string HashString(string str)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string ErrorMessage(string function, Exception ex)
        {
            return $"{function} execution failed. Ex: {ex.Message}. InnerEx: {ex.InnerException?.Message ?? "-"}";
        }
    }
}
