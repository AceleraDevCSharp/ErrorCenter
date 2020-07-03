using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorCenter.IServices
{
    public interface IErrorListingFilters<TDomain, TEnvironment, TOrder, TTypeSearch, TSearch>
        where TDomain : class
    {
        List<TDomain> SelectAll();
        List<TDomain> SelectAllOrderedBy(TOrder order);

        List<TDomain> SelectByEnvironment(TEnvironment environment);
        List<TDomain> SelectByEnvironmentOrderedBy(TEnvironment environment, TOrder order);

        List<TDomain> SelectSearchBy (TTypeSearch typeSearch, TSearch search);
        List<TDomain> SelectSearchByOrderBy (TTypeSearch typeSearch, TSearch search, TOrder order);
    }
}
