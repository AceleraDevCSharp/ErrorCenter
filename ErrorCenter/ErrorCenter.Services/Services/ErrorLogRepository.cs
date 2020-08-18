using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;

namespace ErrorCenter.Services.Services
{
    public class ErrorLogRepository : IErrorLogRepository<ErrorLog>
    {
        protected ErrorCenterDbContext _context;

        public ErrorLogRepository(ErrorCenterDbContext context)
        {
            _context = context;
            UpdateQuantityEventsErrorLogs();
        }

        public async Task<ErrorLog> FindById(int id)
        {
            var errorLog = await _context.ErrorLogs
              .Include(x => x.Environment)
              .Include(x => x.User)
              .Where(x => x.Id == id)
              .FirstOrDefaultAsync();

            return errorLog;
        }

        public async Task<IEnumerable<Environment>> Environments()
        {
            var environments = await _context.Roles.AsNoTracking().ToListAsync();
            return environments;
        }

        public async Task<IEnumerable<ErrorLog>> SelectAll()
        {
            return await _context.ErrorLogs
                .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.Environment)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ErrorLog>> SelectArchived()
        {
            return await _context.ErrorLogs
                .Where(x => x.ArquivedAt != null)
                .Include(x => x.Environment)
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<ErrorLog>> SelectDeleted()
        {
            return await _context.ErrorLogs
                .Where(x => x.DeletedAt != null)
                .Include(x => x.Environment)
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<ErrorLog>> SelectByEnvironment(string whereEnvironment)
        {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.Environment)
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBy(string whereEnvironment, string orderby)
        {
            switch (orderby.ToLower())
            {
                case "level":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderByDescending(x => x.Level)
                        .ToListAsync();
                case "frequencia":
                case "quantity":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderByDescending(x => x.Quantity)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderByDescending(x => x.DeletedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment, string orderby, string whereSearch, string searchText)
        {
            switch (orderby.ToLower())
            {
                case "level":
                    if (whereSearch.ToLower().Equals("level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("descricao") || whereSearch.ToLower().Equals("details"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("origem") || whereSearch.ToLower().Equals("origin"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    } else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderByDescending(x => x.CreatedAt)
                            .ToListAsync();
                    }
                case "frequencia":
                case "quantity":
                    if (whereSearch.ToLower().Equals("level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("descricao") || whereSearch.ToLower().Equals("details"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("origem") || whereSearch.ToLower().Equals("origin"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    } else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderByDescending(x => x.CreatedAt)
                            .ToListAsync();
                    }
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentSearchBy(string whereEnvironment, string whereSearch, string searchText)
        {
            switch (whereSearch.ToLower())
            {
                case "level":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .ToListAsync();
                case "descricao":
                case "details":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .ToListAsync();
                case "origem":
                case "origin":
                    return await _context.ErrorLogs
                        .Where(x => x.Environment.Name.ToLower().Equals(whereEnvironment.ToLower()) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.Environment)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectOrderedBy(string orderby)
        {
            switch (orderby.ToLower())
            {
                case "level":
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderBy(x => x.Level)
                        .ToListAsync();
                case "frequencia":
                case "quantity":
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderBy(x => x.Quantity)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectOrderedBySearchBy(string orderby, string whereSearch, string searchText)
        {
            switch (orderby.ToLower())
            {
                case "level":
                    if (whereSearch.ToLower().Equals("level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("descricao") || whereSearch.ToLower().Equals("details"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("origem") || whereSearch.ToLower().Equals("origin"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Level)
                            .ToListAsync();
                    } else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .OrderByDescending(x => x.CreatedAt)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .ToListAsync();
                    }
                case "frequencia":
                case "quantity":
                    if (whereSearch.ToLower().Equals("level"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("descricao") || whereSearch.ToLower().Equals("details"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    } else if (whereSearch.ToLower().Equals("origem") || whereSearch.ToLower().Equals("origin"))
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderBy(x => x.Quantity)
                            .ToListAsync();
                    } else
                    {
                        return await _context.ErrorLogs
                            .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                            .Include(x => x.User)
                            .Include(x => x.Environment)
                            .OrderByDescending(x => x.CreatedAt)
                            .ToListAsync();
                    }
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
            }
        }
        public async Task<IEnumerable<ErrorLog>> SelectSearchBy(string whereSearch, string searchText)
        {
            switch (whereSearch.ToLower())
            {
                case "level":
                    return await _context.ErrorLogs
                        .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .ToListAsync();
                case "descricao":
                case "details":
                    return await _context.ErrorLogs
                        .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .ToListAsync();
                case "origem":
                case "origin":
                    return await _context.ErrorLogs
                        .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .ToListAsync();
                default:
                    return await _context.ErrorLogs
                        .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                        .OrderByDescending(x => x.CreatedAt)
                        .Include(x => x.User)
                        .Include(x => x.Environment)
                        .ToListAsync();
            }
        }
        private void UpdateQuantityEventsErrorLogs()
        {
            foreach (var error in _context.ErrorLogs.ToList())
            {
                int aux = _context.ErrorLogs.Count(x => x.Title.Equals(error.Title) && x.Level.Equals(error.Level) && x.EnvironmentID.Equals(error.EnvironmentID));
                error.Quantity = aux;
                aux = 0;
            }

            _context.SaveChanges();
        }

        public async Task<ErrorLog> Create(ErrorLog errorLog)
        {
            _context.ErrorLogs.Add(errorLog);
            await _context.SaveChangesAsync();
            return errorLog;
        }
        public async Task<ErrorLog> UpdateErrorLog(ErrorLog errorLog)
        {
            _context.Update(errorLog);
            await _context.SaveChangesAsync();
            return errorLog;
        }
    }
}
