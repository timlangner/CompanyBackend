using System;
using System.Collections.Generic;
using System.Text;
using CompanyApp.Model;

namespace CompanyApp.Interface
{
    public interface IBaseInterface<T>
    {
        List<T> Read();
        T Read(int id);
        T Create(T data);
        T Update(T data);
        bool Delete(int id);

    }
}
