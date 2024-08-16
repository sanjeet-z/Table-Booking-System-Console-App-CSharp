using System.Net;
using System.Net.Mail;

namespace BAL.Utilities
{
    public class SendWarningEmail
    {
        public static async Task SendEmailAsync()
        {
            try
            {
                MailMessage message = new();
                SmtpClient smtpClient = new("smtp.email.com");

                message.From = new MailAddress("name@email.com"); // Replace with your email address
                message.To.Add("name@email.com"); // Replace with recipient's email address
                message.Subject = "Account at Risk";
                message.Body = "Your account may be hacked, Please change your password";

                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("name@email.com", "xxxx xxxx xxxx xxxx"); // Replace with your email address and app password
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(message);

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

            //MailMessage message = new ();
            //    SmtpClient smtpClient = new ("smtp.email.com");

            //    message.From = new MailAddress ("name@email.com");
            //    message.To.Add ("name@email.com");
            //    message.Subject = "Account at Risk";
            //    message.Body = "Your account may be hacked, Please change your password";

            //    smtpClient.Port = 587;
            //    smtpClient.Credentials = new System.Net.NetworkCredential ("name@email.com", "xxxx xxxx xxxx xxxx");
            //    smtpClient.EnableSsl = true;

            //    await Task.Run(() => smtpClient.Send(message));

        }
    }
}
