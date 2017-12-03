using System.Collections.Generic;

namespace ClassHub
{
    interface IWindowOperations
    {
        void Show<T>(List<T> ListName);
        void Show(List<Class> ClassList, List<Student> StudentList);
        void OperationSuccess<T>(T Name);
        void OperationFailed();
    }
}

