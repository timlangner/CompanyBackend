using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyAPI.Interface
{
    public interface IBaseInterface<TModel, in TModelDto>
    {
        Task<List<TModel>> Read();
        Task<TModel> Read(int id);
        Task<bool> Create(TModelDto data);
        Task<bool> Update(int id, TModelDto data);
        Task<bool> Delete(int id);

    }
}
