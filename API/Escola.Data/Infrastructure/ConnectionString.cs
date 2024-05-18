namespace Escola.Persistence.Infrastructure
{
    public sealed record ConnectionString(string Value)
    {
        public const string SettingsKey = "Default";

        public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
    }
}
