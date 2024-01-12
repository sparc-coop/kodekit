using Sparc.Blossom.Data;

namespace Kodekit.Features;

public static class CosmosDbRepositoryExtensions
{
    public static async Task<List<T>> FromSqlAsync<T>(this IRepository<T> repository, string partitionKey, string sql) where T : class, IRoot<string>
    {
        if (repository is CosmosDbRepository<T> cosmosRepository)
            return await cosmosRepository.FromSqlAsync(sql, partitionKey);

        return new();
    }

    public static IQueryable<T> Query<T>(this IRepository<T> repository, string partitionKey) where T : class, IRoot<string>
    {
        if (repository is CosmosDbRepository<T> cosmosRepository)
            return cosmosRepository.PartitionQuery(partitionKey);

        return repository.Query;
    }
}
