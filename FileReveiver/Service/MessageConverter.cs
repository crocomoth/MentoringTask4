using System;
using Zadanie4Common.Model;
using Zadanie4Common.Service;

namespace FileReveiver.Service
{
    public class MessageConverter
    {
        private readonly BinaryConverter binaryConverter;

        public MessageConverter()
        {
            binaryConverter = new BinaryConverter();
        }

        public string GetFileTitle(DocMessage docMessage)
        {
            if (docMessage.MessageType != MessageType.Start)
            {
                throw new Exception("not a start node");
            }

            return binaryConverter.GetStringFromByteArray(docMessage.Data);
        }

        public byte[] AppendToArray(byte[] source, byte[] addition)
        {
            var result = new byte[source.Length + addition.Length];
            source.CopyTo(result, 0);
            addition.CopyTo(result, source.Length);

            return result;
        }
    }
}
