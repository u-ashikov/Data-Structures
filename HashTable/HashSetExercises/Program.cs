	using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    
public class Program
    {
        public static void Main()
        {
            CountSymbols();

            //Phonebook();
        }

        private static void Phonebook()
        {
            var text = Console.ReadLine();
            var phonebook = new HashTable<string, string>();

            while (text != "search")
            {
                var info = text.Split('-');
                var name = info[0];
                var number = info[1];

                phonebook.AddOrReplace(name, number);

                text = Console.ReadLine();
            }

            var searchedName = Console.ReadLine();

            while (!string.IsNullOrEmpty(searchedName))
            {
                if (phonebook.ContainsKey(searchedName))
                {
                    Console.WriteLine($"{searchedName} -> {phonebook[searchedName]}");
                }
                else
                {
                    Console.WriteLine($"Contact {searchedName} does not exist.");
                }

                searchedName = Console.ReadLine();
            }
        }

        private static void CountSymbols()
        {
            var text = Console.ReadLine();
            var result = new HashTable<char, int>();

            foreach (var letter in text)
            {
                if (!result.ContainsKey(letter))
                {
                    result.Add(letter, 0);
                }

                result[letter]++;
            }

            foreach (var kvp in result.OrderBy(kvp => kvp.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} time/s");
            }
        }
    }
