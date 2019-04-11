using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie4Common.Model;
using Zadanie4Common.Service;

namespace FileReveiver.Service
{
    public class FileReceiver
    {
        private MessageReceiver messageReceiver;
        private BinaryConverter binaryConverter;
        private MessagePool messagePool;
        private MessageConverter messageConverter;
        private string filePath = AppDomain.CurrentDomain.BaseDirectory;

        public FileReceiver()
        {
            messageConverter = new MessageConverter();
            binaryConverter = new BinaryConverter();
            messagePool = new MessagePool();
            messageReceiver = new MessageReceiver();
            messageReceiver.ReceivedData += ReceieveFile;
        }

        private void ReceieveFile(byte[] data, string sessionId)
        {
            DocMessage message = binaryConverter.DeserializeDocMessage(data);
            messagePool.AppendToSequence(sessionId, message);
            List<List<DocMessage>> finished = messagePool.FindFinished();

            if (finished.Count == 0)
            {
                return;
            }

            foreach (var list in finished)
            {
                DocMessage header = list.First(x => x.MessageType == MessageType.Start);
                var filename = messageConverter.GetFileTitle(header);

                var dataParts = list.FindAll(x => x.MessageType == MessageType.Data).ToList();
                // put first part in buffer
                byte[] buffer = dataParts[0].Data;
                // fill in the buffer
                for (int i = 1; i < dataParts.Count; i++)
                {
                    buffer = messageConverter.AppendToArray(buffer, dataParts[i].Data);
                }

                var stream = File.Create(filePath + filename);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }

            Console.WriteLine("received message");
        }

        private string FindFileName()
        {
            for (int i = 0; i < 100; i++)
            {
                if (!File.Exists(filePath + $"//{i}"))
                {
                    return filePath + $"//{i}.pdf";
                }
            }

            return "error";
        }
    }
}
