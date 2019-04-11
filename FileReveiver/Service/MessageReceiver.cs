using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace FileReveiver.Service
{
    public class MessageReceiver
    {
        private const string ConnectionString = "Endpoint=sb://papiunenkazadanie4.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+YXUsDUFIoIrlB4D7HGHY0vQxXtSLz9k22BD1POVpYM=";
        private const string QueueName = "docqueue2";
        private readonly QueueClient queueClient;

        public event Action<byte[], string> ReceivedData; 

        public MessageReceiver()
        {
            queueClient = new QueueClient(ConnectionString, QueueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
        }

        private async Task ReceiveMessagesAsync(Message message, CancellationToken token)
        {
            var data = message.Body;
            ReceivedData?.Invoke(data, message.SessionId);

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine(arg.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
