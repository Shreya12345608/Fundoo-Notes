using Experimental.System.Messaging;

namespace RepositoryLayer.MSMQService
{
    class msmqUtility
    {
        /// <summary>
        /// method for send message
        /// </summary>
        public void SendMessage()
        {
            //MailMessage o = new MailMessage("f@hotmail.com", "f@hotmail.com", "KAUH Account Activation", "Hello, " + name + "\n Your KAUH Account about to activate click the link below to complete the actination process \n " +< a href =\"http://localhost:49496/Activated.aspx" > login </ a >);


            var url = "Click on following link to reset your credentials for Fundoonotes: https://localhost:44361/api/Fundoo/reset-password \n";
            MessageQueue msmqQueue = new MessageQueue();
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msmqQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msmqQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            //msmqUtility.ReceiveCompleted += msmqUtility_ReceiveCompleted;

            message.Body = url;
            msmqQueue.Label = "url link";
            msmqQueue.Send(message);
        }
        /// <summary>
        /// method for receiver message
        /// </summary>
        /// <returns></returns>
        public string receiverMessage()
        {
            MessageQueue reciever = new MessageQueue(@".\Private$\MyQueue");
            var recieving = reciever.Receive();
            recieving.Formatter = new BinaryMessageFormatter();
            string linkToBeSend = recieving.Body.ToString();
            return linkToBeSend;
        }
    }
}
