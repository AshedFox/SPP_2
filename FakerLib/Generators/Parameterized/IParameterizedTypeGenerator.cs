using System;

namespace FakerLib.Generators.Parameterized
{
    public interface IParameterizedTypeGenerator
    {
        object Generate(Type parameterType);
    }
}