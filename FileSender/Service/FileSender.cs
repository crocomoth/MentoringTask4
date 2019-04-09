using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSender.Service
{
    public class FileSender
    {
        private readonly MessageSender messageSender;
        private readonly ArraySplitter arraySplitter;
        private string path = AppDomain.CurrentDomain.BaseDirectory;
        private const int Threshold = 200 * 1000;

        public FileSender()
        {
            messageSender = new MessageSender();
            arraySplitter = new ArraySplitter();
        }

        public async Task SendFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var data = File.ReadAllBytes(filePath);
            if (data.Length > Threshold)
            {
                var sessionId = Guid.NewGuid();
                var splittedData = arraySplitter.Split(data, Threshold);

                foreach (var elem in splittedData)
                {
                    await messageSender.SendMessage(elem, sessionId);
                }
            }
        }
    }
}
