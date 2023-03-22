using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SendEmailFunc
{
    [StorageAccount("BlobConnectionString")]
    public class SendEmail
    {
        [FunctionName("SendEmail")]
        public void Run(
            [BlobTrigger("form/{name}", Connection= "AzureWebJobsStorage")] Stream myBlob,
            string name, 
            ILogger log,
            IDictionary<string, string> metaData)
        {
            MailMessage msg = new MailMessage();
            msg.From = new System.Net.Mail.MailAddress("nboholii@gmail.com", "C# Trainee");
            msg.To.Add(new System.Net.Mail.MailAddress(metaData["Email"], ""));

            msg.Subject = $"File {name} has been successfully uploaded!";
            msg.Body = $"Voilà! Your file {name} has been successfully uploaded to Azure Blob storage! This email sent for notificational purposes only. Please, do no reply.";

            SmtpClient client = new SmtpClient("in.mailjet.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("217249df51e61cad38d7d3c623a7fdb9", "90092feafda2aabbfe9799d1ab8e72d6");

            client.Send(msg);
        }
    }
}
