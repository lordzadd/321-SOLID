using System;

//===================================================
// INCORRECT APPROACH - Violating SRP
//===================================================

public class UserService
{
    public void CreateUser(string username, string email)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            throw new ArgumentException("Invalid input");

        // Database logic
        Console.WriteLine($"Saving user {username} to database");

        // Email logic
        Console.WriteLine($"Sending welcome email to {email}");

        // Logging logic
        Console.WriteLine("Logging user creation");
    }

    public bool ValidateEmail(string email)
    {
        // Email validation logic
        return email.Contains("@");
    }

    public void GenerateUserReport(string username)
    {
        // Report generation logic
        Console.WriteLine($"Generating report for user {username}");
    }
}

// Usage of incorrect approach
public class IncorrectExample
{
    public static void ShowExample()
    {
        var userService = new UserService();
        userService.CreateUser("john_doe", "john@example.com");
        // Single class handling multiple responsibilities
        // Hard to modify one aspect without affecting others
    }
}

//===================================================
// CORRECT APPROACH - Following SRP
//===================================================

// 1. User Creation and Validation
public class UserValidator
{
    public bool ValidateUser(string username, string email)
    {
        return !string.IsNullOrEmpty(username) && 
               !string.IsNullOrEmpty(email) && 
               email.Contains("@");
    }
}

// 2. Database Operations
public class UserRepository
{
    public void SaveUser(string username, string email)
    {
        Console.WriteLine($"Saving user {username} to database");
    }
}

// 3. Email Service
public class EmailService
{
    public void SendWelcomeEmail(string email)
    {
        Console.WriteLine($"Sending welcome email to {email}");
    }
}

// 4. Logging Service
public class LoggingService
{
    public void LogUserCreation(string username)
    {
        Console.WriteLine($"Logging creation of user {username}");
    }
}

// 5. Report Generation
public class ReportGenerator
{
    public void GenerateUserReport(string username)
    {
        Console.WriteLine($"Generating report for user {username}");
    }
}

// Orchestration Service
public class UserService
{
    private readonly UserValidator _validator;
    private readonly UserRepository _repository;
    private readonly EmailService _emailService;
    private readonly LoggingService _loggingService;

    public UserService(
        UserValidator validator,
        UserRepository repository,
        EmailService emailService,
        LoggingService loggingService)
    {
        _validator = validator;
        _repository = repository;
        _emailService = emailService;
        _loggingService = loggingService;
    }

    public void CreateUser(string username, string email)
    {
        if (!_validator.ValidateUser(username, email))
            throw new ArgumentException("Invalid input");

        _repository.SaveUser(username, email);
        _emailService.SendWelcomeEmail(email);
        _loggingService.LogUserCreation(username);
    }
}

// Usage of correct approach
public class CorrectExample
{
    public static void ShowExample()
    {
        var userService = new UserService(
            new UserValidator(),
            new UserRepository(),
            new EmailService(),
            new LoggingService()
        );

        userService.CreateUser("john_doe", "john@example.com");
    }
}

//===================================================
// Benefits of the Correct Approach:
//===================================================
// 1. Each class has a single responsibility
// 2. Changes to one aspect don't affect others
// 3. Classes can be tested independently
// 4. Easy to maintain and modify individual components
// 5. Better code organization and readability
