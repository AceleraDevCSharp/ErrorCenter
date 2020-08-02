using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices {
  public interface IErrorLogService {
        public Task<ErrorLog> ArchiveErrorLog(
            int id,
            string user_email,
            string user_role
        );

        public Task<ErrorLog> DeleteErrorLog(int id, string user_email, string user_role);

    }
}