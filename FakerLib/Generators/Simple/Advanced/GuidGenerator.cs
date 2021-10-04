using System;

namespace FakerLib.Generators.Simple.Advanced
{
    public class GuidGenerator: ITypeGenerator<Guid>
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }

        object ITypeGenerator.Generate()
        {
            return Generate();
        }
    }
}