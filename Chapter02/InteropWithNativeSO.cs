using System;
using System.Runtime.InteropServices;


namespace Hello
{
    class Program
    { [DllImport("/home/neha/Documents/InteropWithMonoLib/libHelloSO.so")]
      private static extern void hello(int i, int j,int c);

      

        static void Main(string[] args)
        {
        Console.WriteLine("Enter Character you want to print:\n");
       char c = Convert.ToChar(Console.ReadLine());  // curses call to input from keyboard
        Console.WriteLine("Enter row numbers till where to want to see pattern of character:\n");
       int i= Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter column numbers till where to want to see pattern of character:\n");
       int j=Convert.ToInt32(Console.ReadLine());
       //int a =0;
            // For(int a = 0;a<i;a++)
            // {
            //    For(int b=0;b<j;b++)
            // { 
                hello(i,j,c);
            // }
            // }
             
         
                                
        }
    }
}