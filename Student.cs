using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnItGirlApp
{
    class Student
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }

        public Student (string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }
    }
}
