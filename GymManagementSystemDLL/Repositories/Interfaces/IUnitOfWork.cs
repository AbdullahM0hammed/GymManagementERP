using GymManagementSystemDAL.Entity;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IsessionRepository SessionRepository { get; }
        IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity , new();
        int SaveChanges();
    }
}
