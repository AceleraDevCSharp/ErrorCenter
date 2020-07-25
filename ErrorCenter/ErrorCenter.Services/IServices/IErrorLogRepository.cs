using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices
{
    public interface IErrorLogRepository<TDomain>
    {
        Task<IEnumerable<Environment>> Environments();
        Task<IEnumerable<TDomain>> SelectAll();

        Task<IEnumerable<TDomain>> SelectByEnvironment(string whereEnvironment = null);
        Task<IEnumerable<TDomain>> SelectByEnvironmentOrderedBy(string whereEnvironment = null, string orderby = null);
        Task<IEnumerable<TDomain>> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null);


        Task<IEnumerable<TDomain>> SelectOrderedBy(string orderby = null);
        Task<IEnumerable<TDomain>> SelectOrderedBySearchBy(string orderby = null, string whereSearch = null, string searchText = null);


        Task<IEnumerable<TDomain>> SelectSearchBy(string whereSearch = null, string searchText = null);
        Task<IEnumerable<TDomain>> SelectByEnvironmentSearchBy(string whereEnvironment = null, string whereSearch = null, string searchText = null);

        Task<IEnumerable<TDomain>> SelectArchived();

        Task<IEnumerable<TDomain>> SelectDeleted();

        Task<TDomain> FindById(int id);
        Task<TDomain> UpdateErrorLog(TDomain errorLog);

        Task<TDomain> Create(TDomain errorLog);
    }
}
