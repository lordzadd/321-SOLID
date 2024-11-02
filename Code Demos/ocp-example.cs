using System;
using System.Collections.Generic;

//===================================================
// INCORRECT APPROACH - Violating OCP
//===================================================

public class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
}

public class Circle
{
    public double Radius { get; set; }
}

public class AreaCalculator
{
    public double CalculateArea(object shape)
    {
        if (shape is Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }
        else if (shape is Circle circle)
        {
            return Math.PI * circle.Radius * circle.Radius;
        }
        throw new ArgumentException("Unsupported shape type");
    }
}

// Usage of incorrect approach
public class IncorrectExample
{
    public static void ShowExample()
    {
        var calculator = new AreaCalculator();
        var rectangle = new Rectangle { Width = 5, Height = 4 };
        var circle = new Circle { Radius = 3 };

        Console.WriteLine($"Rectangle area: {calculator.CalculateArea(rectangle)}");
        Console.WriteLine($"Circle area: {calculator.CalculateArea(circle)}");

        // Problem: Adding a new shape requires modifying AreaCalculator
        // This violates OCP as the class is not closed for modification
    }
}

//===================================================
// CORRECT APPROACH - Following OCP
//===================================================

// 1. Define the abstraction
public interface IShape
{
    double CalculateArea();
}

// 2. Implement shapes
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public double CalculateArea()
    {
        return Width * Height;
    }
}

public class Circle : IShape
{
    public double Radius { get; set; }

    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

// 3. Area Calculator that's closed for modification
public class AreaCalculator
{
    public double CalculateArea(IShape shape)
    {
        return shape.CalculateArea();
    }

    public double CalculateTotalArea(IEnumerable<IShape> shapes)
    {
        double total = 0;
        foreach (var shape in shapes)
        {
            total += shape.CalculateArea();
        }
        return total;
    }
}

// 4. Adding a new shape WITHOUT modifying existing code
public class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public double CalculateArea()
    {
        return (Base * Height) / 2;
    }
}

// Usage of correct approach
public class CorrectExample
{
    public static void ShowExample()
    {
        var calculator = new AreaCalculator();
        var shapes = new List<IShape>
        {
            new Rectangle { Width = 5, Height = 4 },
            new Circle { Radius = 3 },
            new Triangle { Base = 3, Height = 6 }
        };

        // Calculate individual areas
        foreach (var shape in shapes)
        {
            Console.WriteLine($"Area: {calculator.CalculateArea(shape)}");
        }

        // Calculate total area
        Console.WriteLine($"Total area: {calculator.CalculateTotalArea(shapes)}");
    }
}

//===================================================
// Benefits of the Correct Approach:
//===================================================
// 1. New shapes can be added without modifying existing code
// 2. AreaCalculator is closed for modification but open for extension
// 3. Each shape handles its own area calculation
// 4. Better maintainability and reduced risk of bugs
// 5. Follows the "plugins" architecture principle
