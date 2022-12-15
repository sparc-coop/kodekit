using Sparc.Database.Cosmos;

namespace Kodekit.Features;

public static class CosmosDbRepositoryExtensions
{
    public static async Task<List<T>> FromSqlAsync<T>(this IRepository<T> repository, string partitionKey, string sql) where T : class, IRoot<string>
    {
        if (repository is CosmosDbRepository<T> cosmosRepository)
            return await cosmosRepository.FromSqlAsync(sql, partitionKey);

        return await repository.FromSqlAsync(partitionKey, sql);
    }

    public static async Task<List<U>> FromSqlAsync<T, U>(this IRepository<T> repository, string partitionKey, string sql) where T : class, IRoot<string>
    {
        if (repository is CosmosDbRepository<T> cosmosRepository)
            return await cosmosRepository.FromSqlAsync<U>(sql, partitionKey);

        return await repository.FromSqlAsync<T, U>(partitionKey, sql);
    }

    public static IQueryable<T> Query<T>(this IRepository<T> repository, string partitionKey) where T : class, IRoot<string>
    {
        if (repository is CosmosDbRepository<T> cosmosRepository)
            return cosmosRepository.PartitionQuery(partitionKey);

        return repository.Query;
    }
}
