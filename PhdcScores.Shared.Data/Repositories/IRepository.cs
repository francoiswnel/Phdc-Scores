using System.Linq.Expressions;
using PhdcScores.Shared.Common.Entities;

namespace PhdcScores.Shared.Data.Repositories;

public interface IRepository<TEntity>
	where TEntity : Entity
{
	int Persist(TEntity entity);

	int Persist(IEnumerable<TEntity> entities);

	TEntity? Get(Guid id);

	IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereExpression);
}
