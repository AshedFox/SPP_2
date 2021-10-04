using System;

namespace FakerLib.Generators.Simple.Base
{
    public class LongGenerator : ITypeGenerator<long>
    {
        public long Generate()
        {
            var bytes = new byte[sizeof(long)];
            new Random().NextBytes(bytes);
            
            return BitConverter.ToInt64(bytes);
        }
    }
}