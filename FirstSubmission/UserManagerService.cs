using System.Text.RegularExpressions;

namespace FirstSubmission;

//This is the service class that contains the methods for user registration.

public class UserManagerService
{
    //This acts like a in-memoy storage for the registered users.
    private readonly List<User> _registeredUsers = new List<User>();



    //The method below registers a user with the given username, password and email.
    public RegistrationResponse RegisterUser(string username, string password, string email)
    {
        //throw new NotImplementedException();
        if (!IsUniqueUsername(username)) //Check if the username already exists.
        {
            return new RegistrationResponse
            {
                IsSuccess = false,
                ErrorMessage = $"Username {username} already exists"
            };
        }


        if (!ValidateUsername(username)) //Check if the username format is valid.
        {
            return new RegistrationResponse
            {
                IsSuccess = false,
                ErrorMessage = "Invalid Username"
            };
        }

        if (!ValidateEmail(email)) //Check if the email format is valid.
        {
            return new RegistrationResponse
            {
                IsSuccess = false,
                ErrorMessage = "Invalid Email"
            };
        }

        if (!ValidatePassword(password)) //Check if the password format is valid.
        {
            return new RegistrationResponse
            {
                IsSuccess = false,
                ErrorMessage = "Invalid Password"
            };
        }
        // If all validations passed, create and store the new user
        _registeredUsers.Add( 
            new User
            {
                UserName = username,
                Password = password,
                Email = email
            }
         );

        // Return success response with a confirmation message
        return new RegistrationResponse
        {
            IsSuccess = true,
            Message = $"User with {username}  username is Registered"
        };

    }

    //The method below checks if the username is unique. Returns true if the username is unique, false if it exists.
    public bool IsUniqueUsername(string username)
    {
        //throw new NotImplementedException(); //This was removed after the test failed and I refactored the code.
        return !_registeredUsers.Exists(u => u.UserName.Equals(username));
    }

    public bool ValidateEmail(string email) //This method validates the email format.
    {
        //throw new NotImplementedException();
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        // Basic email validation using regex
        // This checks for the format: something@something.something
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool ValidatePassword(string password) // This method validates the password format and requirements.
    {
        //throw new NotImplementedException();
        //return password == "testing123";
        if (password.Length < 8)
        {
            return false;
        }
        // Check if the password contains at least one special character
        return password.Any(x => !char.IsLetterOrDigit(x));
    }

    public bool ValidateUsername(string username) //This method validates the username format and requirements.
    {
        //throw new NotImplementedException();
        //return username == "test";
        if (username.Length < 5 || username.Length > 20) //Check if the username is between 5 and 20 characters.
        {
            return false;
        }
        return username.All(x => char.IsLetterOrDigit(x));
    }
}
