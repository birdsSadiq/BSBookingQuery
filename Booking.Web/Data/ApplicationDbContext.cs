using Booking.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booking.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        #region tables
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Demo> Demo { get; set; }
        #endregion


    }
}