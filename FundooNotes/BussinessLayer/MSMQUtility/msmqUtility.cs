using CommanLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Security.Claims;
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

        public msmqUtility(string secret)
        {
            Secret = secret;
        }

        //public msmqUtility()
        //{

        //}
        MessageQueue msmqQueue = new MessageQueue();

        /// <summary>
        /// method for send message
        /// </summary>
        public void SendMessage(string UserEmail, string token)
        {
            //MailMessage o = new MailMessage("f@hotmail.com", "f@hotmail.com", "KAUH Account Activation", "Hello, " + name + "\n Your KAUH Account about to activate click the link below to complete the actination process \n " +< a href =\"http://localhost:49496/Activated.aspx" > login </ a >);

            //$"Click on following link to reset your credentials for Fundoonotes: https://localhost:44361/api/Fundoo/reset-password/{token}";
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
            // Add an event handler for the ReceiveCompleted event.
            msmqQueue.ReceiveCompleted += MsmqQueue_ReceiveCompleted;
            msmqQueue.Label = "url link";
            message.Body = token;
            msmqQueue.Send(message);
            // Begin the asynchronous receive operation.
            msmqQueue.BeginReceive();
            msmqQueue.Close();
        }

        private void MsmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                // End the asynchronous Receive operation.
                var mesg = msmqQueue.EndReceive(e.AsyncResult);
                mesg.Formatter = new BinaryMessageFormatter();
                // mesg.Formatter = new XmlMessageFormatter(new string[] { "System.String, mscorlib" });
                string data = mesg.Body.ToString();
                string userEmail = ExtractData(data);
                // Process the logic be sending the message
                string mailSubject = "Link to reset your FundooNotes App Credentials";
                // using (MailMessage mailMessage = new MailMessage("malviyashreya26@gmail.com", UserEmail))
                using (MailMessage mailMessage = new MailMessage("malviyashreya26@gmail.com", userEmail))
                {
                    mailMessage.Subject = mailSubject;
                    //var messageBody = msmqQueue.msmq.receiverMessage();
                    //user = messageBody;
               
                    string url = $"https://localhost:44361/api/Fundoo/reset-password/{data }";
                    string text = $"<div style='text-align: center'>" +
                  $"<p>Click on following link to reset your credentials for Fundoonotes:</p>" +
                $"<a href='{url}' style ='color:#f44336'>Click Here To Reset Password</a>" +
                $"</div>";
                    mailMessage.Body = text;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient Smtp = new SmtpClient();
                    Smtp.Host = "smtp.gmail.com";
                    Smtp.EnableSsl = true;
                    Smtp.UseDefaultCredentials = false;
                    Smtp.Credentials = new NetworkCredential("malviyashreya26@gmail.com", "Shreya@123");
                    Smtp.Port = 587;
                    // mailMessage.From = new MailboxAddress("malviyashreya26@gmail.com");
                    //From = new MailAddress("malviyashreya26@gmail.com");
                    Smtp.Send(mailMessage);
                }
                //Restart the asynchronous receive operation.

                msmqQueue.BeginReceive();

            }
            catch (MessageQueueException qexception)
            {

            }
        }
       
        public string ExtractData(string token)
        {
            var key = Encoding.ASCII.GetBytes(Secret);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false

            };
            SecurityToken securityToken;
            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                var userEmail = principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var userId = Convert.ToInt32(principal.Claims.SingleOrDefault(c => c.Type == "userid").Value);
                return userEmail.ToString();           
            }
            catch
            {
                principal = null;
            }
            return null;
        }

        ///// <summary>
        ///// method for receiver message
        ///// </summary>
        ///// <returns></returns>
        //public string receiverMessage()
        //{
        //    MessageQueue reciever = new MessageQueue(@".\Private$\MyQueues");
        //    var recieving = reciever.Receive();
        //    recieving.Formatter = new BinaryMessageFormatter();
        //    string linkToBeSend = recieving.Body.ToString();
        //    return linkToBeSend;
        //}
    }
}
