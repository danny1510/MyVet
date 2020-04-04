using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace MyVet.Web.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string to, string subject, string body)
        {
             string from = _configuration["Mail:From"];
             string smtp = _configuration["Mail:Smtp"];
             string port = _configuration["Mail:Port"];
             string password = _configuration["Mail:Password"];

             MailMessage oMailMessage = new MailMessage(from, to, subject, body );
             oMailMessage.IsBodyHtml = true;

             SmtpClient oSmtpClient = new SmtpClient(smtp);
             oSmtpClient.EnableSsl = true;
             oSmtpClient.UseDefaultCredentials = false;
             oSmtpClient.Port =int.Parse(port);
             oSmtpClient.Credentials = new System.Net.NetworkCredential(from, password);
             oSmtpClient.Send(oMailMessage);
             oSmtpClient.Dispose();

        }




    }
}
