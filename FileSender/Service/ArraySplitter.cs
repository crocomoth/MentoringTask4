using System;
using System.Collections.Generic;

namespace FileSender.Service
{
    public class ArraySplitter
    {
        public byte[][] Split(byte[] array, int size)
        {
            if (array.Length <= size)
            {
                return new[] {array};
            }

            int currentSize = 0;
            var result = new List<byte[]>(array.Length / size);

            //do while everything can be chunked with even size
            while (currentSize < array.Length - size)
            {
                var slice = new byte[size];
                Array.Copy(array, currentSize, slice, 0, size);
                result.Add(slice);
                currentSize += size;
            }

            var chunkSize = array.Length - currentSize;
            var lastSlice = new byte[chunkSize];
            Array.Copy(array, currentSize, lastSlice, 0, chunkSize);
            result.Add(lastSlice);

            return result.ToArray();
        }
    }
}
