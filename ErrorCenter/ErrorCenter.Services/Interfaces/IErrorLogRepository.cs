using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErrorCenter.Services.Interfaces
{
    public interface IErrorLogRepository<ErrorLogModel>
    {
        IEnumerable<ErrorLogModel> SelectAllWithoutFilter();
        List<ErrorLogModel> SelectAllWithoutDuplicated();

        List<ErrorLogModel> SelectByEnvironment(string whereEnvironment = null);
        List<ErrorLogModel> SelectByEnvironmentOrderedBy(string whereEnvironment = null, string orderby = null);
        List<ErrorLogModel> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null);


        List<ErrorLogModel> SelectOrderedBy(string orderby = null);
        List<ErrorLogModel> SelectOrderedBySearchBy(string orderby = null, string whereSearch = null, string searchText = null);


        List<ErrorLogModel> SelectSearchBy(string whereSearch = null, string searchText = null);
        List<ErrorLogModel> SelectByEnvironmentSearchBy(string whereEnvironment = null, string whereSearch = null, string searchText = null);
        

    }
}
