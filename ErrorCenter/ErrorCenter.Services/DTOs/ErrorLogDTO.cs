using Flunt.Validations;
using Flunt.Notifications;
using System.Text.RegularExpressions;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.DTOs
{
    public class ErrorLogDTO : Notifiable, IValidatable
    {

        public string Environment { get; set; }
        public string Level { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Origin { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()

            .IsNotNullOrEmpty(Environment, "Environment", "Environment is required")

            .HasMaxLen(Level, 30, "Level", "Level should have no more than 30 characters")
            .IsNotNullOrEmpty(Level, "Level", "Level is required")
            .HasMinLen(Level, 3, "Level", "Level should have more than 3 characters")

            .HasMaxLen(Title, 500, "Title", "Title should have no more than 500 characters")
            .IsNotNullOrEmpty(Title, "Title", "Title is required")
            .HasMinLen(Title, 5, "Title", "Title should have more than 10 characters")

            .HasMaxLen(Details, 1500, "Details", "Details should have no more than 1500 characters")
            .IsNotNullOrEmpty(Details, "Details", "Details is required")
            .HasMinLen(Details, 5, "Details", "Details should have more than 10 characters")

            .HasMaxLen(Origin, 100, "Origin", "Origin should have no more than 100 characters")
            .IsNotNullOrEmpty(Origin, "Origin", "Origin is required")
            .HasMinLen(Origin, 5, "Origin", "Origin should have more than 3 characters")

          );
        }
    }
}
