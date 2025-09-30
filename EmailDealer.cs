using System.Net;
using System.Net.Mail;

/// <summary>
/// Responsavel por enviar o email utilizando de servidor SMTP
/// </summary>
public class EmailDealer {
    private readonly ConfigEmail config;

    public EmailDealer(ConfigEmail configemail) {
        config = configemail;
    }

    /// <summary>
    /// Envia um email para o destinatario configurado nas variaveis do objeto desta classe
    /// </summary>
    /// <param name="subject">Assunto do email enviado</param>
    /// <param name="body">Corpo do texto do email enviado</param>
    ///   /// <exception cref="e">
    /// Excecao caso o email nao seja enviado
    /// </exception>
    public void SendEmail(string subject, string body) {

        //separa as variaveis do servidor SMTP
        SmtpVars? smtp = this.config.Smtp;

        try {
            //inicia o client
            using SmtpClient client = new SmtpClient(smtp.Host, smtp.Port) {
                Credentials = new NetworkCredential(smtp.User, smtp.Key),
                EnableSsl = true //criptografa
            };

            //inicia o email
            MailMessage email = new MailMessage(smtp.User, this.config.EmailTo, subject, body);

            //envia o email
            client.Send(email);
        }
        catch (Exception e) {
            Console.WriteLine("Erro ao enviar o email - " + e.Message);
        }
    }
}