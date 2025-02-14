using System;
using Microsoft.Extensions.DependencyInjection;
using ZooLibrary;

class Program
{
    static void Main()
    {
        VeterinaryClinic clinic = new();
        Zoo zoo = new(clinic);

        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить животное");
            Console.WriteLine("2. Добавить предмет");
            Console.WriteLine("3. Вывести отчет о потреблении еды");
            Console.WriteLine("4. Показать животных контактного зоопарка");
            Console.WriteLine("5. Показать инвентарь");
            Console.WriteLine("0. Выйти");
            Console.Write("Выберите действие: ");
            
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    string? animalType;
                    while (true)
                    {
                        Console.Write("Выберите животное (Rabbit, Monkey, Wolf, Tiger): ");
                        animalType = Console.ReadLine();
                        if (animalType == "Rabbit" || animalType == "Monkey" || 
                            animalType == "Wolf" || animalType == "Tiger")
                            break;
                        Console.WriteLine("Некорректный ввод, попробуйте снова");
                    }
                    
                    Console.Write("Введите инвентарный номер: ");
                    int number;
                    while (!int.TryParse(Console.ReadLine(), out number) || !zoo.IsInventoryNumberUnique(number))
                    {
                        Console.Write("Некорректный или повторяющийся номер, попробуйте снова: ");
                    }
                    bool isHealthy;
                    while (true)
                    {
                        Console.Write("Здорово ли животное? (true/false): ");
                        if (bool.TryParse(Console.ReadLine(), out isHealthy))
                            break;
                        Console.WriteLine("Некорректный ввод, введите 'true' или 'false'.");
                    }
                    Animal? animal = animalType switch
                    {
                        "Rabbit" => new Rabbit(number, isHealthy, 7),
                        "Monkey" => new Monkey(number, isHealthy, 7),
                        "Wolf" => new Wolf(number, isHealthy),
                        "Tiger" => new Tiger(number, isHealthy),
                        _ => null
                    };
                    if (animal != null) zoo.AddAnimal(animal);
                    break;
                case "2":
                    string? thingType;
                    while (true)
                    {
                        Console.Write("Выберите предмет (Table, Computer): ");
                        thingType = Console.ReadLine();
                        if (thingType == "Table" || thingType == "Computer")
                            break;
                        Console.WriteLine("Некорректный ввод, попробуйте снова");
                    }
                    Console.Write("Введите инвентарный номер: ");
                    while (!int.TryParse(Console.ReadLine(), out number) || !zoo.IsInventoryNumberUnique(number))
                    {
                        Console.Write("Некорректный или повторяющийся номер, попробуйте снова: ");
                    }
                    Thing? thing = thingType switch
                    {
                        "Table" => new Table(number),
                        "Computer" => new Computer(number),
                        _ => null
                    };
                    if (thing != null) zoo.AddThing(thing);
                    break;
                case "3":
                    zoo.ReportFoodConsumption();
                    break;
                case "4":
                    zoo.ListContactZooAnimals();
                    break;
                case "5":
                    zoo.ListInventory();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный ввод, попробуйте снова.");
                    break;
            }
        }
    }
}
