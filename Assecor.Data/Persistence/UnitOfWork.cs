using DataAccess.Core;

namespace Assecor.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        #region Interface Repositories
        #endregion

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            #region Initialization of repos

            #endregion
        }



        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
