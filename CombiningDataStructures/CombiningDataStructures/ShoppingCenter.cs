namespace CombiningDataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class ShoppingCenter
    {
        private Dictionary<string, HashSet<Product>> byProducer;

        private Dictionary<string, HashSet<Product>> byName;

        private OrderedDictionary<decimal,HashSet<Product>> byPrice;

        public ShoppingCenter()
        {
            this.byProducer = new Dictionary<string, HashSet<Product>>();
            this.byName = new Dictionary<string, HashSet<Product>>();
            this.byPrice = new OrderedDictionary<decimal, HashSet<Product>>();
        }

        public string AddProduct(string name, decimal price, string producer)
        {
            var product = new Product(name, price, producer);
            var node = new LinkedListNode<Product>(product);

            if (!this.byProducer.ContainsKey(producer))
            {
                this.byProducer.Add(producer, new HashSet<Product>());
            }

            this.byProducer[producer].Add(product);

            if (!this.byName.ContainsKey(name))
            {
                this.byName.Add(name, new HashSet<Product>());
            }

            this.byName[name].Add(product);

            if (!this.byPrice.ContainsKey(price))
            {
                this.byPrice.Add(price, new HashSet<Product>());
            }

            this.byPrice[price].Add(product);

            return "Product added";
        }

        public string FindProductsByProducer(string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                return "No products found";
            }

            var products = this.byProducer[producer].OrderBy(p => p.Name).ThenBy(p => p.Price).ToList();

            if (products.Count == 0)
            {
                return "No products found";
            }

            return this.FormatOutput(products);
        }

        public string FindProductsByName(string name)
        {
            if (!this.byName.ContainsKey(name))
            {
                return "No products found";
            }

            var products = this.byName[name].OrderBy(p => p.Name).ThenBy(p=>p.Producer).ThenBy(p => p.Price).ToList();

            if (products.Count == 0)
            {
                return "No products found";
            }

            return this.FormatOutput(products);
        }

        public string DeleteProductsByProducer(string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                return "No products found";
            }

            var removed = this.byProducer[producer].Count;

            foreach (var p in this.byProducer[producer])
            {
                this.byName[p.Name].Remove(p);
                this.byPrice[p.Price].Remove(p);
            }

            this.byProducer.Remove(producer);

            return $"{removed} products deleted";
        }

        public string DeleteProductsByNameAndProducer(string name, string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                return "No products found";
            }

            var products = this.byProducer[producer].Where(p => p.Name == name).ToList();

            if (products.Count == 0)
            {
                return "No products found";
            }

            foreach (var p in products)
            {
                this.byProducer[producer].Remove(p);
                this.byName[name].Remove(p);
                this.byPrice[p.Price].Remove(p);
            }

            return $"{products.Count} products deleted";
        }

        public string FindProductsInRange(decimal from, decimal to)
        {
            var products = this.byPrice.Range(from,true,to,true)
                .SelectMany(l=>l.Value)
                .OrderBy(p=>p.Name)
                .ThenBy(p=>p.Producer)
                .ThenBy(p=>p.Price)
                .ToList();

            if (products.Count == 0)
            {
                return "No products found";
            }

            return this.FormatOutput(products);
        }

        private string FormatOutput(ICollection<Product> products)
        {
            return string.Join(Environment.NewLine, products.Select(p => $"{{{p.Name};{p.Producer};{p.Price.ToString("0.00")}}}"));
        }
    }
}
