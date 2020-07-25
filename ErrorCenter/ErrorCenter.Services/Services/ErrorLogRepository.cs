using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;

namespace ErrorCenter.Services.Services {
  public class ErrorLogRepository : IErrorLogRepository<ErrorLog> {
    protected ErrorCenterDbContext _context;

    public ErrorLogRepository(ErrorCenterDbContext context) {
      _context = context;
      UpdateQuantityEventsErrorLogs();
    }

    public async Task<IEnumerable<Environment>> Environments() {
      var environments = await _context.Roles.AsNoTracking().ToListAsync();
      return environments;
    }

    /* Alessandro */
    public async Task<IEnumerable<ErrorLog>> SelectAll() {
      return await _context.ErrorLogs
          .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
          .Include(x => x.User)
          .OrderByDescending(x => x.CreatedAt)
          .ToListAsync();
    }
    public async Task<IEnumerable<ErrorLog>> SelectArchived() {
      return await _context.ErrorLogs
          .Where(x => x.ArquivedAt != null)
          .Include(x => x.User)
          .ToListAsync();
    }
    public async Task<IEnumerable<ErrorLog>> SelectDeleted() {
      return await _context.ErrorLogs
          .Where(x => x.DeletedAt != null)
          .Include(x => x.User)
          .ToListAsync();
    }
    public async Task<IEnumerable<ErrorLog>> SelectByEnvironment(string whereEnvironment) {
      return await _context.ErrorLogs
          .Where(x => x.Environment.Equals(whereEnvironment) && x.ArquivedAt == null && x.DeletedAt == null)
          .Include(x => x.User)
          .ToListAsync();
    }
    public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBy(string whereEnvironment = null, string orderby = null) {
      switch (orderby) {
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
    public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null) {
      switch (orderby) {
        case "Level":
          if (whereSearch.Equals("Level")) {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Level)
                .ToListAsync();
          } else if (whereSearch.Equals("Descricao")) {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Level)
                .ToListAsync();
          } else if (whereSearch.Equals("Origem")) {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Level)
                .ToListAsync();
          } else {
            return await _context.ErrorLogs
                .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
          }
        case "Frequencia":
          if (whereSearch.Equals("Level")) {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Equals(whereEnvironment) && x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Quantity)
                .ToListAsync();
          } else if (whereSearch.Equals("Descricao")) {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Equals(whereEnvironment) && x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Quantity)
                .ToListAsync();
          } else if (whereSearch.Equals("Origem")) {
            return await _context.ErrorLogs
                .Where(x => x.Environment.Equals(whereEnvironment) && x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Quantity)
                .ToListAsync();
          } else {
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
    public async Task<IEnumerable<ErrorLog>> SelectByEnvironmentSearchBy(string whereEnvironment = null, string whereSearch = null, string searchText = null) {
      switch (whereSearch) {
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
    public async Task<IEnumerable<ErrorLog>> SelectOrderedBy(string orderby = null) {
      switch (orderby) {
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
    public async Task<IEnumerable<ErrorLog>> SelectOrderedBySearchBy(string orderby = null, string whereSearch = null, string searchText = null) {
      switch (orderby) {
        case "Level":
          if (whereSearch.Equals("Level")) {
            return await _context.ErrorLogs
                .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Level)
                .ToListAsync();
          } else if (whereSearch.Equals("Descricao")) {
            return await _context.ErrorLogs
                .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Level)
                .ToListAsync();
          } else if (whereSearch.Equals("Origem")) {
            return await _context.ErrorLogs
                .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Level)
                .ToListAsync();
          } else {
            return await _context.ErrorLogs
                .Where(x => x.ArquivedAt == null && x.DeletedAt == null)
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.User)
                .ToListAsync();
          }
        case "Frequencia":
          if (whereSearch.Equals("Level")) {
            return await _context.ErrorLogs
                .Where(x => x.Level.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Quantity)
                .ToListAsync();
          } else if (whereSearch.Equals("Descricao")) {
            return await _context.ErrorLogs
                .Where(x => x.Details.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Quantity)
                .ToListAsync();
          } else if (whereSearch.Equals("Origem")) {
            return await _context.ErrorLogs
                .Where(x => x.Origin.Contains(searchText) && x.ArquivedAt == null && x.DeletedAt == null)
                .Include(x => x.User)
                .OrderBy(x => x.Quantity)
                .ToListAsync();
          } else {
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
    public async Task<IEnumerable<ErrorLog>> SelectSearchBy(string whereSearch = null, string searchText = null) {
      switch (whereSearch) {
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

    private void UpdateQuantityEventsErrorLogs() {
      foreach (var error in _context.ErrorLogs.ToList()) {
        int aux = _context.ErrorLogs.Count(x => x.Title.Equals(error.Title) && x.Environment.Equals(error.Environment) && x.Level.Equals(error.Level));
        error.Quantity = aux;
        aux = 0;
      }

      _context.SaveChanges();
    }

    /* Bernardo */
    public async Task<ErrorLog> Create(ErrorLog errorLog) {
      _context.ErrorLogs.Add(errorLog);
      await _context.SaveChangesAsync();
      return errorLog;
    }
    public async Task<ErrorLog> FindById(int id) {
      var errorLog = await _context.ErrorLogs
        .Include(x => x.Environment)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

      return errorLog;
    }
    public async Task<ErrorLog> UpdateErrorLog(ErrorLog errorLog) {
      _context.Update(errorLog);
      await _context.SaveChangesAsync();
      return errorLog;
    }
  }
}
