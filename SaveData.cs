using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.IO;

/// <summary>
/// Responsavel por salvar informacoes do ativo em um arquivo .csv
/// </summary>
public static class SaveData {

    /// <summary>
    /// Recebe preco e nome do ativo e salva em um arquivo .csv
    /// Se nao existir o cria, se ja existir adiciona os valores
    /// </summary>
    /// <param name="stockName">
    /// Nome do ativo
    /// </param>
    /// <param name="stockPrice">
    /// Preco do ativo
    /// </param>
    public static void SavePriceToCsv(string stockName, decimal? stockPrice) {

        //define nome do arquivo e checa sua existencia
        string fileName = stockName + "_price_history.csv";
        bool fileFlag = File.Exists(fileName);

        //abre o arquivo
        using StreamWriter sw = new StreamWriter(fileName, append: true) {
            AutoFlush = true //escrita imediata
        };

        if (!fileFlag) {
            sw.WriteLine("Date;Price"); //cabecalho no arquivo zerado
        }

        try {
            //define data e preco
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string price = stockPrice.ToString();

            //escreve no arquivo
            sw.WriteLine(date + ";" + price);

        }
        catch (Exception e) {
            Console.WriteLine("Erro escrever no arquivo .csv - " + e.Message);
        }



    }


}