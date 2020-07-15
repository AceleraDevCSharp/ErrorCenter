using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErrorCenter.Persistence.EF.IRepository
{
    public interface IErrorLogRepository<TDomain>
    {
        /* alessandro */
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

        /* bernardo */
        public Task<TDomain> Create(TDomain errorLog);
        public Task<TDomain> FindById(int id);
        public Task<TDomain> UpdateErrorLog(TDomain errorLog);
    }
}
