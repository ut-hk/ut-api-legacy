using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace UniTime.EntityFramework.Repositories
{
    public abstract class UniTimeRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<UniTimeDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected UniTimeRepositoryBase(IDbContextProvider<UniTimeDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        // add common methods for all repositories
    }

    public abstract class UniTimeRepositoryBase<TEntity> : UniTimeRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected UniTimeRepositoryBase(IDbContextProvider<UniTimeDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        // do not add any method here, add to the class above (since this inherits it)
    }
}
