using ErrorCenter.Domain;
using ErrorCenter.IServices;
using ErrorCenter.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ErrorCenter.Services
{
    public class ErrorListingFilters : IErrorListingFilters<ErrorLog, string, string, string, string>
    {
        protected DbContext _context;

        public ErrorListingFilters(ErrorCenterDbContext context)
        {
            _context = context;
        }

        public List<ErrorLog> SelectAll()
        {
            return _context.Set<ErrorLog>().ToList();
        }

        public List<ErrorLog> SelectAllOrderedBy(string order)
        {
            throw new System.NotImplementedException();
        }

        public List<ErrorLog> SelectByEnvironment(string environment)
        {
            throw new System.NotImplementedException();
        }

        public List<ErrorLog> SelectByEnvironmentOrderedBy(string environment, string order)
        {
            throw new System.NotImplementedException();
        }

        public List<ErrorLog> SelectSearchBy(string typeSearch, string search)
        {
            throw new System.NotImplementedException();
        }

        public List<ErrorLog> SelectSearchByOrderBy(string typeSearch, string search, string order)
        {
            throw new System.NotImplementedException();
        }
    }
}
