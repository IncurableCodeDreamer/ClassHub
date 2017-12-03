using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ClassHub
{
    class StudentRepository
    { 
        List<Student> StudentList = new List<Student>();
        FileOperations fileOp = new FileOperations();

        internal List<Student> GetStudents(List<Class>ClassList)
        {
            if (File.Exists("ClassHub.xml"))
            {
                StudentList = fileOp.LoadFile(StudentList,ClassList);
                return StudentList;
            }
            else { return null; }
        }

        internal Student FindByID (int id)
        {
            Student StudentById = StudentList.Where(x => x.StudentID == id).ToList().First();
            return StudentById;
        }

        internal List<Student> FindByClassName(string className)
        {
            List<Student> StudentListByClass = StudentList.Where(x => x.SClass.ClassName == className).ToList();
            return StudentListByClass;
        }

        internal Student CreateNewStudent(string sName, string sSurname, Class StudentClass)
        {
           Student newStudent = new Student(StudentList.Count() + 1, StudentClass, sName, sSurname);
           return newStudent;
        }

        internal Student CreateStudent (int id, string sName, string sSurname, Class StudentClass)
        {
            Student student = new Student(id, StudentClass, sName, sSurname);
            return student;
        }

        internal bool Add(Student newStudent)
        {
            StudentList.Add(newStudent);
            return true;
        }

        internal bool Edit(Student oldStudent, Student newStudent)
        {                                           
            StudentList.Find(x => x == oldStudent).Name = newStudent.Name;
            StudentList.Find(x => x == oldStudent).SClass = newStudent.SClass;
            StudentList.Find(x => x == oldStudent).Surname = newStudent.Surname;
            return true;
        }

        internal bool Remove(Student studentToRemove)
        {
            StudentList.Remove(studentToRemove);
            return true;
        }

        internal void SaveFile(List<Student>StudentList,List<Class>ClassList)
        {
            fileOp.SaveNewFile(StudentList, ClassList);
        }
    }
}
