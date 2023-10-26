using DataAccessLayer.IDALs;
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace DataAccessLayer.IDALs
{
    public class DAL_Mail: IDAL_Mail
    {
        string smtpHost = "smtp.gmail.com"; // Reemplaza con el host de tu servidor SMTP
        int smtpPort = 587;
        string smtpUsername = "angelotunado02@gmail.com"; 
        string smtpPassword = "pcij ajml xmfe sfas";
        SmtpClient client;

        public DAL_Mail()
        {
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true; // Habilita SSL si es necesario
            this.client = smtpClient;
        }

        public void sendMail(String receptor, String asunto, String content)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(receptor); 
            mailMessage.Subject = asunto; 
            mailMessage.Body = content;
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
            Console.WriteLine("Correo electrónico enviado correctamente.");
        }
    }
}
