using System.Net;
using System.Net.Mail;


public class EmailDealer 
{
    private readonly ConfigEmail config;

    public EmailDealer(ConfigEmail configemail)
    {
        config = configemail;
    }

    public void SendEmail(string subject, string body)
    {
        SmtpVars? smtp = this.config.Smtp;

        try
        {
            using SmtpClient client = new SmtpClient(smtp.Host, smtp.Port)
            {
                Credentials = new NetworkCredential(smtp.User, smtp.Key),
                EnableSsl = true //criptografa
            };


            MailMessage email = new MailMessage(smtp.User, this.config.EmailTo, subject, body);

            client.Send(email);
        }
        catch(Exception e)
        {
            Console.WriteLine("NULL Config Variables - " + e.Message);
        }




       

    }


    
    
}