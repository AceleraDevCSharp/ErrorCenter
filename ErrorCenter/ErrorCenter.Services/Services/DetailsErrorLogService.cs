using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using System.Threading.Tasks;
using ErrorCenter.Services.Errors;
using Microsoft.AspNetCore.Http;

namespace ErrorCenter.Services.Services
{
    public class DetailsErrorLogService : IDetailsErrorLogService
    {
        private readonly IErrorLogRepository<ErrorLog> _errorLogRepository;

        public DetailsErrorLogService(IErrorLogRepository<ErrorLog> errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public async Task<ErrorLog> FindErrorLog(int id)
        {
            var error = await _errorLogRepository.FindById(id);

            if (error == null)
                throw new ErrorLogException("Log not Found!", StatusCodes.Status404NotFound);

            if (error.ArquivedAt != null)
                throw new ErrorLogException("This Log are archived!", StatusCodes.Status204NoContent);

            if (error.DeletedAt != null)
                throw new ErrorLogException("This Log are deleted!", StatusCodes.Status204NoContent);

            return error;
        }
    }
}
