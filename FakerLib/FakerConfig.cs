using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using FakerLib.Generators;

namespace FakerLib
{
    public class FakerConfig
    {
        private readonly Dictionary<MemberInfo, ITypeGenerator> _customGenerators = new();

        public void Add<TClass, TPropertyType, TGenerator>(Expression<Func<TClass, TPropertyType>> expression)
            where TClass : class 
            where TGenerator: ITypeGenerator, new()
        {
            ITypeGenerator typeGenerator = new TGenerator();

            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Expression is in incorrect format", nameof(expression));
            }

            var memberExpressionBody = (MemberExpression)expression.Body;
            
            if (typeGenerator.GetGeneratingType() != typeof(TPropertyType))
            {
                throw new ArgumentException("Property type doesn't match generator");
            }

            _customGenerators.Add(memberExpressionBody.Member, typeGenerator);
        }

        public Dictionary<MemberInfo, ITypeGenerator> GetCustomGenerators()
        {
            return _customGenerators;
        }
    }
}