using System;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CafeSolutionWPF.ConfigClasses;

public class ConfigReader
{
    public static ConfigStructure ParseSecrets() {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var exePath = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Combine(exePath, "..\\..\\..\\config.yaml");
        var myConfig = deserializer.Deserialize<ConfigStructure>(File.ReadAllText(path));
        return myConfig;
    }
    
    public static string ReadResource(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        string resourcePath = name;
        resourcePath = assembly.GetManifestResourceNames()
            .Single(str => str.EndsWith(name));
        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}