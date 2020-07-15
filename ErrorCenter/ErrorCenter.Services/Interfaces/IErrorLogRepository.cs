using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErrorCenter.Services.Interfaces
{
    public interface IErrorLogRepository<TDomain>
    {
        Task<IEnumerable<TDomain>> SelectAll();

        Task<IEnumerable<TDomain>> SelectByEnvironment(string whereEnvironment = null);
        Task<IEnumerable<TDomain>> SelectByEnvironmentOrderedBy(string whereEnvironment = null, string orderby = null);
        Task<IEnumerable<TDomain>> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null);


        Task<IEnumerable<TDomain>> SelectOrderedBy(string orderby = null);
        Task<IEnumerable<TDomain>> SelectOrderedBySearchBy(string orderby = null, string whereSearch = null, string searchText = null);


        Task<IEnumerable<TDomain>> SelectSearchBy(string whereSearch = null, string searchText = null);
        Task<IEnumerable<TDomain>> SelectByEnvironmentSearchBy(string whereEnvironment = null, string whereSearch = null, string searchText = null);

        Task<IEnumerable<TDomain>> SelectArquived();

        Task<IEnumerable<TDomain>> SelectDeleted();
    }
}
