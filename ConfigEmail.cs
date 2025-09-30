using System.Net;
using System.Net.Mail;
using System.Text.Json;

/// <summary>
/// Responsavel por definir as variáveis para envio de email utilizadas no servidor SMTP
///
/// </summary>
public class ConfigEmail {
    public string? EmailTo { get; set; }
    public SmtpVars? Smtp { get; set; }

    /// <summary>
    /// Carrega informacoes necessarias para envio de email de um arquivo json (enderecos, senhas e variaveis SMTP)
    /// </summary>
    /// <param name="path">
    /// Caminho para arquivo json contendo as informacoes
    /// </param>
    /// <returns>
    /// Objeto da classe ConfigEmail com as informacoes devidamente alocadas
    /// </returns>
    /// <exception cref="Exception">
    /// Excecao caso o objeto seja criado vazio
    /// </exception>
    public static ConfigEmail LoadInfo(string path) {
        //le arquivo json
        string jsonFile = File.ReadAllText(path);

        //refatora informacoes de configuracao para envio de email
        ConfigEmail? config = System.Text.Json.JsonSerializer.Deserialize<ConfigEmail>(jsonFile);

        //retorna configuracoes, se for null aciona excecao
        return config ?? throw new Exception("Erro - json file vazio!");
    }


}