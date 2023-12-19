using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CafeSolutionWPF.ConfigClasses;

public class ConfigReader
{
    public static ConfigStructure ParseSecrets()
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var exePath = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Combine(exePath, "..\\..\\..\\config.yaml");
        var myConfig = deserializer.Deserialize<ConfigStructure>(File.ReadAllText(path));
        return myConfig;
    }
}