using Microsoft.Extensions.Configuration;

namespace DataService.Shared.Configuration;

public record ConnectionString(string Plant, string Value);

public class ConnectionStringProvider
{
    private readonly Dictionary<string, List<ConnectionString>> _connectionStrings = new();

    public ConnectionStringProvider(IConfigurationSection databasesSection)
    {
        var databaseNames = databasesSection.GetChildren().Select(x => x.Key);

        foreach (var databaseName in databaseNames)
        {
            _connectionStrings[databaseName] = new List<ConnectionString>();

            var databaseSection = databasesSection.GetSection(databaseName);

            var c = databaseSection.GetChildren().ToList();

            // In case there is a single database instance for multiple plants,
            // the configuration can be flat.
            if (c.Count == 0) _connectionStrings[databaseName].Add(new ConnectionString(databaseSection.Key, databaseSection.Value));

            // If there are separate database instances for each plant,
            // the configuration is nested.
            else
            {
                foreach (var s in databaseSection.GetChildren())
                {
                    var plant = s.Key;
                    var connectionString = s.Value;
                    _connectionStrings[databaseName].Add(new ConnectionString(plant, connectionString));
                }
            }
        }
    }

    public IEnumerable<ConnectionString> GetConnectionStrings(string databaseName) => _connectionStrings[databaseName];
}