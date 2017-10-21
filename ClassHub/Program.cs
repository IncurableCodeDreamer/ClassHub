using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassHub
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Class> ClassList = new List<Class>();
            List<Student> StudentList = new List<Student>();            
            char choice;
            int classIdIteration = 0;
            int studentIdIteration = 0;            
            string cName, sName, sSurname, classType;

            Console.WriteLine("Hello.");           
            while (true)
            {
                Console.WriteLine("\n Choose what you want to do: " +
                                  "\n 1 - Add a new class. " +
                                  "\n 2 - Add a new student. " +
                                  "\n 3 - Show members of the class. " +
                                  "\n q - End the program.");
                Console.Write("\n Your choice: ");
                choice = Char.Parse(Console.ReadLine());
                
                if(choice == 'q')
                {
                    Console.WriteLine("\n The end of program.");
                    break;
                }

                switch (choice)
                {
                    case '1':
                        Console.Write("\n Enter the type of the class: ");
                        cName = Console.ReadLine();
                        if (!ClassList.Exists(cl => cl.ClassName == cName))
                        {
                            classIdIteration += 1;
                            ClassList.Add(new Class(cName, classIdIteration));
                            Console.WriteLine(ClassList[classIdIteration-1].ToString());
                        }
                        else { Console.WriteLine(" This class already exists."); }                        
                        break;
                    case '2':
                        Console.WriteLine("\n Enter personal data of the student you want to add: ");
                        Console.Write(" Name: ");  
                        sName = Console.ReadLine();
                        Console.Write(" Surname: ");
                        sSurname = Console.ReadLine();
                        Console.Write(" Type of the class: ");                        
                        cName = Console.ReadLine();
                        List<Class> StudentClass = ClassList.Where(cl => cl.ClassName == cName)
                                                             .Distinct()                                                             
                                                             .ToList();                            
                        studentIdIteration += 1;                                             
                        StudentList.Add(new Student(studentIdIteration, StudentClass[0].ClassName, StudentClass[0].ClassID, sName, sSurname));                        
                        Console.WriteLine(StudentList[studentIdIteration-1].ToString());
                        break;
                    case '3':
                        Console.WriteLine(" Existing classes:");
                        foreach(var x in ClassList)
                        {
                            var list = StudentList.Where(st => st.ClassName == x.ClassName)
                                                  .ToList();
                            int count = list.Count();
                            {
                                Console.WriteLine(" {0} has {1} student(s).",x.ClassName,count);
                            }
                        }                        
                        Console.Write(" Select the class you want to show: ");
                        classType = Console.ReadLine(); 
                        var classToShow = StudentList.Where(st => st.ClassName == classType)                                                     
                                                     .ToList();
                        foreach (Student s in classToShow)
                        {
                            Console.WriteLine(s.ToString());
                        }
                        break;                    
                    default: Console.WriteLine("\n You chose the wrong number. Try again.");
                        break;
                }           
            } 
            Console.ReadKey();
        }
    }
}
