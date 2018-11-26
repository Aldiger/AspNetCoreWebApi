using System;

namespace DataAccess.Core
{
    public interface IUnitOfWork : IDisposable
    {

		int Complete();
    }
}
