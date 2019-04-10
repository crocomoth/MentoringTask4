using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie4Common.Model;
using StringSplitOptions = System.StringSplitOptions;

namespace FileSender.Service
{
    public class MessageConverter
    {
        private ArraySplitter arraySplitter;

        public MessageConverter()
        {
            arraySplitter = new ArraySplitter();
        }

        public List<DocMessage> ConvertToMessages(byte[] data, int chunkSize, string filename)
        {
            var result = new List<DocMessage>();
            var realName = filename.Split(new[] {"\\"}, StringSplitOptions.RemoveEmptyEntries).Last();
            // First message format is = start + name + order 0
            var header = new DocMessage(MessageType.Start, Encoding.UTF8.GetBytes(realName), 0);
            result.Add(header);

            // then ordinary format
            var splittedData = arraySplitter.Split(data, chunkSize);
            int counter = 1; // count messages
            foreach (var elem in splittedData)
            {
                result.Add(new DocMessage(MessageType.Data, elem, counter));
                counter++;
            }

            // Last message has format of type - end, no data, last order
            var ending = new DocMessage(MessageType.End, new byte[1], counter);
            result.Add(ending);

            return result;
        }
    }
}
