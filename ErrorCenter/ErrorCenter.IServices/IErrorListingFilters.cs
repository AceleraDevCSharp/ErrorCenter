using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ErrorCenter.IServices
{
    public interface IErrorListingFilters<TDomain>
        where TDomain : class
    {
        List<TDomain> SelectAllWithoutFilter();
        List<TDomain> SelectAllWithoutDuplicated();

        List<TDomain> SelectByEnvironmentOrderedBySearchby(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null);
        

    }
}
