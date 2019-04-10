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
        private string filePath = AppDomain.CurrentDomain.BaseDirectory;

        public FileReceiver()
        {
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
                foreach (var docPart in list)
                {
                    //var stream = File.Create(filePath)
                }
            }

            var fileName = FindFileName();
            var stream = File.Create(fileName);
            stream.Write(data, 0, data.Length);
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
