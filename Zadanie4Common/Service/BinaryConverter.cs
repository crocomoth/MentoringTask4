using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Zadanie4Common.Model;

namespace Zadanie4Common.Service
{
    public class BinaryConverter
    {
        private IFormatter formatter;

        public BinaryConverter()
        {
            formatter = new BinaryFormatter();
        }

        public byte[] SerializeToBytes(DocMessage document)
        {
            byte[] bytes;

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, document);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public List<byte[]> SerializeListToBytes(List<DocMessage> docMessages)
        {
            var result = new List<byte[]>();

            foreach (var message in docMessages)
            {
                result.Add(SerializeToBytes(message));
            }

            return result;
        }
    }
}
