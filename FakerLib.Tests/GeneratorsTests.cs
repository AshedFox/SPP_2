using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakerLib.Generators;
using FakerLib.Generators.Parameterized;
using FakerLib.Generators.Simple.Advanced;
using FakerLib.Generators.Simple.Base;
using Xunit;

namespace FakerLib.Tests
{
    public class GeneratorsTests
    {
        public static IEnumerable<object[]> TestBaseGenerators
        {
            get
            {
                yield return new object[] { typeof(sbyte), new SByteGenerator() };
                yield return new object[] { typeof(byte), new ByteGenerator() };
                yield return new object[] { typeof(short), new ShortGenerator() };
                yield return new object[] { typeof(ushort), new UShortGenerator() };
                yield return new object[] { typeof(uint), new UIntGenerator() };
                yield return new object[] { typeof(long), new LongGenerator() };
                yield return new object[] { typeof(ulong), new ULongGenerator() };
                yield return new object[] { typeof(float), new FloatGenerator() };
                yield return new object[] { typeof(double), new DoubleGenerator() };
                yield return new object[] { typeof(decimal), new DecimalGenerator() };
                yield return new object[] { typeof(char), new CharGenerator() };
                yield return new object[] { typeof(string), new StringGenerator() };
                yield return new object[] { typeof(DateTime), new DateTimeGenerator() };
                yield return new object[] { typeof(Guid), new GuidGenerator() };
            }
        }

        public static IEnumerable<object[]> TestCollectionGenerator
        {
            get
            {
                var collectionGenerator = new CollectionGenerator();
                yield return new object[] { typeof(sbyte), collectionGenerator };
                yield return new object[] { typeof(byte), collectionGenerator };
                yield return new object[] { typeof(short), collectionGenerator };
                yield return new object[] { typeof(ushort), collectionGenerator };
                yield return new object[] { typeof(uint), collectionGenerator };
                yield return new object[] { typeof(long), collectionGenerator };
                yield return new object[] { typeof(ulong), collectionGenerator };
                yield return new object[] { typeof(float), collectionGenerator };
                yield return new object[] { typeof(double), collectionGenerator };
                yield return new object[] { typeof(decimal), collectionGenerator };
                yield return new object[] { typeof(char), collectionGenerator };
                yield return new object[] { typeof(string), collectionGenerator };
                yield return new object[] { typeof(DateTime), collectionGenerator };
                yield return new object[] { typeof(Guid), collectionGenerator };
            }
        }

        public static IEnumerable<object[]> TestArrayGenerator
        {
            get
            {
                var arrayGenerator = new ArrayGenerator();
                yield return new object[] { typeof(sbyte), arrayGenerator };
                yield return new object[] { typeof(byte), arrayGenerator };
                yield return new object[] { typeof(short), arrayGenerator };
                yield return new object[] { typeof(ushort), arrayGenerator };
                yield return new object[] { typeof(uint), arrayGenerator };
                yield return new object[] { typeof(long), arrayGenerator };
                yield return new object[] { typeof(ulong), arrayGenerator };
                yield return new object[] { typeof(float), arrayGenerator };
                yield return new object[] { typeof(double), arrayGenerator };
                yield return new object[] { typeof(decimal), arrayGenerator };
                yield return new object[] { typeof(char), arrayGenerator };
                yield return new object[] { typeof(string), arrayGenerator };
                yield return new object[] { typeof(DateTime), arrayGenerator };
                yield return new object[] { typeof(Guid), arrayGenerator };
            }
        }
        
        [Theory]
        [MemberData(nameof(TestBaseGenerators))]
        public void Generate_GeneratingSimpleValue_ReturnedValueTypeIsValid(Type type, ITypeGenerator generator)
        {
            Assert.IsType(type, generator.Generate());
        }

        [Theory]
        [MemberData(nameof(TestCollectionGenerator))]
        public void Generate_GeneratingCollectionValue_ReturnedValueTypeIsValid(Type parameterType,
            IParameterizedTypeGenerator generator)
        {
            var result = generator.Generate(parameterType);
            Assert.IsAssignableFrom<IList>(result);
            Assert.True(result.GetType().IsGenericType);
            
            var genericArgumentsTypes = result.GetType().GetGenericArguments();
            Assert.Single(genericArgumentsTypes);
            Assert.Equal(parameterType, genericArgumentsTypes.First());
        }
        
        [Theory]
        [MemberData(nameof(TestArrayGenerator))]
        public void Generate_GeneratingArrayValue_ReturnedValueTypeIsValid(Type parameterType,
            IParameterizedTypeGenerator generator)
        {
            var result = generator.Generate(parameterType);
            Assert.True(result.GetType().IsArray);
            
            var arrayElementType = result.GetType().GetElementType();
            Assert.NotNull(arrayElementType);
            Assert.Equal(parameterType, arrayElementType);
        }
    }
}