using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassHub
{
    class User
    {
        internal void AddStudent(List<Student> StudentList, List<Class> ClassList, string sName, string sSurname, string cName, int studentIdIteration)
        {
            Class StudentClass = ClassList.Where(cl => cl.ClassName == cName)
                                                      .Distinct()
                                                      .First();
            studentIdIteration = StudentList.Count() + 1;
            StudentList.Add(new Student(studentIdIteration, StudentClass, sName, sSurname));
            Console.WriteLine(StudentList[studentIdIteration - 1].ToString());
        }

        internal void AddClass(List<Class> ClassList, int classIdIteration, string cName)
        {
            if (!ClassList.Exists(cl => cl.ClassName == cName))
            {
                classIdIteration = ClassList.Count() + 1;
                ClassList.Add(new Class(cName, classIdIteration));
                Console.WriteLine(ClassList[classIdIteration - 1].ToString());
            }
            else { Console.WriteLine(" This class already exists."); }
        }

        internal void EditStudent(List<Student> StudentList, List<Class> ClassList, string toEdit, string toEditClass, string toEditName, string toEditSurname)
        {
            Student studentToEdit = StudentList.Where(st => st.StudentID == int.Parse(toEdit))
                                                           .ToList()
                                                           .First();
            studentToEdit.Name = toEditName;
            studentToEdit.Surname = toEditSurname;
            studentToEdit.SClass.ClassName = toEditClass;
            studentToEdit.SClass.ClassID = ClassList.Where(cl => cl.ClassName == toEditClass)
                                                    .Select(cl => cl.ClassID)
                                                    .ToList()
                                                    .First();
        }
    }
}
