using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Zadanie4Common.Model;

namespace FileReveiver.Service
{
    public class MessagePool
    {
        private Dictionary<string, List<Message>> pool;
        public List<List<Message>> FinishedSequences { get; set; }

        public MessagePool()
        {
            pool = new Dictionary<string, List<Message>>();
        }

        public void AppendToSequence(string sessionId, Message message)
        {
            List<Message> buf;

            if (pool.ContainsKey(sessionId))
            {
                if (pool.TryGetValue(sessionId, out buf))
                {
                    buf.Add(message);
                }
            }
            else
            {
                pool.Add(sessionId, new List<Message>() {message});
            }
        }
    }
}
