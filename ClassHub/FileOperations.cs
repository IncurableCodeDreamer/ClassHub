using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ClassHub
{
    class FileOperations
    {
        XDocument document;

        internal void SaveNewFile(List<Student> st, List<Class> cl)
        {
            var stList = from person in st
                         orderby person.SClass.ClassID, person.StudentID, person.Surname, person.Name
                         select person;
            var clList = from c in cl
                         orderby c.ClassID, c.ClassName
                         select c;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter w = XmlWriter.Create("ClassHub.xml", settings);

            w.WriteStartDocument();
            w.WriteComment("List of students and classes.");
            w.WriteStartElement("StudentsClasses");
            foreach (var s in stList)
            {
                w.WriteStartElement("Student");
                w.WriteAttributeString("StudentID", Convert.ToString(s.StudentID));
                w.WriteAttributeString("ClassID", Convert.ToString(s.SClass.ClassID));
                w.WriteElementString("Name", s.Name);
                w.WriteElementString("Surname", s.Surname);
                w.WriteEndElement();
            }
            foreach (var c in clList)
            {
                w.WriteStartElement("Class");
                w.WriteAttributeString("ClassID", Convert.ToString(c.ClassID));
                w.WriteElementString("Name", c.ClassName);
                w.WriteEndElement();
            }
            w.WriteEndElement();
            w.WriteEndDocument();
            w.Flush();
            w.Close();
        }

        internal void LoadFileToEdit(string toEdit, string toEditClass, string toEditName, string toEditSurname, List<Class> ClassList)
        {
            document = XDocument.Load("ClassHub.xml");
            var editStudent = document.Root.Elements("Student")
                                           .Where(s => s.Attribute("StudentID")
                                           .Value == toEdit);
            if (editStudent.Any())
            {
                editStudent.First().Element("Name").Value = toEditName;
                editStudent.First().Element("Surname").Value = toEditSurname;
                editStudent.First().Attribute("ClassID").Value = ClassList.Where(cl => cl.ClassName == toEditClass)
                                                        .Select(cl => Convert.ToString(cl.ClassID))
                                                        .ToList()
                                                        .First();
            }
            document.Save("ClassHub.xml");
        }

        internal Tuple<List<Student>, List<Class>> LoadFile(List<Student> StudentList, List<Class> ClassList)
        {
            document = XDocument.Load("ClassHub.xml");
            ClassList = (
                  from cl in document.Root.Elements("Class")
                  select new Class(
                      cl.Element("Name").Value,
                      int.Parse(cl.Attribute("ClassID").Value)))
                      .ToList();
            StudentList = (
                 from st in document.Root.Elements("Student")
                 select new Student(
                     int.Parse(st.Attribute("StudentID").Value),
                     ClassList.Where(cl => cl.ClassID == int.Parse(st.FirstAttribute.Value))
                     .ToList().First(),
                     st.Element("Name").Value,
                     st.Element("Surname").Value))
                     .ToList();
            return Tuple.Create(StudentList, ClassList);
        }
    }
}
