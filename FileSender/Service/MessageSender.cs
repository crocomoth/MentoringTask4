using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace FileSender.Service
{
    public class MessageSender
    {
        private readonly string connectionString = "Endpoint=sb://papiunenkazadanie4.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+YXUsDUFIoIrlB4D7HGHY0vQxXtSLz9k22BD1POVpYM=";
        private const string QueueName = "docqueue2";
        private QueueClient queueClient;

        public MessageSender()
        {
            this.queueClient = new QueueClient(connectionString, QueueName);
        }

        public MessageSender(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.queueClient = new QueueClient(connectionString, QueueName, ReceiveMode.ReceiveAndDelete);
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
