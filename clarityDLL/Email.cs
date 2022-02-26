using System;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Threading.Tasks;

/*
Your challenge is to address this problem with the following constraints:
• Code should be written in C#.
• Send Email Method should be in a dll that can be reused throughout different applications and entry points.
• Email sender, recipient, subject, and body (not attachments), and date must be logged/stored indefinitely with status of send attempt.
• If email fails to send it should either be retried until success or a max of 3 times whichever comes first, and can be sent in succession or over a period of time.
• Please store all credentials in an appsettings instead of hardcoded.
• At minimum that method/dll should be called from a console application.
• Extra Credit if attached to an API that can be called from Postman.
• EXTRA Credit if a front end (wpf/asp.net web application/etc...) calls the API to send the email.
• In any scenario you should be able to take in an input of a recipient email to send a test email.
*/

namespace ClarityDLL
{
    public class Email
    {
        public static async Task<string> SendEmail(string recipient, string subject, string body)
        {
            JObject settings = JObject.Parse(File.ReadAllText(@"C:\Users\Echo\source\repos\ClarityInterview\clarityDLL\appsettings.json"));

            var smtpClient = new SmtpClient((string)settings["Smtp"]["Host"])
            {
                Port = (int)settings["Smtp"]["Port"],
                Credentials = new NetworkCredential((string)settings["Smtp"]["Username"],(string)settings["Smtp"]["Password"]),
                EnableSsl = true,
            };

            var message = new MailMessage
            {
                From = new MailAddress((string)settings["Smtp"]["Username"]),
                Subject = subject,
                Body = body,
            };
            message.To.Add(recipient);

            string status = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    await smtpClient.SendMailAsync(message);
                    status = "Send attempt " + (i+1) + " succeeded";
                    i = 2;
                }
                catch
                {
                    status = "Send attempt " + (i+1) + " failed";
                }
                Databaser(message, status, settings);
            }

            return status;
        }

        private static void Databaser(MailMessage message, string status, JObject settings)
        {
            string connectionString = (string)settings["Sql"]["ConnectionString"];
            string insertQuery = $"INSERT INTO EmailHistory (Sender, Recipient, Subject, Body, Date, Status)";
            insertQuery += " VALUES (@Sender, @Recipient, @Subject, @Body, @Date, @Status)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand saveEmail = new SqlCommand(insertQuery, connection);
                saveEmail.Parameters.AddWithValue("@Sender", message.From.ToString());
                saveEmail.Parameters.AddWithValue("@Recipient", message.To.ToString());
                saveEmail.Parameters.AddWithValue("@Subject", message.Subject);
                saveEmail.Parameters.AddWithValue("@Body", message.Body);
                saveEmail.Parameters.AddWithValue("@Date", DateTime.Today);
                saveEmail.Parameters.AddWithValue("@Status", status);
                saveEmail.ExecuteNonQuery();
            }
        }

    }
}
