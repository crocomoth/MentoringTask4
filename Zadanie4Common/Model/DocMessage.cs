using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4Common.Model
{
    [Serializable]
    public class DocMessage
    {
        public DocMessage()
        {
        }

        public DocMessage(MessageType messageType, byte[] data, int order)
        {
            MessageType = messageType;
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Order = order;
        }

        public MessageType MessageType { get; set; }

        public byte[] Data { get; set; }

        public int Order { get; set; }
    }
}
