using Microsoft.Vbe.Interop;
using System;
using System.Configuration;
using System.Net.Mail;


namespace OctaShape.Common
{
    public class SendEmail
    {
       
  

    public void SendEmails(string subject,string Body,string messageto)
        {

            string emailid = ConfigurationManager.AppSettings["EmailId"];
            string password= ConfigurationManager.AppSettings["Password"];
            string getsmtp= ConfigurationManager.AppSettings["Smtp"];
            int getport= Convert.ToInt32(ConfigurationManager.AppSettings["port"]);

           
            //AppSettingsReader settingsReader = new AppSettingsReader();

            //string emailid = (string)settingsReader.GetValue("EmailId", typeof(String));
            //string password = (string)settingsReader.GetValue("Password", typeof(String));
            //string getsmtp = (string)settingsReader.GetValue("Smtp", typeof(String));
            //int getport = (int)settingsReader.GetValue("port", typeof(int));

            MailMessage m = new MailMessage(emailid, messageto);

            m.Subject = subject;
            m.Body = Body;

            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(getsmtp, getport);
            smtp.Credentials = new System.Net.NetworkCredential(emailid, password);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
