using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace FileSender.Service
{
    public class MessageSender
    {
        private const string ConnectionString = "Endpoint=sb://papiunenkazadanie4.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+YXUsDUFIoIrlB4D7HGHY0vQxXtSLz9k22BD1POVpYM=";
        private const string QueueName = "docqueue2";
        private readonly QueueClient queueClient;

        public MessageSender()
        {
            this.queueClient = new QueueClient(ConnectionString, QueueName);
        }

        public async Task SendMessage(byte[] data, Guid? sessionId = null)
        {
            var message = new Message(data);
            if (sessionId.HasValue)
            {
                message.SessionId = sessionId.Value.ToString();
            }

            await queueClient.SendAsync(message);
        }

        public async Task CloseConnection()
        {
            await this.queueClient.CloseAsync();
        }
    }
}
