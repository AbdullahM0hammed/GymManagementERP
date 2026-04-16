
using GymManagementSystemDAL.Data.Contexts;
using GymManagementSystemDAL.Entity;
using GymManagementSystemDAL.Repositories.Interfaces;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary <Type , object> _Repositories = new();
        private readonly GymDbContext gymDbContext;

        public UnitOfWork(GymDbContext _GymDbContext , IsessionRepository sessionRepository)
        {
            gymDbContext = _GymDbContext;
            SessionRepository = sessionRepository;
        }

        public IsessionRepository SessionRepository  {get;}

        public IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);
            if (_Repositories.TryGetValue(EntityType,out var Repo ))
                return (IGenaricRepository<TEntity>)Repo;
            var NewRepo = new GenaricRepository<TEntity>(gymDbContext);
            _Repositories[EntityType] = NewRepo;
            return NewRepo;
        }
        public int SaveChanges()
        {
            return gymDbContext.SaveChanges();
        }
    }
}
