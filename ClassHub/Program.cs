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
            int classIdIteration = 0;

            List<Student> StudentList = new List<Student>();
            int studentIdIteration = 0;

            char choice;
            string cName, sName, sSurname, classType;
            int toRemove;
            FileOperations file = new FileOperations();
            ClassRepository cRep = new ClassRepository();
            StudentRepository sRep = new StudentRepository();
            WindowOperations winOp = new WindowOperations();

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
                                  "\n 5 - Remove class." +
                                  "\n 6 - Remove student." +
                                  "\n 7 - Save the file." +
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
                        if (cRep.Add(ClassList, classIdIteration, cName))
                        {
                            classIdIteration = ClassList.Count;
                            winOp.OperationSuccess(ClassList,classIdIteration);
                        }
                        else
                        {
                            winOp.OperationFailed();
                        }                            
                        break;
                    case '2':
                        Console.WriteLine("\n Enter personal data of the student you want to add: ");
                        Console.Write(" Name: ");
                        sName = Console.ReadLine();
                        Console.Write(" Surname: ");
                        sSurname = Console.ReadLine();
                        Console.Write(" Type of the class: ");
                        cName = Console.ReadLine();
                        if (sRep.Add(StudentList, ClassList, sName, sSurname, cName, studentIdIteration))
                        {
                            studentIdIteration = StudentList.Count;
                            winOp.OperationSuccess(StudentList,studentIdIteration);
                        }
                        else { winOp.OperationFailed(); }
                        break;
                    case '3':
                        Console.WriteLine(" Existing classes:");
                        winOp.Show(ClassList, StudentList);
                        Console.Write(" Select the class you want to show: ");
                        classType = Console.ReadLine();
                        var classToShow = StudentList.Where(st => st.SClass.ClassName == classType)
                                                     .ToList();
                        winOp.Show(classToShow);
                        break;
                    case '4':
                        Console.WriteLine("\n Select the student you want to edit: ");
                        winOp.Show(StudentList);
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
                            if (sRep.Edit(StudentList, ClassList, toEdit, toEditClass, toEditName, toEditSurname))
                            {
                                sRep.SaveFile(StudentList, ClassList);
                                winOp.OperationSuccess(StudentList, int.Parse(toEdit));
                            }
                            else { winOp.OperationFailed(); }
                        }
                        else
                        {
                            var load = file.LoadFile(StudentList, ClassList);
                            StudentList = load.Item1;
                            ClassList = load.Item2;                                                 
                            if (sRep.Edit(StudentList, ClassList, toEdit, toEditClass, toEditName, toEditSurname))
                            {
                                sRep.SaveFile(StudentList, ClassList);
                                winOp.OperationSuccess(StudentList, int.Parse(toEdit));
                            }
                            else { winOp.OperationFailed(); }
                        }
                        break;
                    case '5':
                        Console.WriteLine(" Select the class you want to remove.");
                        winOp.Show(ClassList);
                        Console.Write("\n Id: ");
                        toRemove = int.Parse(Console.ReadLine());
                        if (cRep.Remove(ClassList, toRemove))
                        {
                            winOp.OperationSuccess(ClassList,toRemove);
                        }
                        else { winOp.OperationFailed(); }
                        break;
                    case '6':
                        Console.WriteLine(" Select the student you want to remove.");
                        winOp.Show(StudentList);
                        Console.Write("\n Id: ");
                        toRemove = int.Parse(Console.ReadLine());
                        if (sRep.Remove(StudentList, toRemove))
                        {
                            winOp.OperationSuccess(StudentList,toRemove);
                        }
                        else{ winOp.OperationFailed(); }
                        break;
                    case '7':
                        sRep.SaveFile(StudentList, ClassList);
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

