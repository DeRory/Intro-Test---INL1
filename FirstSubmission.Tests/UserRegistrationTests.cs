namespace FirstSubmission.Tests;

//This is the test class for the UserManagerService class.
//It contains tests for the ValidateUsername, ValidatePassword, ValidateEmail, RegisterUser, and IsUniqueUsername methods.

[TestClass]
public sealed class UserRegistrationTests
{

    //Unique Username Tests

    [TestMethod] //A test that checks if the username is unique.
    public void IsUniqueUsername_CheckIfUsernameIsUnique_ShouldReturnTrue()
    {
        //Arrange - Create the service and a new username
        var userManager = new UserManagerService();
        string username = "UniqueTest";

        // Act - Check if the username is unique
        bool result = userManager.IsUniqueUsername(username);

        // Assert - Verify username is considered unique
        Assert.IsTrue(result);
    }

    [TestMethod] //A test that checks if the username is not unique.
    public void IsUsernameUnique_CheckIfUsernameIsNotUnique_ShouldReturnFalse()
    {
        //Arrange - Create the service and register a user
        var userManager = new UserManagerService();
        string username = "existinguser";
        var user = new User
        {
            UserName = username,
            Email = "existing@example.com",
            Password = "Password@123"
        };

        // Register first user
        userManager.RegisterUser(user.UserName, user.Password, user.Email);

        // Act - Check if the same username is unique
        var result = userManager.IsUniqueUsername(username);

        // Assert - Verify username is not considered unique
        Assert.IsFalse(result);
    }

    // Validate Username Tests

    [TestMethod] //This a happy path test case.
    public void ValidateUsername_ShouldReturnTrue()
    {
        //Arrange - Create the service and a valid username
        var userManager = new UserManagerService();
        string username = "testing123";

        //Act - Attempt to validate the username
        bool result = userManager.ValidateUsername(username);

        //Assert - Verify the username is considered valid
        Assert.IsTrue(result);
    }

    [TestMethod] //A test that checks if the username is too short.
    public void ValidateUsername_TooShort_ShouldReturnFalse()
    {
        //Arrange - Creating the service and a username that is too short.
        var userManager = new UserManagerService();
        string username = "tes";

        //Act - Validating the username
        bool result = userManager.ValidateUsername(username);

        //Assert - Verifying the username is considered invalid
        Assert.IsFalse(result);
    }

    [TestMethod] //A test that checks if the username is too long.
    public void ValidateUsername_TooLong_ShouldReturnFalse()
    {
        //Arrange - Creating the service and a username that is too long.
        var userManager = new UserManagerService();
        string username = "thisuserhastoomanycharacters";

        //Act - Validating the username
        bool result = userManager.ValidateUsername(username);

        //Assert - Verifying the username is considered invalid
        Assert.IsFalse(result);
    }

    //Validate Password Tests

    [TestMethod] //A test that checks if the password is valid.
    public void ValidatePassword_ValidPassword_ShouldReturnTrue()
    {
        //Arrange
        var userManager = new UserManagerService();
        string password = "testing123!";

        //Act
        bool result = userManager.ValidatePassword(password);

        //Assert
        Assert.IsTrue(result);
    }

    [TestMethod] //A test that checks if the password has special characters.
    public void ValidatePassword_NoSpecialCharacter_ReturnsFalse()
    {
        //Arrange
        var userManager = new UserManagerService();
        string password = "testing123";

        //Act
        bool result = userManager.ValidatePassword(password);

        //Assert
        Assert.IsFalse(result);
    }

    [TestMethod] //A test that checks if the password is too short.
    public void ValidatePassword_TooShort_ReturnsFalse()
    {
        //Arrange
        var userManager = new UserManagerService();
        string password = "test";

        //Act
        bool result = userManager.ValidatePassword(password);

        //Assert
        Assert.IsFalse(result);
    }

    //Validate Email Tests

    [TestMethod] //A test that checks if the email is valid.
    public void ValidateEmail_ValidEmailFormat_ReturnsTrue()
    {
        //Arrange
        var userManager = new UserManagerService();
        string email = "test@example.com";

        //Act
        bool result = userManager.ValidateEmail(email);

        //Assert
        Assert.IsTrue(result);
    }

    [TestMethod] //A test that checks if the email is in the correct format.
    public void ValidateEmail_NotValidFormat_ReturnsFalse()
    {
        //Arrange
        var userManager = new UserManagerService();
        string email = "test-example.com";

        //Act
        bool result = userManager.ValidateEmail(email);

        //Assert
        Assert.IsFalse(result);
    }

    [TestMethod] //A test that checks if the email has a domain.
    public void ValidateEmail_NoDomainEmail_ReturnsFalse()
    {
        //Arrange
        var userManager = new UserManagerService();
        string email = "test@";

        //Act
        bool result = userManager.ValidateEmail(email);

        //Assert
        Assert.IsFalse(result);
    }

    [TestMethod] //A test that checks if the email has an @ symbol.
    public void ValidateEmail_NoAtSymbol_ReturnsFalse()
    {
        // Arrange
        var userManager = new UserManagerService();
        string email = "userexample.com";

        // Act
        bool result = userManager.ValidateEmail(email);

        // Assert
        Assert.IsFalse(result);
    }


    //Register Users Tests

    [TestMethod] //A test that register user with valid inputs.
    public void RegisterUser_ValidUser_ShouldReturnTrue()
    {

        var userManager = new UserManagerService();
        string UserName = "testingUser";
        string password = "testing123!";
        string email = "test@example.com";

        //Act
        var result = userManager.RegisterUser(UserName, password, email);

        //Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual($"User with {UserName}  username is Registered", result.Message);

    }

    [TestMethod] //A test that fails to register a user with invalid username.
    public void RegisterUser_InvalidUser_ShouldReturnFalse()
    {

        var userManager = new UserManagerService();
        string UserName = "tes"; // Too short name, invalid.
        string password = "Valid@Pass123";
        string email = "test@example.com";

        //Act - Attempt to register the user with invalid username
        var result = userManager.RegisterUser(UserName, password, email);

        //Assert - A verification that the user was not registered
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Invalid Username", result.ErrorMessage);

    }

    [TestMethod] //A test that fails to register a user with invalid password.
    public void RegisterUser_InvalidPassword_ShouldReturnFalse()
    {

        var userManager = new UserManagerService();
        string UserName = "AValidUser";
        string password = "shortpw"; //Too short and no special characters. Invalid.
        string email = "test@example.com";

        //Act - Attempt to register with invalid password
        var result = userManager.RegisterUser(UserName, password, email);

        //Assert - Verify registration failed
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Invalid Password", result.ErrorMessage);

    }

    [TestMethod] //A test that fails to register when using duplicate Username.
    public void RegisterUser_DuplicateUsername_ShouldReturnFalse()
    {
        // Arrange - Create the service and two users with the same username
        var userManager = new UserManagerService();
        string UserName = "duplicateuser";
        var firstUser = new User
        {
            UserName = UserName,
            Password = "Valid@Pass123",
            Email = "first@example.com"
        };

        var secondUser = new User
        {
            UserName = UserName, // Same username as first user
            Password = "Different@Pass456",
            Email = "second@example.com"
        };

        // Registers the first user
        userManager.RegisterUser(firstUser.UserName, firstUser.Password, firstUser.Email);

        // Act - Attempt to register second user with duplicate username
        var result = userManager.RegisterUser(secondUser.UserName, secondUser.Password, secondUser.Email);

        //Assert - Verify second registration failed
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual($"Username {UserName} already exists", result.ErrorMessage);
    }

    [TestMethod] //A test that shows error message when registering with invalid email.
    public void RegisterUser_EmailNotValidWhenMissingAtSymbol_ShouldReturnErrorMessage()
    {
        // Arrange - Create the service and a user with an invalid email
        var userManager = new UserManagerService();
        var firstUser = new User
        {
            UserName = "UserTest",
            Password = "Valid@Pass123",
            Email = "first.example.com"
        };

        // Register user
        var result = userManager.RegisterUser(firstUser.UserName, firstUser.Password, firstUser.Email);

        //Assert - Verify invalid email registration failed
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Invalid Email", result.ErrorMessage);
    }

    [TestMethod] //A test that shows error message when registering with invalid email.
    public void RegisterUser_EmailNotValidWhenMissingDomain_ShouldReturnErrorMessage()
    {
        // Arrange - Create the service and a user with an invalid email
        var userManager = new UserManagerService();
        var firstUser = new User
        {
            UserName = "UserTest",
            Password = "Valid@Pass123",
            Email = "first@example."
        };

        // Register user
        var result = userManager.RegisterUser(firstUser.UserName, firstUser.Password, firstUser.Email);

        //Assert - Verify invalid email registration failed
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Invalid Email", result.ErrorMessage);
    }

}


