using System;

namespace FakerLib.Generators
{
    public interface ITypeGenerator
    {
        object Generate();
        Type GetGeneratingType();
    }

    public interface ITypeGenerator<out T>: ITypeGenerator
    {
        new T Generate();

        Type ITypeGenerator.GetGeneratingType() => typeof(T);

        object ITypeGenerator.Generate() => Generate();
    }
}