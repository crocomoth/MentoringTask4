﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zadanie4Common.Model;
using Zadanie4Common.Service;

namespace FileReveiver.Service
{
    public class FileReceiver
    {
        private MessageReceiver messageReceiver;
        private readonly BinaryConverter binaryConverter;
        private readonly MessagePool messagePool;
        private readonly MessageConverter messageConverter;
        private readonly string filePath = AppDomain.CurrentDomain.BaseDirectory;

        public FileReceiver()
        {
            messageConverter = new MessageConverter();
            binaryConverter = new BinaryConverter();
            messagePool = new MessagePool();
            messageReceiver = new MessageReceiver();
            messageReceiver.ReceivedData += ReceiveFile;
        }

        private void ReceiveFile(byte[] data, string sessionId)
        {
            DocMessage message = binaryConverter.DeserializeDocMessage(data);
            messagePool.AppendToSequence(sessionId, message);
            List<List<DocMessage>> finished = messagePool.FindFinished();

            if (finished.Count == 0)
            {
                return;
            }

            foreach (var list in finished)
            {
                DocMessage header = list.First(x => x.MessageType == MessageType.Start);
                var filename = messageConverter.GetFileTitle(header);

                var dataParts = list.FindAll(x => x.MessageType == MessageType.Data).ToList();
                // put first part in buffer
                byte[] buffer = dataParts[0].Data;
                // fill in the buffer
                for (int i = 1; i < dataParts.Count; i++)
                {
                    buffer = messageConverter.AppendToArray(buffer, dataParts[i].Data);
                }

                var stream = File.Create(filePath + filename);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }

            Console.WriteLine("received file");
        }
    }
}
