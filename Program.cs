using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Microsoft.VisualBasic;

public class Program {
    private static readonly string path = "config.json";
    private static bool thresholdFlag = false;

    static async Task Main(string[] args) {

        //resolver separador decimal
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        //checa variaveis de entrada
        if (args.Length > 3) {
            Console.WriteLine("Variaveis de entrada incorretas");
            Console.WriteLine("Uso correto: stock-quote-alert.exe <SIGLA> <PRECO VENDA> <PRECO COMPRA>");
            return;
        }

        //define variaveis de entrada
        string stockName = args[0].ToUpper();
        decimal sellPrice = decimal.Parse(args[1]);
        decimal buyPrice = decimal.Parse(args[2]);

        //inicializa as instancias 
        ConfigEmail configEmail = ConfigEmail.LoadInfo(path);
        EmailDealer emailDealer = new EmailDealer(configEmail);
        StockDealer stockDealer = new StockDealer(stockName);

        Console.WriteLine($"configs email: {configEmail.EmailTo}");
        Console.WriteLine("configs SMTP:");
        Console.WriteLine($"configs HOST: {configEmail.Smtp.Host}");
        Console.WriteLine($"configs PORT: {configEmail.Smtp.Port}");
        Console.WriteLine($"configs USER: {configEmail.Smtp.User}");
        Console.WriteLine($"configs KEY: {configEmail.Smtp.Key}");
        Console.WriteLine("Preco de compra: " + sellPrice);
        Console.WriteLine("Preco de venda: " + buyPrice + "\n");

        while (true) {
            //busca o preco do ativo
            decimal? result = await stockDealer.GetStockPrice();

            if (result == null) {
                //preco do ativo retornou null
                Console.WriteLine("Erro - Nao foi possivel obter o preco");
                return;
            }

            if (result > sellPrice && !thresholdFlag) {

                thresholdFlag = true;

                //define mensagem do email
                string emailSubject = "Alerta de venda para " + stockName;
                string emailBody = "O ativo " + stockName + " atingiu o threshold de venda (" + sellPrice + ").\n" + stockName + ": R$ " + result;

                //envia email de venda
                emailDealer.SendEmail(emailSubject, emailBody);
                Console.WriteLine("ALERTA DE VENDA - EMAIL ENVIADO \n" + stockName + ": R$ " + result + "\n");

            }
            else if (result < buyPrice && !thresholdFlag) {

                thresholdFlag = true;

                //define mensagem do email
                string emailSubject = "Alerta de compra para " + stockName;
                string emailBody = "O ativo " + stockName + " atingiu o threshold de compra (" + sellPrice + ").\n" + stockName + ": R$ " + result;

                //envia email de compra
                emailDealer.SendEmail(emailSubject, emailBody);
                Console.WriteLine("ALERTA DE COMPRA - EMAIL ENVIADO \n" + stockName + ": R$ " + result + "\n");

            }
            else if (result >= buyPrice && result <= sellPrice) {

                thresholdFlag = false;
                Console.WriteLine("Ativo entre os thresholds: \n" + stockName + ": R$ " + result + "\n");
            }
            else {
                Console.WriteLine("EMAIL ENVIADO \n" + stockName + ": R$ " + result + "\n");
            }

            //aguarda 1 minuto e faz a analise de preco novamente
            await Task.Delay(TimeSpan.FromSeconds(30));

        }
    }
}