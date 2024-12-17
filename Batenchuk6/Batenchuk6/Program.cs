using System;
using System.Globalization;

class Currency
{
    public string Name { get; set; }
    public double ExRate { get; set; }

    public Currency()
    {
        Name = "UAH";
        ExRate = 1.0;
    }

    public Currency(string name, double exRate)
    {
        Name = name;
        ExRate = exRate;
    }

    public Currency(Currency other)
    {
        Name = other.Name;
        ExRate = other.ExRate;
    }

    public override string ToString()
    {
        return $"Валюта: {Name}, Курс: {ExRate}";
    }
}

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public Currency Cost { get; set; }
    public int Quantity { get; set; }
    public string Producer { get; set; }
    public double Weight { get; set; }

    public Product()
    {
        Name = "Невідомо";
        Price = 0.0;
        Cost = new Currency();
        Quantity = 0;
        Producer = "Невідомо";
        Weight = 0.0;
    }

    public Product(string name, double price, Currency cost, int quantity, string producer, double weight)
    {
        Name = name;
        Price = price;
        Cost = cost;
        Quantity = quantity;
        Producer = producer;
        Weight = weight;
    }

    public Product(Product other)
    {
        Name = other.Name;
        Price = other.Price;
        Cost = new Currency(other.Cost);
        Quantity = other.Quantity;
        Producer = other.Producer;
        Weight = other.Weight;
    }

    public double GetPriceInUAH()
    {
        return Price * Cost.ExRate;
    }

    public double GetWeightInPounds()
    {
        return Weight * 2.20462;
    }

    public override string ToString()
    {
        return $"Назва: {Name}, Ціна: {Price} {Cost.Name}, Кількість: {Quantity}, Виробник: {Producer}, Вага: {Weight} кг";
    }
}

class Program
{
    public static Product[] CreateProductsArray(int n)
    {
        Product[] products = new Product[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nВведіть дані для товару {i + 1}:");
            Console.Write("Назва: ");
            string name = Console.ReadLine();

            Console.Write("Ціна: ");
            double price = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.Write("Назва валюти: ");
            string currencyName = Console.ReadLine();

            Console.Write("Курс валюти: ");
            double exRate = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Currency currency = new Currency(currencyName, exRate);

            Console.Write("Кількість: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Виробник: ");
            string producer = Console.ReadLine();

            Console.Write("Вага (кг): ");
            double weight = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            products[i] = new Product(name, price, currency, quantity, producer, weight);
        }
        return products;
    }

    public static void DisplayProduct(Product product)
    {
        Console.WriteLine(product);
        Console.WriteLine($"Ціна в гривнях: {product.GetPriceInUAH():F2}");
        Console.WriteLine($"Вага в фунтах: {product.GetWeightInPounds():F2}");
    }

    public static void DisplayAllProducts(Product[] products)
    {
        foreach (var product in products)
        {
            DisplayProduct(product);
            Console.WriteLine("--------------------");
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Product[] products = null;

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Створити масив товарів");
            Console.WriteLine("2. Вивести дані про окремий товар");
            Console.WriteLine("3. Вивести дані про всі товари");
            Console.WriteLine("4. Вихід");
            Console.Write("Оберіть пункт меню: ");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Write("Введіть кількість товарів: ");
                    int n = int.Parse(Console.ReadLine());
                    products = CreateProductsArray(n);
                    break;
                case 2:
                    if (products != null)
                    {
                        Console.Write("Введіть індекс товару для виведення: ");
                        int index = int.Parse(Console.ReadLine());
                        if (index >= 0 && index < products.Length)
                            DisplayProduct(products[index]);
                        else
                            Console.WriteLine("Неправильний індекс.");
                    }
                    else
                    {
                        Console.WriteLine("Масив товарів не створено.");
                    }
                    break;
                case 3:
                    if (products != null)
                        DisplayAllProducts(products);
                    else
                        Console.WriteLine("Немає товарів для відображення.");
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Неправильний вибір.");
                    break;
            }
        }
    }
}

