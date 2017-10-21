using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnItGirlApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Class NewClass = new Class();
            Console.WriteLine("Hello.");
            char choose;
            while(true)
            {                
                Console.WriteLine("\n Choose what you want to do: \n 1 - Add a new class. \n 2 - Add a student to the existing class. \n 3 - Show members of the class. \n q - End the program.");
                Console.Write(" Your choice: ");
                choose = Char.Parse(Console.ReadLine());
                if (choose == 2) { Console.WriteLine(" Enter number of the class you want to modify: "); int number = Int16.Parse(Console.ReadLine()); }
                              
                switch (choose)
                {
                    case '1': NewClass.AddClass(); break;
                    case '2': break;
                    case '3': Console.WriteLine(NewClass.ToString()); break;
                    case 'q': goto Exist;
                    default: Console.WriteLine("\n You chose the wrong number. Try again."); break;
                }
            }

        Exist:
            Console.WriteLine("\n The end of program.");

            Console.ReadKey();
        }
    }
}
