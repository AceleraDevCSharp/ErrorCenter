using Flunt.Validations;
using Flunt.Notifications;
using System.Text.RegularExpressions;

namespace ErrorCenter.Services.DTOs
{
    public class UserDTO : Notifiable, IValidatable
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Environment { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
              .IsEmail(Email, "Email", "E-mail should be valid")
              .HasMaxLen(
                Email,
                100,
                "Email",
                "E-mail should have no more than 100 characters"
              )
              .IsNotNullOrEmpty(Email, "Email", "E-mail is required")
              .HasMinLen(
                Password,
                6,
                "Password",
                "Password should have at least 6 characters"
              )
              .HasMaxLen(
                Password,
                12,
                "Password",
                "Password should have no more than 12 characters"
              )
              .IsNotNull(Password, "Password", "Password is required")
              .IsNotNullOrEmpty(Environment, "Environment", "Environment is required")
            );
        }
    }
}
