using System.Collections.Generic;
using System.Linq;

namespace ClassHub
{
    class ClassRepository
    {
        //List<Class> ClassList = new List<Class>();
        //public Class Item {get { return ClassList.LastOrDefault(); } set { ClassList.Add(value); } }
        
        //public List<Class> ClassList { get; private set; }
        //public List<Class> Content { get; private set; }
        //int classIdIteration = 0;

        internal bool Add(List<Class> ClassList,int classIdIteration, string cName)
        {
            if (!ClassList.Exists(cl => cl.ClassName == cName))
            {
                classIdIteration = ClassList.Count() + 1;
                ClassList.Add(new Class(cName, classIdIteration));
                return true;
            }
            else { return false; }
        }

        internal bool Remove(List<Class>ClassList, int toRemove)
        {
            Class classToRemove = ClassList.Where(cl => cl.ClassID == toRemove)
                                                 .ToList()
                                                 .First();
            ClassList.Remove(classToRemove);
            return true;
        }

    }
}
