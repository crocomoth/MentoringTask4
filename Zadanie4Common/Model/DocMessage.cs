using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4Common.Model
{
    public class DocMessage
    {
        public MessageType MessageType { get; set; }

        public byte[] Data { get; set; }
    }
}
