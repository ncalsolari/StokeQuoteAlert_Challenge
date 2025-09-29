using System.Net;
using System.Net.Mail;
using System.Text.Json;

public class ConfigEmail
{
    public string? EmailTo { get;  set; }

    public SmtpVars? Smtp { get;  set; }

 

    public static ConfigEmail LoadInfo(string path)
    {
        string jsonFile = File.ReadAllText(path);
      
        ConfigEmail? config = System.Text.Json.JsonSerializer.Deserialize<ConfigEmail>(jsonFile);


        return config ?? throw new Exception("Erro - json file vazio!");
    }


}