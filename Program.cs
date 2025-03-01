using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace DZ_OTUS_LINQ
{
    internal class Program
    {
        static void Main()
        {



            IEnumerable<int> a = new List<int>() { 1, 2, 3, 4, 5 };
            //int[] a = [1, 2, 3, 4, 5 ];
            //var a = new Queue<int>();
            //a.Enqueue(1);
            //a.Enqueue(2);
            //a.Enqueue(3);

            var res=a.Take(2);
            foreach (int i in res) 
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', Console.WindowWidth));

            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var res2 = list.Top(30);
            foreach (int i in res2)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', Console.WindowWidth));
            var list3 = new List<Person> { new Person(20), new Person(18), new Person(40), new Person(35), new Person(28), new Person(69), new Person(4), new Person(80), new Person(28), new Person(15) };

            var res3 = list3.Top(30, person => person.Age);
            foreach (Person i in res3)
            {
                Console.WriteLine(i.Age);
            }
            
        }
    }

    public static class IEnumerableExtension
    {
        public static IEnumerable<T> Take<T>(this IEnumerable<T> collection, int count)
        {

            List<T> newCol = collection.ToList();
            List<T> returnCol = new();

            for (int i = 0; i < count; i++) 
            {
                try
                {
                    returnCol.Add(newCol[i]);
                }
                catch (Exception)
                {
                    break;
                }
            
            }
            return returnCol;


        }

        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, int percent) 
        {
            if (percent < 1 || percent > 100)
            { 
                throw new ArgumentException("Не может выходить за границы интервала от 1 до 100");
            }
            

            decimal countForReturn = (decimal)collection.Count()*percent/100;

            decimal celPart=Math.Truncate(countForReturn);

            int totalCountForReturn;

            if (countForReturn % celPart == 0)
            {
                totalCountForReturn = Decimal.ToInt32(celPart);
            }
            else 
            {
                totalCountForReturn = Decimal.ToInt32(celPart + 1);

            }

            var orderedCol = collection.OrderByDescending(n => n);

            return orderedCol.Take(totalCountForReturn);
        }


        public static IEnumerable<T> Top<T,S>(this IEnumerable<T> collection, int percent, Func<T, S> attributeForSort)
        {
            
            
            if (percent < 1 || percent > 100)
            {
                throw new ArgumentException("Не может выходить за границы интервала от 1 до 100");
            }


            decimal countForReturn = (decimal)collection.Count() * percent / 100;

            decimal celPart = Math.Truncate(countForReturn);

            int totalCountForReturn;

            if (countForReturn % celPart == 0)
            {
                totalCountForReturn = Decimal.ToInt32(celPart);
            }
            else
            {
                totalCountForReturn = Decimal.ToInt32(celPart + 1);

            }

            var orderedCol = collection.OrderByDescending(attributeForSort); 

            return orderedCol.Take(totalCountForReturn);
        }

    }
}
