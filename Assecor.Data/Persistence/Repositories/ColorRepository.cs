
using Assecor.Data.Core.IRepositories;
using Assecor.Data.Entities;

namespace Assecor.Data.Persistence.Repositories
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        public ColorRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}