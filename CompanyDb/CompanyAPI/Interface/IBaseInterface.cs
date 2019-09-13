using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Model;

namespace CompanyAPI.Interface
{
    public interface IBaseInterface<TModel, TModelDto>
    {
        List<TModel> Read();
        TModel Read(int id);
        bool Create(TModelDto data);
        bool Update(int id, TModelDto data);
        bool Delete(int id);

    }
}
