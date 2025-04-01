using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSubmission
{
    public class User //A class that contains the properties of a user.
    {
        public string UserName { get; set; } = string.Empty; //A string that contains the username of the user.
        public string Password { get; set; } = string.Empty; //A string that contains the password of the user.
        public string Email { get; set; } = string.Empty; //A string that contains the email of the user.
    }

    public class RegistrationResponse //A class that contains the properties of a registration response.
    {
        public bool IsSuccess { get; set; } //A bool that indicates if the registration was successful.
        public string ErrorMessage { get; set; } = string.Empty; //A string message that indicates the error message if the registration failed.
        public string Message { get; set; } = string.Empty; //A string message that indicates the success message if the registration was successful.
    }
}
