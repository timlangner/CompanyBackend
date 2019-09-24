using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CompanyAPI.Model;

namespace CompanyAPI.Interface
{
    public interface IBaseInterface<TModel, TModelDto>
    {
        Task<List<TModel>> Read();
        Task<TModel> Read(int id);
        Task<bool> Create(TModelDto data);
        Task<bool> Update(int id, TModelDto data);
        Task<bool> Delete(int id);

    }
}
