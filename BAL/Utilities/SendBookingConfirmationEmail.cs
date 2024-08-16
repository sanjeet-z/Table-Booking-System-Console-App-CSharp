using Shared.Constants;
using System.Net.Mail;

namespace BAL.Utilities
{
    public class SendBookingConfirmationEmail
    {
        public static async Task SendEmailAsync(string receiver, string numberOfTables, string occassion, string modeOfPayment, string discount, string bookingTime)
        {
            try
            {
                MailMessage message = new();
                SmtpClient smtpClient = new(RestaurantTableBookingConstants.smtpClientAddress);

                message.From = new MailAddress("name@email.com");
                message.To.Add(receiver);
                message.Subject = "Welcome Aboard!!";
                string htmlBody = @"<html>
                            <body>
                                <h1>Welcome!</h1>
                                <p>[NumberOfTables] Table Booked for [BookingTime] to celebrate [Occassion]. You will pay through [ModeOfPayment] and [Discount] discount will be applicable”</p>
                                <p>Thank you for choosing us!</p>
                            </body></html>";

                htmlBody = htmlBody.Replace("[NumberOfTables]", numberOfTables)
                           .Replace("[Occassion]", occassion)
                           .Replace("[ModeOfPayment]", modeOfPayment)
                           .Replace("[Discount]", discount)
                           .Replace("[BookingTime]", bookingTime)
                           .Replace("Table", (int.Parse(numberOfTables) <= 1 ? "Table is" : "Tables are"));

                message.IsBodyHtml = true;
                message.Body = htmlBody;

                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(RestaurantTableBookingConstants.senderEmailId, RestaurantTableBookingConstants.senderAppPassword);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
