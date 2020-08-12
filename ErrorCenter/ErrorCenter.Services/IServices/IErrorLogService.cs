using System.Threading.Tasks;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices
{
    public interface IErrorLogService
    {

        public Task<ErrorLogDTO> CreateNewErrorLog(ErrorLogDTO newErrorLog, string email, string userEnvironment);
        public Task<ErrorLog> ArchiveErrorLog(int id, string user_email, string user_role);
        public Task<ErrorLog> DeleteErrorLog(int id, string user_email, string user_role);

    }
}