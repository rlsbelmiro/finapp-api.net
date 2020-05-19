using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Repositories
{
    public interface IReposity<T,L,E>
    {
        E Insert(E obj);
        E Update(E obj);
        E Get(int id);

        void Delete(int id);
        IEnumerable<L> GetByClientId(int clientId);
        IEnumerable<L> List();

        E EntityToViewModel(T obj);

        T ViewModelToEntity(E obj);

        L EntityToViewModelList(T obj);
    }
}
