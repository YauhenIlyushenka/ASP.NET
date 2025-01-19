namespace Pcf.CommonData.Core.Core.Abstractions
{
	public interface IMongoRepository<T, TId> where T : class
	{
		Task<List<T>> GetAllAsync();
		Task<T> GetByIdAsync(TId id);
	}
}
