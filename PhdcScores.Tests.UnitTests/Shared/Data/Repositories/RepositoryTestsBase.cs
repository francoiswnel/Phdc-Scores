using Microsoft.EntityFrameworkCore;

namespace PhdcScores.Tests.UnitTests.Shared.Data.Repositories;

public class RepositoryTestsBase
{
	protected static Mock<DbSet<T>> GetMockDbSet<T>(IEnumerable<T> enumerable)
		where T : class
	{
		var data = enumerable.AsQueryable();

		var mockSet = new Mock<DbSet<T>>();
		mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
		mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
		mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
		mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

		return mockSet;
	}
}
