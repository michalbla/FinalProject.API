using TNAI_FinalProject.Model;

namespace TNAI_FinalProject.Repository
{
    public abstract class BaseRepository
    {
        protected AppDbContext DbContext;
        public BaseRepository(AppDbContext dbContext) { DbContext = dbContext; }
    }
}
