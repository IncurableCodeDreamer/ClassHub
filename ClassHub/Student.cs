using System;

namespace ClassHub
{
    class Student
    {
        public int StudentID { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string ClassName { get; private set; }
        //public Class SClass { get { return sClass.ClassName,sClass.ClassID; } private set; }
        Class sClass;

        public Student(int sID, string cName, int cID, string sName, string sSurname)
        {
            this.StudentID = sID;
            this.sClass = new Class(cName, cID);
            this.Name = sName;
            this.Surname = sSurname;
            this.ClassName = this.sClass.ClassName;
            //this.SClass.ClassName = sClass.ClassName;
            //this.SClass.ClassID = sClass.ClassID;
        }               

        public override string ToString()
        {
            string str = String.Format(" Student {2}: {0} {1} from class {3}. ",Name,Surname,StudentID,ClassName);
            return str;
        }        
    }
}
