using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;

namespace RepositoryLayer.FundooRepository.MSMQUtility
{
    class msmqUtility
    {
        private string Secret;
        public msmqUtility(IConfiguration configuration)
        {
            Secret = configuration.GetSection("AppSettings").GetSection("Secret").Value;
        }
        MessageQueue msmqQueue = new MessageQueue();
       // private string mailSubject;

        /// <summary>
        /// method for send message
        /// </summary>
        public void SendMessage(string UserEmail, string token)
        {
            //MailMessage o = new MailMessage("f@hotmail.com", "f@hotmail.com", "KAUH Account Activation", "Hello, " + name + "\n Your KAUH Account about to activate click the link below to complete the actination process \n " +< a href =\"http://localhost:49496/Activated.aspx" > login </ a >);


            var url = $"Click on following link to reset your credentials for Fundoonotes: https://localhost:44361/api/Fundoo/reset-password/{token}";
            if (MessageQueue.Exists(@".\Private$\MyQueues"))
            {
                msmqQueue = new MessageQueue(@".\Private$\MyQueues");
            }
            else
            {
                msmqQueue = MessageQueue.Create(@".\Private$\MyQueues");
            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            msmqQueue.ReceiveCompleted += MsmqQueue_ReceiveCompleted;
            msmqQueue.Label = "url link";
            message.Body = url;
            msmqQueue.Send(message);
            msmqQueue.BeginReceive();
            msmqQueue.Close();
        }

        private void MsmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                //string user;
                var mesg = msmqQueue.EndReceive(e.AsyncResult);
                mesg.Formatter = new BinaryMessageFormatter();
                // mesg.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                string data = mesg.Body.ToString();
                // Process the logic be sending the message
                string mailSubject = "Link to reset your FundooNotes App Credentials";
                 using (MailMessage mailMessage = new MailMessage("malviyashreya26@gmail.com", "indirashahi144@gmail.com"))
               // using (MailMessage mailMessage = new MailMessage())
                {
                    //mailMessage.Subject = mailSubject;
                    //var messageBody = msmqQueue. msmq.receiverMessage();
                    //user = messageBody;
                    // mailMessage.Body = user;
                    //mailMessage.IsBodyHtml = true;
                    //SmtpClient Smtp = new SmtpClient();
                    //Smtp.Host = "smtp.gmail.com";
                    //Smtp.EnableSsl = true;
                    //Smtp.UseDefaultCredentials = false;
                    //Smtp.Credentials = new NetworkCredential("malviyashreya26@gmail.com", "Shreya@123");
                    //Smtp.Port = 587;
                    //mailMessage.From = new MailboxAddress("malviyashreya26@gmail.com");
                    //From = new MailAddress("malviyashreya26@gmail.com");
                    //Smtp.Send(mailMessage);
                }
                //Restart the asynchronous receive operation.

                msmqQueue.BeginReceive();

            }
            catch (MessageQueueException qexception)
            {

            }
        }
        /// <summary>
        /// method for receiver message
        /// </summary>
        /// <returns></returns>
        public string receiverMessage()
        {
            MessageQueue reciever = new MessageQueue(@".\Private$\MyQueues");
            var recieving = reciever.Receive();
            recieving.Formatter = new BinaryMessageFormatter();
            string linkToBeSend = recieving.Body.ToString();
            return linkToBeSend;
        }
    }
}