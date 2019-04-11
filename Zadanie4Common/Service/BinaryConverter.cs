using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Zadanie4Common.Model;

namespace Zadanie4Common.Service
{
    public class BinaryConverter
    {
        private readonly IFormatter formatter;

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

        public List<DocMessage> DeserializeBytesToList(List<byte[]> data)
        {
            var result = new List<DocMessage>();

            foreach (var elem in data)
            {
                result.Add(DeserializeDocMessage(elem));
            }

            return result;
        }

        public DocMessage DeserializeDocMessage(byte[] data)
        {
            DocMessage docMessage;

            using (var stream = new MemoryStream(data))
            {
                docMessage = (DocMessage)formatter.Deserialize(stream);
            }

            return docMessage;
        }

        public string GetStringFromByteArray(byte[] array)
        {
            return Encoding.UTF8.GetString(array);
        }
    }
}
