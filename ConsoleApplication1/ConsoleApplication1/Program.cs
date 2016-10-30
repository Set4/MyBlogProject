using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class SequentialGuidGenerator
    {
        private static readonly RNGCryptoServiceProvider Rng = new RNGCryptoServiceProvider();

        public static Guid NewSequentialGuid()
        {
            var randomBytes = new byte[10];
            Rng.GetBytes(randomBytes);

            var timestamp = DateTime.Now.Ticks / 10000L;
            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = new byte[16];



            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

            // If formatting as a string, we have to reverse the order
            // of the Data1 and Data2 blocks on little-endian systems.
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(guidBytes, 0, 4);
                Array.Reverse(guidBytes, 4, 2);
            }



            return new Guid(guidBytes);
        }
    }

    public class NativeMethods
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid guid);
    }

    public static class SequentialGuidGenerator2
    {
        public static Guid CreateGuid()
        {
            const int RPC_S_OK = 0;

            Guid guid;
            int result = NativeMethods.UuidCreateSequential(out guid);
            if (result == RPC_S_OK)
                return guid;
            else
                return Guid.NewGuid();
        }
    }

    public class SequentialGuid
    {
        Guid _CurrentGuid;
        public Guid CurrentGuid
        {
            get
            {
                return _CurrentGuid;
            }
        }

        public SequentialGuid()
        {
            _CurrentGuid = Guid.NewGuid();
        }

        public SequentialGuid(Guid previousGuid)
        {
            _CurrentGuid = previousGuid;
        }

        public static SequentialGuid operator ++(SequentialGuid sequentialGuid)
        {
            byte[] bytes = sequentialGuid._CurrentGuid.ToByteArray();
            for (int mapIndex = 0; mapIndex < 16; mapIndex++)
            {
                int bytesIndex = SqlOrderMap[mapIndex];
                bytes[bytesIndex]++;
                if (bytes[bytesIndex] != 0)
                {
                    break; // No need to increment more significant bytes
                }
            }
            sequentialGuid._CurrentGuid = new Guid(bytes);
            return sequentialGuid;
        }

        private static int[] _SqlOrderMap = null;
        private static int[] SqlOrderMap
        {
            get
            {
                if (_SqlOrderMap == null)
                {
                    _SqlOrderMap = new int[16] {
                    3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10
                };
                    // 3 - the least significant byte in Guid ByteArray [for SQL Server ORDER BY clause]
                    // 10 - the most significant byte in Guid ByteArray [for SQL Server ORDERY BY clause]
                }
                return _SqlOrderMap;
            }
        }
    }


    internal static class SequentialGuidUtils
    {
        public static Guid CreateGuid()
        {
            Guid guid;
            int result = NativeMethods.UuidCreateSequential(out guid);
            if (result == 0)
            {
                var bytes = guid.ToByteArray();
                var indexes = new int[] { 3, 2, 1, 0, 5, 4, 7, 6, 8, 9, 10, 11, 12, 13, 14, 15 };
                return new Guid(indexes.Select(i => bytes[i]).ToArray());
            }
            else
                throw new Exception("Error generating sequential GUID");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Guid g1;
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine("NewSequentialGuid");
                g1 = SequentialGuidGenerator.NewSequentialGuid();
                Console.WriteLine(g1.ToString());
                Console.WriteLine("");
                Console.WriteLine("CreateGuid");
                g1 = SequentialGuidGenerator2.CreateGuid();
                Console.WriteLine(g1.ToString());
                Console.WriteLine("");

                Console.WriteLine("SequentialGuid");
                SequentialGuid g3 = new SequentialGuid();
                Console.WriteLine(g3.CurrentGuid.ToString());
                Console.WriteLine("");

                Console.WriteLine("SequentialGuidUtils");
                g1 = SequentialGuidUtils.CreateGuid();
                Console.WriteLine(g1.ToString());
                Console.WriteLine("");
            }
    Console.ReadKey();
        }
    }
}
