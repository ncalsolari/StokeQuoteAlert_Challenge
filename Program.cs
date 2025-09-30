using System.Net;
using System.Net.Mail;
using System.Text.Json;

public class Program
{
    private static readonly string path = "config.json";

    static void Main()
    {
        ConfigEmail configs = ConfigEmail.LoadInfo(path);
        EmailDealer emailDealer = new EmailDealer(configs);
        if (configs != null)
        {
            Console.WriteLine($"configs email: {configs.EmailTo}");
            Console.WriteLine("configs SMTP:");
            Console.WriteLine($"configs HOST: {configs.Smtp.Host}");
            Console.WriteLine($"configs PORT: {configs.Smtp.Port}");
            Console.WriteLine($"configs USER: {configs.Smtp.User}");
            Console.WriteLine($"configs KEY: {configs.Smtp.Key}");


            try
            {
                emailDealer.SendEmail("teste assunto 2 ", "teste corpo 2");
                Console.WriteLine("Email SIM enviado");
                
            }
            catch
            {
                Console.WriteLine("Email NAO enviado");
            }
            
        }
       


    }
}