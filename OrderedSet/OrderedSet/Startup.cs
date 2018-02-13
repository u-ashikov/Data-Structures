namespace OrderedSet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            var set = new OrderedSet<int>();

            set.Add(1);
            set.Add(2);
            set.Add(3);
            set.Add(-1);
            set.Add(10);

            set.Remove(1);

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
        }
    }
}
