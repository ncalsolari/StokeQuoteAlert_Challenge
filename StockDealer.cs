using System.Net;
using System.Net.Mail;
using System.Text.Json;

/// <summary>
/// Responsavel por buscar o preco da acao fornecida, por meio ed uma API da BRAPI
/// </summary>
public class StockDealer {

    private readonly string stockName;

    public StockDealer(string stockName) {
        this.stockName = stockName;
    }

    /// <summary>
    /// Faz a requisicao para a API e extrai, do arquivo json recebido, as informacoes de preco da acao requisitada
    /// </summary>
    /// <returns>
    /// Valor da acao em reais
    /// </returns>
    public async Task<decimal?> GetStockPrice() {

        try {

            //inicia o client
            using HttpClient client = new HttpClient();
            string apiUrl = $"https://brapi.dev/api/quote/{this.stockName}";

            //faz a requisicao
            using HttpResponseMessage responseMessage = await client.GetAsync(apiUrl);

            if (!responseMessage.IsSuccessStatusCode) return null;

            //passa para json 
            string json = await responseMessage.Content.ReadAsStringAsync();
            JsonDocument stockJson = JsonDocument.Parse(json);

            //extrai as informacoes do json
            JsonElement root = stockJson.RootElement;
            JsonElement result = root.GetProperty("results")[0];
            JsonElement price = result.GetProperty("regularMarketPrice");

            //seleciona o preco
            decimal stockPrice = price.GetDecimal();

            return stockPrice;

        }
        catch (Exception e) {

            Console.WriteLine("Erro na solicitacao da API - " + e.Message);
            return null;

        }
    }
}