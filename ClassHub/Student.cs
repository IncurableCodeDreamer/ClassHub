using System;

namespace ClassHub
{
    class Student
    {
        
        public int StudentID { get; private set; }
        public string Name { get; set; }        
        public string Surname { get; set; }        
        public Class SClass { get; set; }

        public Student(int sID, Class cClass, string sName, string sSurname)
        {
            StudentID = sID;            
            Name = sName;
            Surname = sSurname;
            SClass = cClass;
        }               

        public override string ToString()
        {
            string str = String.Format(" Student {2}: {0} {1} from class {3}. ",Name,Surname,StudentID,SClass.ClassName);
            return str;
        }        
    }
}
