using System.Data;
using System.Data.Common;
using System.Text;
using Dapper;
using InnowiseClinic.Microservices.Profiles.Data.Entities.Interfaces;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public abstract partial class Repository<TEntity> : IRepository<TEntity>, IDisposable
    where TEntity : IEntity
{
    protected Repository(
        IDbConnectionFactory connectionFactory,
        ISqlValueFormatter sqlValueFormatter)
    {
        Connection = connectionFactory.CreateConnection();
        SqlValueFormatter = sqlValueFormatter;
    }

    protected IDbConnection Connection { get; private set; }

    protected bool Disposed { get; private set; }

    protected ISqlValueFormatter SqlValueFormatter { get; }

    protected abstract string TableName { get; }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);

        string query = new StringBuilder()
            .AppendLine(@$"SELECT * FROM {TableName}")
            .AppendLine(@$"WHERE {nameof(IEntity.Id)} = {SqlValueFormatter.FormatToSql(id)};")
            .ToString();

        return await Connection.QuerySingleOrDefaultAsync<TEntity>(query);
    }

    public async Task<Guid> AddAsync(TEntity entity)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);

        var namesAndValues = GetPropertyNameValueDictionary(entity);

        var formattedValues = namesAndValues.Values
            .Select(value => SqlValueFormatter.FormatToSql(value));

        string command = new StringBuilder()
            .AppendLine(@$"INSERT INTO {TableName}")
            .AppendLine(@$"({string.Join(',', namesAndValues.Keys)})")
            .AppendLine(@"VALUES")
            .AppendLine(@$"({string.Join(',', formattedValues)})")
            .AppendLine(@$"RETURNING {nameof(IEntity.Id)};")
            .ToString();

        return await Connection.ExecuteScalarAsync<Guid>(command);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);

        string assignments = string.Join(
                ", ",
                GetPropertyNameValueDictionary(entity)
                    .Select(kv => @$"{kv.Key} = {kv.Value}"));

        string command = new StringBuilder()
            .AppendLine(@$"UPDATE {TableName}")
            .AppendLine(@$"SET {assignments}")
            .AppendLine(@$"WHERE {nameof(IEntity.Id)} = {SqlValueFormatter.FormatToSql(entity.Id)};")
            .ToString();

        await Connection.ExecuteAsync(command);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);

        var command = new StringBuilder()
            .AppendLine(@$"DELETE FROM {TableName}")
            .AppendLine(@$"WHERE {nameof(IEntity.Id)} = {SqlValueFormatter.FormatToSql(entity.Id)};")
            .ToString();

        await Connection.ExecuteAsync(command);
    }

    public void Dispose()
    {
        if (!Disposed)
        {
            Dispose(disposing: true);
            Disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    ~Repository()
    {
        if (!Disposed)
        {
            Dispose(disposing: false);
            Disposed = true;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Connection.Dispose();
            Connection = null!;
        }
    }

    protected abstract IEnumerable<(string Key, object? Value)> GetPropertyNamesAndValues(TEntity entity);

    private IDictionary<string, object?> GetPropertyNameValueDictionary(TEntity entity)
    {
        var dictionary = GetPropertyNamesAndValues(entity).ToDictionary(
            keySelector: kv => kv.Key,
            elementSelector: kv => kv.Value,
            comparer: StringComparer.OrdinalIgnoreCase);

        dictionary.Remove(nameof(IEntity.Id));

        return dictionary;
    }
}