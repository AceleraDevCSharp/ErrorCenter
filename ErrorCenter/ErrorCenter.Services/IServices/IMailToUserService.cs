using ErrorCenter.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCenter.Services.IServices
{
    public interface IMailToUserService
    {
        Task<String> MailToUser(string mail);
    }
}
