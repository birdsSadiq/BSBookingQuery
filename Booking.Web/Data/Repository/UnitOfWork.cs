namespace Booking.Web.Data.Repository
{
    public interface IUnitOfWork
    {
        IDemoRepository DemoRepository { get; }
        Task<bool> SaveAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dc;
        public IDemoRepository DemoRepository => new DemoRepository(dc);
        public UnitOfWork(ApplicationDbContext dc)
        {
            this.dc = dc;
        }

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;//returns number of records >0 means changes successfull saved
        }


    }
}
