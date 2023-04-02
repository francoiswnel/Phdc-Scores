using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PhdcScores.Shared.Common.Entities;
using PhdcScores.Shared.Data.DataContext;

namespace PhdcScores.Shared.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
	where TEntity : Entity
{
	protected readonly IDataContext DataContext;

	protected RepositoryBase(IDataContext dataContext)
	{
		DataContext = dataContext;
	}

	public int Persist(TEntity entity)
	{
		GetDbSet().Add(entity);

		return DataContext.SaveChanges();
	}

	public int Persist(IEnumerable<TEntity> entities)
	{
		GetDbSet().AddRange(entities);

		return DataContext.SaveChanges();
	}

	public TEntity? Get(Guid id)
	{
		return GetDbSet().SingleOrDefault(s => !s.Deleted && s.Id == id);
	}

	public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereExpression)
	{
		return GetDbSet().Where(whereExpression);
	}

	protected abstract DbSet<TEntity> GetDbSet();
}
