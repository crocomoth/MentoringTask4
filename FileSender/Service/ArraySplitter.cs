using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSender.Service
{
    public class ArraySplitter
    {
        public byte[][] Split(byte[] array, int size)
        {
            int currentSize = 0;
            List<byte[]> result = new List<byte[]>(array.Length / size);

            //do while everything can be chunked with even size
            while (currentSize < array.Length - size)
            {
                byte[] slice = new byte[size];
                Array.Copy(array, currentSize, slice, 0, size);
                result.Add(slice);
                currentSize += size;
            }

            var chunkSize = array.Length - currentSize;
            byte[] lastSlice = new byte[chunkSize];
            Array.Copy(array, currentSize, lastSlice, 0, chunkSize);
            result.Add(lastSlice);

            return result.ToArray();
        }
    }
}
