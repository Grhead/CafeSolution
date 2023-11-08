namespace CafeSolution.ConfigClasses;

// public class Player {
//     public User User { get; set; } 
// }
//
// public class User {
//     public string Name { get; set; }
// }

public class ConfigStructure
{
    public Secrets Secrets { get; set; }
}

public class Secrets
{
    public string DbServer { get; set; }
    public string DbUser { get; set; }
    public string DbPassword { get; set; }
    public string DbDatabase { get; set; }
    
}
