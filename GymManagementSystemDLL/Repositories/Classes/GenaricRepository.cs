using GymManagementSystemDAL.Data.Contexts;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : BaseEntity, new()
    {
       private readonly GymDbContext _dbContext;
        public GenaricRepository(GymDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        public void Add(TEntity entity)=>_dbContext.Set<TEntity>().Add(entity);
        public void Delete(TEntity entity)=>_dbContext.Set<TEntity>().Remove(entity);
         
        public IEnumerable<TEntity> Getall(Func<TEntity, bool>? Condation = null)
        {
            if (Condation is null)
            {
                return _dbContext.Set<TEntity>().ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().Where(Condation).ToList();
            }
        }
        public TEntity? GetByID(int id) => _dbContext.Set<TEntity>().Find(id);
        public void Update(TEntity entity)=>_dbContext.Set<TEntity>().Update(entity);
        
    }
}
