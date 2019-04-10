using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace MessageReceiver.Service
{
    public class DataReceiver
    {
        private const string ConnectionString =
            "Endpoint=sb://papiunenkazadanie4.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+YXUsDUFIoIrlB4D7HGHY0vQxXtSLz9k22BD1POVpYM=";

        private const string QueueName = "documentqueue";
        private QueueClient queueClient;

        public DataReceiver()
        {
            this.queueClient = new QueueClient(ConnectionString, QueueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
        }

        private Task ReceiveMessagesAsync(Message message, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }
    }
}
