using System.Net;
using System.Net.Mail;
using System.Text.Json;

public class StockDealer
{

    private readonly string stockName;

    public StockDealer(string stockName)
    {
        this.stockName = stockName;
    }

    public async Task<decimal?> GetStockPrice()
    {

        try
        {
            using HttpClient client = new HttpClient();
            string apiUrl = $"https://brapi.dev/api/quote/{this.stockName}";

            using HttpResponseMessage responseMessage = await client.GetAsync(apiUrl);

            if (!responseMessage.IsSuccessStatusCode) return null;

            string json = await responseMessage.Content.ReadAsStringAsync();
            JsonDocument stockJson = JsonDocument.Parse(json);

            JsonElement root = stockJson.RootElement;
            JsonElement result = root.GetProperty("results")[0];
            JsonElement price = result.GetProperty("regularMarketPrice");

            decimal stockPrice = price.GetDecimal();

            return stockPrice;

        }
        catch (Exception e)
        {

            Console.WriteLine("API Request Error - " + e.Message);
            return null;

        }
    }
}