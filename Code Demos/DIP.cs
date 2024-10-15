using System;

// Incorrect Approach - Violating DIP

// Low-level module
class MySqlDatabase
{
    public void Save(string data)
    {
        Console.WriteLine($"Saving data to MySQL database: {data}");
    }
}

// High-level module
class UserManager
{
    private readonly MySqlDatabase _database;

    public UserManager()
    {
        _database = new MySqlDatabase();
    }

    public void SaveUser(string userData)
    {
        _database.Save(userData);
    }
}

// Correct Approach - Adhering to DIP

// Abstraction
interface IDatabase
{
    void Save(string data);
}

// Low-level module implementing the abstraction
class MySqlDatabase : IDatabase
{
    public void Save(string data)
    {
        Console.WriteLine($"Saving data to MySQL database: {data}");
    }
}

// Another low-level module implementing the abstraction
class PostgreSqlDatabase : IDatabase
{
    public void Save(string data)
    {
        Console.WriteLine($"Saving data to PostgreSQL database: {data}");
    }
}

// High-level module depending on abstraction
class UserManager
{
    private readonly IDatabase _database;

    public UserManager(IDatabase database)
    {
        _database = database;
    }

    public void SaveUser(string userData)
    {
        _database.Save(userData);
    }
}

// Main class to demonstrate the usage
class Program
{
    static void Main(string[] args)
    {
        // Using MySQL
        IDatabase mySqlDb = new MySqlDatabase();
        UserManager userManager1 = new UserManager(mySqlDb);
        userManager1.SaveUser("John Doe");

        // Using PostgreSQL
        IDatabase postgreSqlDb = new PostgreSqlDatabase();
        UserManager userManager2 = new UserManager(postgreSqlDb);
        userManager2.SaveUser("Jane Doe");
    }
}