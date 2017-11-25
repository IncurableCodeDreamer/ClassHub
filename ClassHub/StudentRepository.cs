using System.Collections.Generic;
using System.Linq;

namespace ClassHub
{
    class StudentRepository
    { 
        //List<Student> StudentList = new List<Student>();
        //int studentIdIteration = 0;
        FileOperations file = new FileOperations();

        internal bool Add(List<Student> StudentList, List<Class> ClassList, string sName, string sSurname, string cName, int studentIdIteration)
        {
            Class StudentClass = ClassList.Where(cl => cl.ClassName == cName)
                                                      .Distinct()
                                                      .First();
            studentIdIteration = StudentList.Count() + 1;
            StudentList.Add(new Student(studentIdIteration, StudentClass, sName, sSurname));
            return true;
        }

        internal bool Edit(List<Student> StudentList, List<Class> ClassList, string toEdit, string toEditClass, string toEditName, string toEditSurname)
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
            return true;
        }

        internal bool Remove(List<Student>StudentList, int toRemove)
        {
            Student studentToRemove = StudentList.Where(st => st.StudentID == toRemove)
                                                 .ToList()
                                                 .First();
            StudentList.Remove(studentToRemove);
            return true;
        }

        internal void SaveFile(List<Student>StudentList,List<Class>ClassList)
        {
            file.SaveNewFile(StudentList, ClassList);
        }

    }
}
