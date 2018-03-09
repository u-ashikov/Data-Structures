namespace CombiningDataStructures
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var commandsCount = int.Parse(Console.ReadLine());
            var center = new ShoppingCenter();

            for (int i = 0; i < commandsCount; i++)
            {
                var input = Console.ReadLine();
                var command = input.Substring(0, input.IndexOf(' '));
                var arguments = input.Substring(input.IndexOf(' ')).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToList();

                switch (command)
                {
                    case "AddProduct":
                        Console.WriteLine(center.AddProduct(arguments[0],decimal.Parse(arguments[1]), arguments[2]));
                        break;
                    case "FindProductsByProducer":
                        Console.WriteLine(center.FindProductsByProducer(arguments[0]));
                        break;
                    case "FindProductsByName":
                        Console.WriteLine(center.FindProductsByName(arguments[0]));
                        break;
                    case "DeleteProducts":
                        if (arguments.Count == 1)
                        {
                            Console.WriteLine(center.DeleteProductsByProducer(arguments[0]));
                        }
                        else
                        {
                            Console.WriteLine(center.DeleteProductsByNameAndProducer(arguments[0],arguments[1]));
                        }
                        break;
                    case "FindProductsByPriceRange":
                        var ranges = arguments.Select(decimal.Parse).ToList();
                        Console.WriteLine(center.FindProductsInRange(ranges[0],ranges[1]));
                        break;
                }
            }
        }
    }
}
