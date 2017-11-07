using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
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
            XDocument document;

            if (File.Exists("ClassHub.xml"))
            {
                document = XDocument.Load("ClassHub.xml");
                ClassList = (
                    from cl in document.Root.Elements("Class")
                    select new Class(
                        cl.Element("Name").Value,
                        int.Parse(cl.Attribute("ClassID").Value)))
                        .ToList();
                StudentList = (
                    from st in document.Root.Elements("Student")
                    select new Student(
                        int.Parse(st.Attribute("StudentID").Value),
                        ClassList.Where(cl => cl.ClassID == int.Parse(st.FirstAttribute.Value))
                        .ToList().First(),
                        st.Element("Name").Value,
                        st.Element("Surname").Value))
                        .ToList();
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
                        if (!ClassList.Exists(cl => cl.ClassName == cName))
                        {
                            classIdIteration = ClassList.Count() + 1;
                            ClassList.Add(new Class(cName, classIdIteration));
                            Console.WriteLine(ClassList[classIdIteration - 1].ToString());
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
                        Class StudentClass = ClassList.Where(cl => cl.ClassName == cName)
                                                      .Distinct()
                                                      .First();
                        studentIdIteration = StudentList.Count() + 1;
                        StudentList.Add(new Student(studentIdIteration, StudentClass, sName, sSurname));
                        Console.WriteLine(StudentList[studentIdIteration - 1].ToString());
                        break;
                    case '3':
                        Console.WriteLine(" Existing classes:");
                        foreach (var x in ClassList)
                        {
                            var list = StudentList.Where(st => st.SClass.ClassName == x.ClassName)
                                                  .ToList();
                            int count = list.Count();
                            {
                                Console.WriteLine(" {0} has {1} student(s).", x.ClassName, count);
                            }
                        }
                        Console.Write(" Select the class you want to show: ");
                        classType = Console.ReadLine();
                        var classToShow = StudentList.Where(st => st.SClass.ClassName == classType)
                                                     .ToList();
                        foreach (Student s in classToShow)
                        {
                            Console.WriteLine(s.ToString());
                        }
                        break;
                    case '4':
                        Console.WriteLine("\n Select the student you want to edit: ");
                        foreach (Student s in StudentList)
                        {
                            Console.WriteLine(s.ToString());
                        }
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
                            Student studentToEdit = StudentList.Where(st => st.StudentID == int.Parse(toEdit))
                                                           .ToList()
                                                           .First();
                            studentToEdit.Name = toEditName;
                            studentToEdit.Surname = toEditSurname;
                            studentToEdit.SClass.ClassName = ClassList.Where(cl => cl.ClassID == int.Parse(toEditClass))
                                                                      .Select(cl => cl.ClassName)
                                                                      .ToList()
                                                                      .First();
                            studentToEdit.SClass.ClassID = int.Parse(toEditClass);
                            

                            var stList = from person in StudentList
                                         orderby person.SClass.ClassID, person.StudentID, person.Surname, person.Name
                                         select person;
                            var clList = from cl in ClassList
                                         orderby cl.ClassID, cl.ClassName
                                         select cl;
                            
                            XmlWriterSettings settings = new XmlWriterSettings();
                            settings.Indent = true;

                            XmlWriter w = XmlWriter.Create("ClassHub.xml", settings);

                            w.WriteStartDocument();
                            w.WriteComment("List of students and classes.");
                            w.WriteStartElement("StudentsClasses");
                            foreach (var st in stList)
                            {
                                w.WriteStartElement("Student");
                                w.WriteAttributeString("StudentID", Convert.ToString(st.StudentID));
                                w.WriteAttributeString("ClassID", Convert.ToString(st.SClass.ClassID));
                                w.WriteElementString("Name", st.Name);
                                w.WriteElementString("Surname", st.Surname);
                                w.WriteEndElement();
                            }
                            foreach (var cl in clList)
                            {
                                w.WriteStartElement("Class");
                                w.WriteAttributeString("ClassID", Convert.ToString(cl.ClassID));
                                w.WriteElementString("Name", cl.ClassName);
                                w.WriteEndElement();
                            }
                            w.WriteEndElement();
                            w.WriteEndDocument();
                            w.Flush();
                            w.Close();                        
                        }
                        else
                        {
                            document = XDocument.Load("ClassHub.xml");
                            var editStudent = document.Root.Elements("Student")
                                                           .Where(s => s.Attribute("StudentID")
                                                           .Value == toEdit);
                            if (editStudent.Any())
                            {
                                editStudent.First().Element("Name").Value = toEditName;
                                editStudent.First().Element("Surname").Value = toEditSurname;
                                editStudent.First().Attribute("ClassID").Value = toEditClass;
                            }
                            document.Save("ClassHub.xml");

                            Student studentToEdit = StudentList.Where(st => st.StudentID == int.Parse(toEdit))
                                                           .ToList()
                                                           .First();
                            studentToEdit.Name = toEditName;
                            studentToEdit.Surname = toEditSurname;
                            studentToEdit.SClass.ClassName = ClassList.Where(cl => cl.ClassID == int.Parse(toEditClass))
                                                                      .Select(cl => cl.ClassName)
                                                                      .ToList()
                                                                      .First();
                            studentToEdit.SClass.ClassID = int.Parse(toEditClass);                           
                        }
                        break;
                    case '5':
                        if (!File.Exists("ClassHub.xml"))
                        {  
                            var stList = from person in StudentList
                                         orderby person.SClass.ClassID, person.StudentID, person.Surname, person.Name
                                         select person;
                            var clList = from cl in ClassList
                                         orderby cl.ClassID, cl.ClassName
                                         select cl;

                            XmlWriterSettings settings = new XmlWriterSettings();
                            settings.Indent = true;

                            XmlWriter w = XmlWriter.Create("ClassHub.xml",settings);                                                  

                            w.WriteStartDocument();
                            w.WriteComment("List of students and classes.");                            
                            w.WriteStartElement("StudentsClasses");
                            foreach (var st in stList)
                            {
                                w.WriteStartElement("Student");
                                w.WriteAttributeString("StudentID",Convert.ToString(st.StudentID));
                                w.WriteAttributeString("ClassID",Convert.ToString(st.SClass.ClassID));
                                w.WriteElementString("Name",st.Name);
                                w.WriteElementString("Surname",st.Surname);
                                w.WriteEndElement();
                            }
                            foreach(var cl in clList)
                            {
                                w.WriteStartElement("Class");
                                w.WriteAttributeString("ClassID",Convert.ToString(cl.ClassID));
                                w.WriteElementString("Name",cl.ClassName);
                                w.WriteEndElement();
                            }
                            w.WriteEndElement();
                            w.WriteEndDocument();
                            w.Flush();
                            w.Close();
                        }
                        else
                        {
                            document = XDocument.Load("ClassHub.xml");
                            var studentsFromXml = document.Root.Elements("Student")
                                                          .Select(s => s.Attribute("StudentID").Value)
                                                          .ToList();
                            var classesFromXml = document.Root.Elements("Class")
                                                         .Select(s => s.Attribute("ClassID").Value)
                                                         .ToList();

                            IEnumerable<Student> newStudents = new List<Student>();
                            IEnumerable<Class> newClasses = new List<Class>();
                              
                            newStudents = StudentList.SkipWhile(x => studentsFromXml
                                                     .Contains(Convert.ToString(x.StudentID)))
                                                     .ToList();                           
                            newClasses = ClassList.SkipWhile(x => classesFromXml
                                                  .Contains(Convert.ToString(x.ClassID)))
                                                  .ToList();
                            
                            XmlDocument doc = new XmlDocument();
                            doc.Load("ClassHub.xml");

                            if (newStudents.Any() | newClasses.Any())
                            {
                                using (XmlWriter w = doc.CreateNavigator().AppendChild())
                                {
                                    //w.WriteStartDocument();
                                    w.WriteStartElement("StudentsClasses");
                                    if (newStudents.Any())
                                    {
                                        foreach (var st in newStudents)
                                        {
                                            w.WriteStartElement("Student");
                                            w.WriteAttributeString("StudentID", Convert.ToString(st.StudentID));
                                            w.WriteAttributeString("ClassID", Convert.ToString(st.SClass.ClassID));
                                            w.WriteElementString("Name", st.Name);
                                            w.WriteElementString("Surname", st.Surname);
                                            w.WriteEndElement();
                                        }
                                    }
                                    if (newClasses.Any())
                                    {
                                        foreach (var cl in newClasses)
                                        {
                                            w.WriteStartElement("Class");
                                            w.WriteAttributeString("ClassID", Convert.ToString(cl.ClassID));
                                            w.WriteElementString("Name", cl.ClassName);
                                            w.WriteEndElement();
                                        }
                                    }
                                    w.WriteEndElement();
                                    //w.WriteEndDocument();
                                    w.Flush();
                                    w.Close(); //blad 
                                    doc.Save("ClassHub.xml");
                                }
                            }
                        }
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
