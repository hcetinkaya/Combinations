using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationsToFitSum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var combinations = new Combinations();

            var ar = new List<double>();
            var sum = 11.90;

            ar.Add(3.15);
            ar.Add(0.40);

            var res = Combinations.CombinationSum(ar, sum);

            combinations.printList(res);

            Console.Read();
        }
    }

    public class Combinations
    {
        private static void FindNumbers(IReadOnlyList<double> ar, double sum, ICollection<List<double>> res,
            List<double> r, int i)
        {
            const double tolerance = 2.0;

            // If current sum becomes negative 
            if (sum < 0) return;

            // if we get exact answer 
            if (Math.Abs(sum) < tolerance)
            {
                res.Add(r.GetRange(0, r.Count));
                return;
            }

            // Recur for all remaining elements that 
            // have value smaller than sum. 

            var diff = sum - ar[i];
            while (i < ar.Count() && Math.Abs(diff) >= tolerance)
            {
                // Till every element in the array starting 
                // from i which can contribute to the sum 
                r.Add(ar[i]); // Add them to list

                // recur for next numbers                 
                FindNumbers(ar, sum - ar[i], res, r, i);
                i++;

                r.RemoveAt(r.Count - 1);
            }
        }

        // Returns all combinations of ar[] that have given 
        // sum. 
        public static IEnumerable<List<double>> CombinationSum(List<double> ar, double sum)
        {
            // sort input array 
            ar.Sort();

            // remove duplicates 
            ar = ar.Union(ar).ToList();

            var r = new List<double>();
            var res = new List<List<double>>();

            FindNumbers(ar, sum, res, r, 0);

            return res;
        }

        public void printList(IEnumerable<List<double>> list)
        {
            foreach (var elements in list)
            {
                if (!elements.Any()) Console.WriteLine("No results!");

                Console.Write("(");
                for (var i = 0; i < elements.Count; i++)
                {
                    Console.Write(elements[i]);
                    if (i == elements.Count-1) continue;
                    Console.Write(", ");
                }

                Console.Write(") ");
                Console.WriteLine();
            }
        }
    }
}
