using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Web.Models
{
    [Table("Demo")]
    public class Demo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        
        public string LogoUrl { get; set; }


        public int? AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }
    }
}
