using Microsoft.EntityFrameworkCore;
using Services.Impl.Data;


namespace Infrastructure.EntityFramework
{
    public class EfDbInitializer:IDbInitializer
    {
        private readonly DataBaseContext _dataContext;

        public EfDbInitializer(DataBaseContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void InitializeDb()
        {
            try
            {
                _dataContext.Database.Migrate();

                _dataContext.AddRange(FakeDataFactory.JobPostings);
                _dataContext.SaveChanges();

                _dataContext.AddRange(FakeDataFactory.Statuses);
                _dataContext.SaveChanges();

                _dataContext.AddRange(FakeDataFactory.JobPostingStatuses);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
