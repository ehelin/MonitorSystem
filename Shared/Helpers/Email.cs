using System;
using System.Text;
using Shared.data;
using Shared.misc;

namespace Shared.Helpers
{
    public class Email
    {
        private static void SendEmail(System.Net.Mail.MailMessage message,
                                    string header,
                                    string body,
                                    string email, 
                                    string smptAddr)
        {
            if (body.Length > 0)
            {
                message.To.Add(email);
                
                message.Subject = header;
                message.From = new System.Net.Mail.MailAddress(Constants.EMAIL_NO_RESPONSE);
                message.Body = body;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smptAddr);
                smtp.Send(message);
            }
        }
    
        public static void AlertEmail(DisplayOutput dispOut, string smtp, string notificationEmail)
        {
            StringBuilder sb = new StringBuilder();
            string header = Constants.SYSTEM_CHECKES + DateTime.Now.ToString();
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            foreach (string result in dispOut.Events)
            {
                sb.AppendLine(result);
                sb.AppendLine("");
            }

            Email.SendEmail(message, header, sb.ToString(), notificationEmail, smtp);
        }
    }
}
