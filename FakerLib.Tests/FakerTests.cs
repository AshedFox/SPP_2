using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FakerLib.Tests.TestModels;
using Xunit;

namespace FakerLib.Tests
{
    public class FakerTests
    {
        public static IEnumerable<object[]> TestGeneratingTypes
        {
            get
            {
                yield return new object[] { typeof(TestUser1) };
                yield return new object[] { typeof(TestUser2) };
                yield return new object[] { typeof(TestUser3) };
                yield return new object[] { typeof(TestUser4) };
                yield return new object[] { typeof(TestUserRecursive1) };
                yield return new object[] { typeof(TestUserRecursive2) };
                yield return new object[] { typeof(TestUserRecursive3) };
                yield return new object[] { typeof(TestUserRecursive4) };
                yield return new object[] { typeof(TestMessage1) };
                yield return new object[] { typeof(TestMessage2) };
                yield return new object[] { typeof(TestMessage3) };
                yield return new object[] { typeof(TestMessage4) };
                yield return new object[] { typeof(TestChat1) };
                yield return new object[] { typeof(TestChat2) };
                yield return new object[] { typeof(TestChat3) };
                yield return new object[] { typeof(TestChat4) };
            }
        }

        [Theory]
        [MemberData(nameof(TestGeneratingTypes))]
        public void Create_CreatingValueOfType_CreatedValueTypeIsCorrect(Type type)
        {
            var faker = new Faker();
            var result = faker.Create(type);

            Assert.NotNull(result);
            Assert.IsType(type, result);
        }
    }
}