using System;

namespace ErrorCenter.Persistence.EF.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Environment { get; set; }
        public string Level { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ArquivedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Origin { get; set; }

        public string IdUser { get; set; }
        public User User { get; set; }
    }
}
