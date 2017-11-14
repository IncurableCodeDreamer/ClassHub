using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassHub
{
    class Interface
    {
        internal void Show<T>(List<T> ListName)
        {
            foreach (var x in ListName)
            {
                Console.WriteLine(x.ToString());
            }
        }

        internal void Show(List<Class> ClassList, List<Student> StudentList)
        {

            foreach (var x in ClassList)
            {
                var list = StudentList.Where(st => st.SClass.ClassName == x.ClassName)
                                      .ToList();
                int count = list.Count();
                {
                    Console.WriteLine(" {0} has {1} student(s).", x.ClassName, count);
                }
            }
        }
    }
}
