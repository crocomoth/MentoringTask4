using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Zadanie4Common.Model;
using Zadanie4Common.Service;

namespace FileSender.Service
{
    public class FileSender
    {
        private readonly MessageSender messageSender;
        private readonly MessageConverter messageConverter;
        private readonly BinaryConverter converter;

        private const int Threshold = 200 * 1000;

        public FileSender()
        {
            messageSender = new MessageSender();
            messageConverter = new MessageConverter();
            converter = new BinaryConverter();
        }

        public async Task SendFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var data = File.ReadAllBytes(filePath);

            List<DocMessage> docMessages = messageConverter.ConvertToMessages(data, Threshold, filePath);
            List<byte[]> serializedList = converter.SerializeListToBytes(docMessages);
            var sessionId = Guid.NewGuid();

            foreach (var elem in serializedList)
            {
                await messageSender.SendMessage(elem, sessionId);
            }
        }

        public async Task CloseConnection()
        {
            await messageSender.CloseConnection();
        }
    }
}
