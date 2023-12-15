using System;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CafeSolution.ConfigClasses;

public class ConfigReader
{
    public static ConfigStructure ParseSecrets() {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        string resourceText = ReadResource("CafeSolution.config.yaml");
        return deserializer.Deserialize<ConfigStructure>(resourceText);
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