using System;

// Incorrect Approach - Violating ISP

interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

class Human : IWorker
{
    public void Work()
    {
        Console.WriteLine("Human is working");
    }

    public void Eat()
    {
        Console.WriteLine("Human is eating");
    }

    public void Sleep()
    {
        Console.WriteLine("Human is sleeping");
    }
}

class Robot : IWorker
{
    public void Work()
    {
        Console.WriteLine("Robot is working");
    }

    public void Eat()
    {
        // Robots don't eat, but forced to implement this method
        throw new NotImplementedException("Robots don't eat");
    }

    public void Sleep()
    {
        // Robots don't sleep, but forced to implement this method
        throw new NotImplementedException("Robots don't sleep");
    }
}

// Correct Approach - Adhering to ISP

interface IWorkable
{
    void Work();
}

interface IEatable
{
    void Eat();
}

interface ISleepable
{
    void Sleep();
}

class Human : IWorkable, IEatable, ISleepable
{
    public void Work()
    {
        Console.WriteLine("Human is working");
    }

    public void Eat()
    {
        Console.WriteLine("Human is eating");
    }

    public void Sleep()
    {
        Console.WriteLine("Human is sleeping");
    }
}

class Robot : IWorkable
{
    public void Work()
    {
        Console.WriteLine("Robot is working");
    }
}

// Main class to demonstrate the usage
class Program
{
    static void Main(string[] args)
    {
        Human human = new Human();
        human.Work();
        human.Eat();
        human.Sleep();

        Robot robot = new Robot();
        robot.Work();
        // robot.Eat(); // This would cause a compilation error now, which is good
        // robot.Sleep(); // This would cause a compilation error now, which is good
    }
}