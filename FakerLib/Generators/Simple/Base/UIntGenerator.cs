using System;

namespace FakerLib.Generators.Simple.Base
{
    public class UIntGenerator: ITypeGenerator<uint>
    {
        public uint Generate() => (uint)new Random().Next();
    }
}