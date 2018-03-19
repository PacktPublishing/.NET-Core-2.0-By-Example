using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DllImport
{
    class Program
    {
        [DllImport(@"Dll\ExampleDLL.dll")]
        static extern int Multiply(int number1, int number2);

        [DllImport(@"Dll\ExampleDLL.dll")]
        static extern double divide(int number1, int number2);

        [DllImport(@"Dll\ExampleDLL.dll")]
        static extern int Sum(int a, int b);

        static void Main(string[] args)
        {

            Console.WriteLine("Enter number1");
            int number1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number2");
            int number2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Select Operation : 1 for Sum , 2 for multiply, 3 for divide");
            int option = Convert.ToInt32(Console.ReadLine());
            int result = 0;
            switch (option)
            {
                case 1:
                    result = Sum(number1, number2);
                    Console.WriteLine("You have selected Sum operation! Sum is : " + result);
                    Console.ReadLine();
                    break;
                case 2:
                    result = Multiply(number1, number2);
                    Console.WriteLine("You have selected Sum operation! multiplication is : " + result);
                    Console.ReadLine();
                    break;
                case 3:
                    double result1 = divide(number1, number2);
                    Console.WriteLine("You have selected Sum operation! division is : " + result);
                    Console.ReadLine();
                    break;
            }
        }
    }
}


