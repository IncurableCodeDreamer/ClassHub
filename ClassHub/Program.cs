using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
            FileOperations file = new FileOperations();
            User user = new User();
            Interface interf = new Interface();

            if (File.Exists("ClassHub.xml"))
            {
                var load = file.LoadFile(StudentList, ClassList);
                StudentList = load.Item1;
                ClassList = load.Item2;
            }
            Console.WriteLine(" Hello.");
            while (true)
            {
                Console.WriteLine("\n Choose what you want to do: " +
                                  "\n 1 - Add a new class. " +
                                  "\n 2 - Add a new student. " +
                                  "\n 3 - Show members of the class. " +
                                  "\n 4 - Edit the student." +
                                  "\n 5 - Save the file." +
                                  "\n q - End the program.");
                Console.Write("\n Your choice: ");
                choice = Char.Parse(Console.ReadLine());

                if (choice == 'q')
                {
                    Console.WriteLine("\n The end of program.");
                    break;
                }
                switch (choice)
                {
                    case '1':
                        Console.Write("\n Enter the type of the class: ");
                        cName = Console.ReadLine();
                        user.AddClass(ClassList, classIdIteration, cName);
                        break;
                    case '2':
                        Console.WriteLine("\n Enter personal data of the student you want to add: ");
                        Console.Write(" Name: ");
                        sName = Console.ReadLine();
                        Console.Write(" Surname: ");
                        sSurname = Console.ReadLine();
                        Console.Write(" Type of the class: ");
                        cName = Console.ReadLine();
                        user.AddStudent(StudentList, ClassList, sName, sSurname, cName, studentIdIteration);
                        break;
                    case '3':
                        Console.WriteLine(" Existing classes:");
                        interf.Show(ClassList, StudentList);
                        Console.Write(" Select the class you want to show: ");
                        classType = Console.ReadLine();
                        var classToShow = StudentList.Where(st => st.SClass.ClassName == classType)
                                                     .ToList();
                        interf.Show(classToShow);
                        break;
                    case '4':
                        Console.WriteLine("\n Select the student you want to edit: ");
                        interf.Show(StudentList);
                        Console.Write("\n Id: ");
                        string toEdit = Console.ReadLine();
                        Console.Write("\n Enter new data:" +
                                      "\n Name: ");
                        string toEditName = Console.ReadLine();
                        Console.Write(" Surname: ");
                        string toEditSurname = Console.ReadLine();
                        Console.Write(" Class: ");
                        string toEditClass = Console.ReadLine();
                        if (!File.Exists("ClassHub.xml"))
                        {
                            user.EditStudent(StudentList, ClassList, toEdit, toEditClass, toEditName, toEditSurname);
                            file.SaveNewFile(StudentList, ClassList);
                        }
                        else
                        {
                            var load = file.LoadFile(StudentList, ClassList);
                            StudentList = load.Item1;
                            ClassList = load.Item2;
                            file.LoadFileToEdit(toEdit, toEditClass, toEditName, toEditSurname, ClassList);
                            user.EditStudent(StudentList, ClassList, toEdit, toEditClass, toEditName, toEditSurname);
                        }
                        break;
                    case '5':
                        file.SaveNewFile(StudentList, ClassList);
                        Console.WriteLine(" The file 'ClassHub.xml' has been saved.");
                        break;
                    default:
                        Console.WriteLine("\n You chose the wrong number. Try again.");
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}

