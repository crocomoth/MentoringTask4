using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReveiver.Service
{
    public class FileReceiver
    {
        private MessageReceiver messageReceiver;
        private string filePath = AppDomain.CurrentDomain.BaseDirectory;

        public FileReceiver()
        {
            messageReceiver = new MessageReceiver();
            messageReceiver.ReceivedData += ReceieveFile;
        }

        private void ReceieveFile(byte[] data)
        {
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
