using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.Repositories;

namespace ErrorCenter.Persistence.EF.Repository
{
    public class ErrorLogRepository : IErrorLogRepository<ErrorLog>
    {
        protected ErrorCenterDbContext _context;

        public ErrorLogRepository(ErrorCenterDbContext context)
        {
            _context = context;
            UpdateQuantityEventsErrorLogs();
        }

        public async Task<IEnumerable<ErrorLog>> SelectAll()
        {
            return await _context.ErrorLogs
                .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
        public async Task<IEnumerable<ErrorLog>> SelectArquived()
        {
            return await _context.ErrorLogs
                .Where(x => x.ArquivedAt != null)
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<ErrorLog>> SelectDeleted()
        {
            return await _context.ErrorLogs
                .Where(x => x.DeletedAt != null)
                .Include(x => x.User)
                .ToListAsync();
        }


        public async Task<IEnumerable<ErrorLog>> SelectByEnvironment(string whereEnvironment)
        {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Equals(whereEnvironment) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBy(string whereEnvironment = null, string orderby = null)
        {
            switch (orderby)
            {
                case "Level":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Equals(whereEnvironment) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.Level)
                        .ToListAsync();
                case "Frequencia":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Equals(whereEnvironment) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.Quantity)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.DeletedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null)
        {
            switch (orderby)
            {
                case "Level":
                    if (whereSearch.Equals("Level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    }
                    else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderByDescending(x => x.CreatedAt)
                            .ToListAsync();
                    }
                case "Frequencia":
                    if (whereSearch.Equals("Level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    }
                    else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderByDescending(x => x.CreatedAt)
                            .ToListAsync();
                    }
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentSearchBy(string whereEnvironment = null, string whereSearch = null, string searchText = null)
        {
            switch (whereSearch)
            {
                case "Level":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .ToListAsync();
                case "Descricao":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .ToListAsync();
                case "Origem":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<ErrorLog>> SelectOrderedBy(string orderby = null)
        {
            switch (orderby)
            {
                case "Level":
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderBy(x => x.Level)
                        .ToListAsync();
                case "Frequencia":
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderBy(x => x.Quantity)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectOrderedBySearchBy(string orderby = null, string whereSearch = null, string searchText = null)
        {
            switch (orderby)
            {
                case "Level":
                    if (whereSearch.Equals("Level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    }
                    else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .OrderByDescending(x => x.CreatedAt)
                            .Include(x => x.User)
                            .ToListAsync();
                    }
                case "Frequencia":
                    if (whereSearch.Equals("Level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Descricao"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    }
                    else if (whereSearch.Equals("Origem"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    }
                    else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .OrderByDescending(x => x.CreatedAt)
                            .ToListAsync();
                    }
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }



        public async Task<IEnumerable<ErrorLog>> SelectSearchBy(string whereSearch = null, string searchText = null)
        {
            switch (whereSearch)
            {
                case "Level":
                    return await _context.ErrorLogs
                        .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .ToListAsync();
                case "Descricao":
                    return await _context.ErrorLogs
                        .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .ToListAsync();
                case "Origem":
                    return await _context.ErrorLogs
                        .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .OrderByDescending(x => x.CreatedAt)
                        .Include(x => x.User)
                        .ToListAsync();
            }
        }



        private void UpdateQuantityEventsErrorLogs()
        {
            foreach (var error in _context.ErrorLogs.ToList())
            {
                int aux = _context.ErrorLogs.Count(x => x.Title.Equals(error.Title));
                error.Quantity = aux;
                aux = 0;
            }

            _context.SaveChanges();
        }
    }
}
