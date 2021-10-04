using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FakerLib.Generators.Simple.Base;

namespace FakerLib.Generators.Parameterized
{
    public class CollectionGenerator: IParameterizedTypeGenerator
    {
        public object Generate(Type parameterType)
        {
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(parameterType));

            var size = new Random().Next(0, 20);
            
            for (var i = 0; i < size; i++)
            {
                list!.Add(parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null);
            }

            return list;
        }
    }
}