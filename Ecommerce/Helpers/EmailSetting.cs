using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Ecommerce.Helpers
{
    public class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.sendgrid.net", 587);
                client.EnableSsl = true;//Encrypted
                client.Credentials = new NetworkCredential("apikey", "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");
                client.Send("dogaryahmed2017@gmail.com", email.To, email.Title, email.Body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
