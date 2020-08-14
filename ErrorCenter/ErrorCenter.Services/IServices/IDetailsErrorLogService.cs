using ErrorCenter.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCenter.Services.IServices
{
    public interface IDetailsErrorLogService
    {
        public Task<ErrorLog> FindErrorLog(int id);
    }
}
