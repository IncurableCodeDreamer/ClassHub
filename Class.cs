using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnItGirlApp
{
    class Class
    {
        int classId=1;

        Dictionary<int,Class> ClassList = new Dictionary<int,Class>();
        List<Student> ClassStudents = new List<Student>();
        
        public void AddClass()
        {
            ClassList.Add(classId, new Class());
            foreach (var item in ClassList.Values)
            {
                item.AddStudent();
                classId++;
            }
        }

        public void AddStudent()
        {
            Console.WriteLine("\n If you want to stop adding students enter: q");
            while (true)
            {
                Console.Write(" Enter student first name: ");                
                string name = Console.ReadLine();
                if (name == "q") { break; }
                Console.Write(" Enter student surname: ");
                string surname = Console.ReadLine();
                ClassStudents.Add(new Student(name, surname));
            }

        }

        public override string ToString()
        {
            string str= "\n";
            foreach (var x in ClassList.Values)                
            {
                str += string.Format(" Students in class: " + x.classId);
                foreach(var z in x.ClassStudents)
                str += string.Format("\n - {0} {1},",z.Name,z.Surname);                
            }
            return str;
        }

    }
}
