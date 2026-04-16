using GymManagementSystemDAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IGenaricRepository <TEntity> where TEntity : BaseEntity, new()
    {
        TEntity? GetByID(int id);
        IEnumerable<TEntity> Getall(Func<TEntity,bool>?Condation= null);
        void Add (TEntity entity);
        void Update (TEntity entity);
        void Delete (TEntity entity);
    }
}
