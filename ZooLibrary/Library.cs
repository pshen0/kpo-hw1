namespace ZooLibrary;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IAlive
{
    int Food { get; }
}

public interface IInventory
{
    int Number { get; }
}

public abstract class Animal : IAlive, IInventory
{
    public int Food { get; protected set; }
    public int Number { get; }
    public string Name { get; }
    public bool IsHealthy { get; }

    protected Animal(string name, int food, int number, bool isHealthy)
    {
        Name = name;
        Food = food;
        Number = number;
        IsHealthy = isHealthy;
    }
}

public class Herbo : Animal
{
    public int Kindness { get; }

    public Herbo(string name, int food, int number, bool isHealthy, int kindness)
        : base(name, food, number, isHealthy)
    {
        Kindness = kindness;
    }
}

public class Predator : Animal
{
    public Predator(string name, int food, int number, bool isHealthy)
        : base(name, food, number, isHealthy) { }
}

public class Rabbit : Herbo
{
    public Rabbit(int number, bool isHealthy, int kindness)
        : base("Кролик", 2, number, isHealthy, kindness) { }
}

public class Tiger : Predator
{
    public Tiger(int number, bool isHealthy)
        : base("Тигр", 8, number, isHealthy) { }
}

public class Monkey : Herbo
{
    public Monkey(int number, bool isHealthy, int kindness)
        : base("Обезьяна", 3, number, isHealthy, kindness) { }
}

public class Wolf : Predator
{
    public Wolf(int number, bool isHealthy)
        : base("Волк", 5, number, isHealthy) { }
}

public abstract class Thing : IInventory
{
    public int Number { get; }
    public string Name { get; }

    protected Thing(string name, int number)
    {
        Name = name;
        Number = number;
    }
}

public class Table : Thing
{
    public Table(int number) : base("Стол", number) { }
}

public class Computer : Thing
{
    public Computer(int number) : base("Компьютер", number) { }
}

public class VeterinaryClinic
{
    public bool InspectAnimal(Animal animal) => animal.IsHealthy;
}

public class Zoo
{
    private readonly List<Animal> _animals = new();
    private readonly List<Thing> _things = new();
    private readonly VeterinaryClinic _clinic;

    public Zoo(VeterinaryClinic clinic)
    {
        _clinic = clinic;
    }

    public bool IsInventoryNumberUnique(int number)
    {
        return !_animals.Any(a => a.Number == number) && !_things.Any(t => t.Number == number);
    }

    public void AddAnimal(Animal animal)
    {
        if (_clinic.InspectAnimal(animal))
        {
            _animals.Add(animal);
            Console.WriteLine($"{animal.Name} добавлен в зоопарк.");
        }
        else
        {
            Console.WriteLine($"{animal.Name} не принят в зоопарк из-за состояния здоровья.");
        }
    }

    public void AddThing(Thing thing)
    {
        _things.Add(thing);
    }

    public void ReportFoodConsumption()
    {
        int totalFood = _animals.Sum(a => a.Food);
        Console.WriteLine($"Общее количество корма в день: {totalFood} кг");
    }

    public void ListContactZooAnimals()
    {
        var contactAnimals = _animals.OfType<Herbo>().Where(h => h.Kindness > 5);
        Console.WriteLine("Животные для контактного зоопарка:");
        foreach (var animal in contactAnimals)
        {
            Console.WriteLine($"- {animal.Name} (Инв. номер {animal.Number})");
        }
    }

    public void ListInventory()
    {
        Console.WriteLine("Инвентарные предметы и животные:");
        foreach (var item in _animals.Cast<IInventory>().Concat(_things))
        {
            Console.WriteLine($"- {item.GetType().Name} (Инв. номер {item.Number})");
        }
    }
}