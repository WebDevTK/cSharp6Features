using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using static cSharp6Features.MyStaticClass;
using System.Threading;

namespace cSharp6Features
{
    /* 
1. using Static.
2. Auto property initializer.
3. Dictionary Initializer.
4. nameof Expression.
5. New way for Exception filters.
6. await in catch and finally block.
7. Null – Conditional Operator.
8. Expression – Bodied Methods
9. Easily format strings – String interpolation 
        */
    class Program
    {
        private static double MultiplyNumbers(double n1, double n2) => n1 * n2;

        static void Main(string[] args)
        {
            WriteLine("Hello People!"); //using Static class system.console 
            HelloMyStaticClass();
            Customer cust = new Customer();
            WriteLine(cust.customerID);

            //dictionary intializer
            var countryList = new Dictionary<int, string>()
            {
                [1] = "India",
                [2] = "US",
                [3] = "UK"
            };
            countryList[4] = "Canada";
            foreach (var item in countryList)
            {
                WriteLine(item.Key + " -- " + item.Value);
            }

            string firstName = "Ketan"; string lastName = "Agnihotri";

            WriteLine($"I am {firstName} {lastName} from India.");

            DoSomething("Hi"); 
            WriteLine("Multiplication Result : "+MultiplyNumbers(100, 100));

            //Exception filters

            var httpStatusCode = 404;
            Write("HTTP Error: ");

            try
            {
                throw new Exception(httpStatusCode.ToString());
            }
            catch (Exception ex) when (ex.Message.Equals("400"))
            {
                Write("Bad Request");
            }
            catch (Exception ex) when (ex.Message.Equals("401"))
            {
                Write("Unauthorized");
            }
            catch (Exception ex) when (ex.Message.Equals("404"))
            {
                Write("Not Found - 404 \n");
            }

            Customer customer = new Customer();
            customer = null;
            WriteLine(customer?.Name ?? "customer name is null.");

            Task.Factory.StartNew(() => Div(12, 0)); // Async Await support in finally n catch block
             
            ReadLine();
            ReadLine();
        }

        public static void DoSomething(string newName) //Name of 
        {
            if (newName == null) throw new Exception(nameof(newName) + " is null");
        } 
    }

    static class MyStaticClass
    {
        public static void HelloMyStaticClass()
        {
            WriteLine("Hello My Static Class!");
        }

        public static async void Div(int value1, int value2)
        {
            try
            {
                int res = value1 / value2;
                WriteLine("Div : {0}", res);
            }
            catch (Exception ex)
            {
                await asyncMethodForCatch(ex);
            }
            finally
            {
                await asyncMethodForFinally();
            }
        }
        private static async Task asyncMethodForFinally()
        {
            Thread.Sleep(2000);
            WriteLine("\nMethod from async finally Method !!" );
        }

        private static async Task asyncMethodForCatch(Exception e)
        {
            Thread.Sleep(2000);
            WriteLine("\nMethod from async Catch Method !!"+ e.ToString());
        }
    }

    public class Customer
    {
        public Guid customerID { get; set; } = Guid.NewGuid(); // Auto property initializer 

        public string Name { get; set; } = "";
        public int Age { get; } = 18; // No setter Required
    } 
}
