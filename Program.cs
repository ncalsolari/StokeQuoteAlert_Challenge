using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Microsoft.VisualBasic;

public class Program
{
    private static readonly string path = "config.json";

    static async Task Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        if (args.Length > 3)
        {
            Console.WriteLine("Variaveis de entrada incorretas");
            Console.WriteLine("Uso correto: stock-quote-alert.exe <SIGLA> <PRECO VENDA> <PRECO COMPRA>");
            return;
        }

        string stockName = args[0].ToUpper();
        decimal sellPrice = decimal.Parse(args[1]);
        decimal buyPrice = decimal.Parse(args[2]);




        ConfigEmail configEmail = ConfigEmail.LoadInfo(path);
        EmailDealer emailDealer = new EmailDealer(configEmail);
        StockDealer stockDealer = new StockDealer(stockName);

        if (configEmail != null)
        {
            Console.WriteLine($"configs email: {configEmail.EmailTo}");
            Console.WriteLine("configs SMTP:");
            Console.WriteLine($"configs HOST: {configEmail.Smtp.Host}");
            Console.WriteLine($"configs PORT: {configEmail.Smtp.Port}");
            Console.WriteLine($"configs USER: {configEmail.Smtp.User}");
            Console.WriteLine($"configs KEY: {configEmail.Smtp.Key}");
            Console.WriteLine("sell: " + sellPrice);
            Console.WriteLine("buy: " + buyPrice);

            while (true)
            {
                decimal? result = await stockDealer.GetStockPrice();

                if (result == null)
                {
                    Console.WriteLine($"Error - Unable to get Stock Price");
                    return;
                }

                if (result > sellPrice)
                {
                    emailDealer.SendEmail(stockName + " sale recommend", "teste corpo venda");
                    Console.WriteLine($"Enviado email de venda " + result);

                }
                else if (result < buyPrice)
                {
                    emailDealer.SendEmail(stockName + " purchase recommend", "teste corpo compra");
                    Console.WriteLine($"Enviado email de compra " + result);

                }
                else
                {
                    Console.WriteLine($"Sotck price in between treshholds: " + result);
                }

                await Task.Delay(TimeSpan.FromSeconds(15));

            }
       

        }


    }
}