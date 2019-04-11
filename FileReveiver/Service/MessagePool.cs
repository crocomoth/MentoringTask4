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
        private Dictionary<string, List<DocMessage>> pool;
        public List<List<DocMessage>> FinishedSequences { get; set; }
        private List<string> keysToRemove;

        public MessagePool()
        {
            pool = new Dictionary<string, List<DocMessage>>();
            FinishedSequences = new List<List<DocMessage>>();
            keysToRemove = new List<string>();
        }

        public void AppendToSequence(string sessionId, DocMessage message)
        {
            List<DocMessage> buf;

            if (pool.ContainsKey(sessionId))
            {
                if (pool.TryGetValue(sessionId, out buf))
                {
                    buf.Add(message);
                }
            }
            else
            {
                pool.Add(sessionId, new List<DocMessage>() {message});
            }
        }

        public List<List<DocMessage>> FindFinished()
        {
            FinishedSequences.Clear();
            keysToRemove.Clear();

            foreach (var pair in pool)
            {
                if (CheckIfElemIsFinished(pair.Value))
                {
                    FinishedSequences.Add(pair.Value);
                    keysToRemove.Add(pair.Key);
                }
            }

            RemoveFinished();
            SortFinished();

            return FinishedSequences;
        }

        private void SortFinished()
        {
            foreach (var list in FinishedSequences)
            {
                list.Sort((x, y) => x.Order > y.Order ? 1 : -1);
            }
        }

        private void RemoveFinished()
        {
            foreach (var elem in keysToRemove)
            {
                pool.Remove(elem);
            }

            keysToRemove.Clear();
        }

        private bool CheckIfElemIsFinished(List<DocMessage> list)
        {
            if(CheckIfAllElemsPresent(list) && CheckIfAllDataIsPresent(list))
            {
                return true;
            }

            return false;
        }

        private bool CheckIfAllDataIsPresent(List<DocMessage> list)
        {
            DocMessage endNode = list.FirstOrDefault(x => x.MessageType == MessageType.End);
            if (endNode == null)
            {
                return false;
            }

            if (list.Count - 1 == endNode.Order)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfAllElemsPresent(List<DocMessage> list)
        {
            bool hasHeader = list.Any(x => x.MessageType == MessageType.Start);
            bool hasData = list.Any(x => x.MessageType == MessageType.Data);
            bool hasEnd = list.Any(x => x.MessageType == MessageType.End);

            if (hasHeader && hasData && hasEnd)
            {
                return true;
            }

            return false;
        }
    }
}
