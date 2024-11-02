using System;
using System.Collections.Generic;

//===================================================
// INCORRECT APPROACH - Violating LSP
//===================================================

public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying high in the sky!");
    }
}

public class Duck : Bird
{
    public override void Fly()
    {
        Console.WriteLine("Duck flying!");
    }
}

public class Ostrich : Bird
{
    public override void Fly()
    {
        // Violates LSP because Ostrich can't actually fly
        throw new NotSupportedException("Ostriches can't fly!");
    }
}

// Usage of incorrect approach
public class IncorrectExample
{
    public static void MakeBirdFly(Bird bird)
    {
        // This might throw an exception for Ostrich!
        bird.Fly();
    }

    public static void ShowExample()
    {
        var birds = new List<Bird>
        {
            new Bird(),
            new Duck(),
            new Ostrich() // This will cause problems!
        };

        foreach (var bird in birds)
        {
            try
            {
                MakeBirdFly(bird); // Might throw exception
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

//===================================================
// CORRECT APPROACH - Following LSP
//===================================================

// 1. Base class with common behavior
public abstract class Animal
{
    public virtual void Move()
    {
        Console.WriteLine("Moving...");
    }

    public virtual void MakeSound()
    {
        Console.WriteLine("Making sound...");
    }
}

// 2. Interface for flying behavior
public interface IFlyable
{
    void Fly();
}

// 3. Interface for running behavior
public interface IRunnable
{
    void Run();
}

// 4. Proper implementation of different birds
public class Duck : Animal, IFlyable, IRunnable
{
    public void Fly()
    {
        Console.WriteLine("Duck flying!");
    }

    public void Run()
    {
        Console.WriteLine("Duck running!");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Quack!");
    }
}

public class Ostrich : Animal, IRunnable
{
    public void Run()
    {
        Console.WriteLine("Ostrich running fast!");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Boom!");
    }
}

// 5. Bird management that respects LSP
public class BirdManager
{
    public void MakeFly(IFlyable flyingBird)
    {
        flyingBird.Fly(); // Safe - only flying birds implemented IFlyable
    }

    public void MakeRun(IRunnable runningBird)
    {
        runningBird.Run(); // Safe - all birds can run
    }
}

// Usage of correct approach
public class CorrectExample
{
    public static void ShowExample()
    {
        var birdManager = new BirdManager();
        var duck = new Duck();
        var ostrich = new Ostrich();

        // Safe operations - no exceptions possible
        birdManager.MakeFly(duck);     // Works fine
        birdManager.MakeRun(duck);     // Works fine
        birdManager.MakeRun(ostrich);  // Works fine
        // birdManager.MakeFly(ostrich); // Won't compile - ostrich doesn't implement IFlyable

        // Both are still animals
        var animals = new List<Animal> { duck, ostrich };
        foreach (var animal in animals)
        {
            animal.Move();      // Works for all animals
            animal.MakeSound(); // Works for all animals
        }
    }
}

//===================================================
// Benefits of the Correct Approach:
//===================================================
// 1. No runtime exceptions due to unsupported operations
// 2. Compile-time safety
// 3. Clear separation of capabilities
// 4. Each subtype can be used in place of its base type
// 5. Behavior is predictable and consistent
// 6. Easy to add new types without breaking existing code
