using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ClassHub
{
    class ClassRepository
    {
        List<Class> ClassList = new List<Class>();
        FileOperations fileOp = new FileOperations();

        internal List<Class> GetClasses()
        {
            if (File.Exists("ClassHub.xml"))
            {
                ClassList = fileOp.LoadFile(ClassList);
                return ClassList;
            }
            else { return ClassList; }
        }

        internal Class FindByID(int id)
        {
            Class ClassById = ClassList.Where(x => x.ClassID == id).ToList().First();
            return ClassById;
        }

        internal Class FindByName(string name)
        {
            Class ClassByName = ClassList.Where(x => x.ClassName == name).ToList().First();
            return ClassByName;
        }

        internal Class CreateClass(string cName)
        {
            Class newClass = new Class(cName,ClassList.Count() + 1);
            return newClass;
        }

        internal bool IfExists(string name)
        {
            bool result = ClassList.Any(x => x.ClassName == name);
            return result;
        }

        internal bool IsEmpty(List<Class> ClassList)
        {
            if(ClassList.Count == 0)
            {
                return true;
            }
            else { return false; }
        }

        internal bool Add (Class newClass)
        {
            ClassList.Add(newClass);
            return true;
        }

        internal bool Remove(Class clasToRemove)
        {                         
            ClassList.Remove(clasToRemove);
            return true;
        }

    }
}
