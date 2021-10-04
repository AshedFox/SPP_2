using System;

namespace FakerLib.Generators.Simple.Base
{
    public class ShortGenerator : ITypeGenerator<short>
    {
        public short Generate()
        {
            var bytes = new byte[sizeof(short)];
            new Random().NextBytes(bytes);
            
            return BitConverter.ToInt16(bytes);
        }
    }
}
