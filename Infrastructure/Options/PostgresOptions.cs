namespace Infrastructure.Options;

internal sealed class PostgresOptions
{
    public string ConnectionString { get; set; } = string.Empty;
}