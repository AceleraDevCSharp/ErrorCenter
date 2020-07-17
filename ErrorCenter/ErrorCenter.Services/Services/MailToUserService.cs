using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.IServices;
using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ErrorCenter.Services.Services
{
    public class MailToUserService : IMailToUserService
    {
        private readonly ErrorCenterDbContext _context;
        public MailToUserService(ErrorCenterDbContext context)
        {
            _context = context;
        }

        public async Task<String> MailToUser(string user_mail)
        {
            var message = new MimeMessage();
            var user = FindUserByMail(user_mail);

            message.From.Add(new MailboxAddress("Grupo 1 - Wiz soluções", "groupone.wiz@gmail.com"));

            if (!IsValidEmail(user_mail)) return await Task.FromResult("Invalid mail!");

            message.To.Add(new MailboxAddress("Caro usuário!", user_mail));

            message.Subject = "Esqueceu a senha?";

            var builder = new BodyBuilder();

            if (user == null)
            {
                builder.HtmlBody = string.Format(@"<p>Olá!</p>
                                                   <p>Para o email respectivo, não existe nenhum cadastro em nossa base de dados</p>");
            }
            else
            {
                builder.HtmlBody = string.Format(@"<p>Olá!</p>
                                                   <p>Para o Email {0}</p>
                                                   <p>Senha: {1}</p>", user_mail, user.Result.Password);
            }

            message.Body = builder.ToMessageBody();

            using(var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com",587);
                client.Authenticate("groupone.wiz@gmail.com", "firstgroup!");
                client.Send(message);
                client.Disconnect(true);
            }

            return await Task.FromResult("Mail sended!");
        }

        private async Task<User> FindUserByMail(string user_mail)
        {
            var user = await _context
                .Users
                .Where(x => x.Email.Equals(user_mail))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return user;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

        }
    }
}

