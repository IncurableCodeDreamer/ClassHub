using System;

namespace ClassHub
{
    class Class
    {           
        public string ClassName { get; set; }
        public int ClassID { get; set; }
        
        public Class (string cName, int cID)
        {
            this.ClassName = cName;
            this.ClassID = cID;            
        }       

        public override string ToString()
        {
            string str = String.Format(" Class {0} - {1}. \n", ClassID,ClassName);
            return str;
        }
    }
}
