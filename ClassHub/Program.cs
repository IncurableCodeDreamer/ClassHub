using System;

namespace ClassHub
{
    class Program
    {
        static void Main(string[] args)
        {
            char choice;            
            ClassRepository classRep = new ClassRepository();
            StudentRepository studentRep = new StudentRepository();
            IWindowOperations windowOp = new ConsoleOperations();
            var ClassList = classRep.GetClasses();
            var StudentList = studentRep.GetStudents(ClassList);

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
                        string cName = Console.ReadLine();
                        bool ifExists = classRep.IfExists(cName);   
                        if(!ifExists)
                        {
                            Class newClass = classRep.CreateClass(cName);
                            classRep.Add(newClass);
                            windowOp.OperationSuccess(newClass);
                        }
                        else { windowOp.OperationFailed(); }                      
                        break;
                    case '2':
                        Console.WriteLine("\n Enter personal data of the student you want to add: ");
                        Console.Write(" Name: ");
                        string sName = Console.ReadLine();
                        Console.Write(" Surname: ");
                        string sSurname = Console.ReadLine();
                        Console.Write(" Type of the class: ");
                        cName = Console.ReadLine();
                        Student newStudent = studentRep.CreateNewStudent(sName, sSurname, classRep.FindByName(cName));
                        if (studentRep.Add(newStudent)) 
                        {                           
                            windowOp.OperationSuccess(newStudent);
                        }
                        else { windowOp.OperationFailed(); }
                        break;
                    case '3':
                        Console.WriteLine(" Existing classes:");
                        windowOp.Show(ClassList, StudentList);
                        Console.Write(" Select the class you want to show: ");
                        string classType = Console.ReadLine();
                        var classToShow = studentRep.FindByClassName(classType);                                                  
                        windowOp.Show(classToShow);
                        break;
                    case '4':
                        Console.WriteLine("\n Select the student you want to edit: ");
                        windowOp.Show(StudentList);
                        Console.Write("\n Id: ");
                        int toEdit = int.Parse(Console.ReadLine());
                        Console.Write("\n Enter new data:" +
                                      "\n Name: ");
                        string toEditName = Console.ReadLine();
                        Console.Write(" Surname: ");
                        string toEditSurname = Console.ReadLine();
                        Console.Write(" Class: ");
                        string toEditClass = Console.ReadLine();                        
                        Student studentToEdit = studentRep.FindByID(toEdit);
                        Student student = studentRep.CreateStudent(studentToEdit.StudentID, toEditName, toEditSurname, classRep.FindByName(toEditClass));
                        if (studentRep.Edit(studentToEdit,student))
                        {
                            windowOp.OperationSuccess(student);
                        }
                        else { windowOp.OperationFailed(); }                        
                        break;
                    case '5':
                        Console.WriteLine(" Select the class you want to remove.");
                        windowOp.Show(ClassList);
                        Console.Write("\n Id: ");
                        int toRemove = int.Parse(Console.ReadLine());
                        Class classToRemove = classRep.FindByID(toRemove);
                        if (classRep.Remove(classToRemove))
                        {
                            windowOp.OperationSuccess(classToRemove);
                        }
                        else { windowOp.OperationFailed(); }
                        break;
                    case '6':
                        Console.WriteLine(" Select the student you want to remove.");
                        windowOp.Show(StudentList);
                        Console.Write("\n Id: ");
                        toRemove = int.Parse(Console.ReadLine());
                        Student studentToRemove = studentRep.FindByID(toRemove);
                        if (studentRep.Remove(studentToRemove))
                        {
                            windowOp.OperationSuccess(studentToRemove);
                        }
                        else { windowOp.OperationFailed(); }
                        break;
                    case '7':
                        studentRep.SaveFile(StudentList, ClassList);
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

