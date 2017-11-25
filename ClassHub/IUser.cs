using System.Collections.Generic;

namespace ClassHub
{
    interface IUser
    {
        bool Add(List<Student> StudentList, List<Class> ClassList, string sName, string sSurname, string cName, int studentIdIteration);
        bool Edit(List<Student> StudentList, List<Class> ClassList, string toEdit, string toEditClass, string toEditName, string toEditSurname);
        bool Remove(List<Student> StudentList, int toRemove);    
        void Show ();
    }
}

