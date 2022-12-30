using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Model.Models
{
    [Table("AppUser")]
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }

        public string ImageUrl { get; set; }
    }
}
