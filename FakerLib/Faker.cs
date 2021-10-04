using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using FakerLib.Generators;
using FakerLib.Generators.Parameterized;
using FakerLib.Generators.Simple.Advanced;
using FakerLib.Generators.Simple.Base;

namespace FakerLib
{
    public class Faker
    {
        private readonly byte _maxRepeatableNestingLevel;
        private readonly PluginsLoader _pluginsLoader;
        
        private MemberNode _rootMember;
        private List<MemberInfo> _classMembers;
        private readonly Dictionary<Type, ITypeGenerator> _baseTypesGenerators = new();
        private readonly Dictionary<MemberInfo, ITypeGenerator> _customGenerators = new();
        private readonly CollectionGenerator _collectionGenerator = new();
        private readonly ArrayGenerator _arrayGenerator = new();
        
        /// <summary>
        /// Fake data generator for data transfer objects (DTOs)
        /// </summary>
        /// <param name="maxRepeatableNestingLevel">
        /// Maximum nesting level for non-unique recursive complex types (for user classes)
        /// </param>
        /// <param name="config">Config with custom generator for some properties or fields</param>
        public Faker(byte maxRepeatableNestingLevel = 4, FakerConfig config = null)
        {
            _maxRepeatableNestingLevel = maxRepeatableNestingLevel;
            InitBaseGenerators();
            
            _pluginsLoader = new PluginsLoader(Path.Combine(AppContext.BaseDirectory, "plugins"));
            InitPlugins();
            
            if (config is not null)
            {
                _customGenerators = config.GetCustomGenerators();
            }
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }
        
        public object Create(Type type)
        {
            _classMembers = new List<MemberInfo>();

            _rootMember = InitMembers(new MemberNode(type, type), 0);

            var result = Fill(_rootMember);
            
            return result;
        }

        private void InitBaseGenerators()
        {
            _baseTypesGenerators.Add(typeof(sbyte), new SByteGenerator());
            _baseTypesGenerators.Add(typeof(byte), new ByteGenerator());
            _baseTypesGenerators.Add(typeof(short), new ShortGenerator());
            _baseTypesGenerators.Add(typeof(ushort), new UShortGenerator());
            _baseTypesGenerators.Add(typeof(uint), new UIntGenerator());
            _baseTypesGenerators.Add(typeof(long), new LongGenerator());
            _baseTypesGenerators.Add(typeof(ulong), new ULongGenerator());
            _baseTypesGenerators.Add(typeof(float), new FloatGenerator());
            _baseTypesGenerators.Add(typeof(double), new DoubleGenerator());
            _baseTypesGenerators.Add(typeof(decimal), new DecimalGenerator());
            _baseTypesGenerators.Add(typeof(char), new CharGenerator());
            _baseTypesGenerators.Add(typeof(string), new StringGenerator());
            _baseTypesGenerators.Add(typeof(DateTime), new DateTimeGenerator());
            _baseTypesGenerators.Add(typeof(Guid), new GuidGenerator());
        }
        
        private void InitPlugins()
        {
            var plugins = _pluginsLoader.ImportAll();
            
            foreach (var plugin in plugins)
            {
                _baseTypesGenerators.TryAdd(plugin.Key, plugin.Value);
            }
        }
        
        private MemberNode InitMembers(MemberNode memberNode, int level)
        {
            if (memberNode.Type == null)
            {
                return memberNode;
            }
            var allMembers = memberNode.Type.GetMembers(BindingFlags.Instance | BindingFlags.Public);

            var constructors = memberNode.Type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (constructors.Length > 0)
            {
                var constructorWithMostParameters = constructors[0];

                foreach (var constructorInfo in constructors)
                {
                    var constructorParameters = constructorInfo.GetParameters();

                    if (constructorParameters.Length > constructorWithMostParameters.GetParameters().Length)
                    {
                        constructorWithMostParameters = constructorInfo;
                    }
                }
                
                memberNode.ConstructorNode = InitConstructorNode(constructorWithMostParameters, level);
            }

            foreach (var childMemberInfo in allMembers)
            {
                if (childMemberInfo is PropertyInfo { CanWrite: true } propertyInfo)
                {
                    memberNode.ChildNodes.Add(InitChildNode(propertyInfo, propertyInfo.PropertyType, level + 1));
                }
                else if (childMemberInfo is FieldInfo { IsPublic: true } fieldInfo)
                {
                    memberNode.ChildNodes.Add(InitChildNode(fieldInfo, fieldInfo.FieldType, level + 1));
                }
            }

            return memberNode;
        }

        private ConstructorNode InitConstructorNode(ConstructorInfo constructorInfo, int level)
        {
            var constructorNode = new ConstructorNode(constructorInfo);
            var constructorParameters = constructorInfo.GetParameters();

            foreach (var constructorParameter in constructorParameters)
            {
                constructorNode.ChildNodes.Add(InitChildNode(constructorParameter.ParameterType,
                    constructorParameter.ParameterType, level + 1));
            }

            return constructorNode;
        }

        private MemberNode InitChildNode(MemberInfo memberInfo, Type type, int level)
        {
            var childNode = new MemberNode(memberInfo, type);

            if (memberInfo.Name == "Creator")
            {
                Console.WriteLine();
            }
            
            if (type.IsGenericType)
            {
                var genericArgsTypes = type.GetGenericArguments();

                foreach (var genericArgType in genericArgsTypes)
                {
                    var genericNode = new MemberNode(genericArgType, genericArgType);

                    if (IsComplex(genericArgType))
                    {
                        if (!_classMembers.Exists(info =>
                            info.DeclaringType == memberInfo.DeclaringType &&
                            info.MemberType == memberInfo.MemberType && string.Equals(info.Name, memberInfo.Name))) 
                        {
                            _classMembers.Add(memberInfo);
                            genericNode = InitMembers(genericNode, level);
                        }
                        else if (level < _maxRepeatableNestingLevel)
                        {
                            genericNode = InitMembers(genericNode, level);
                        }
                    }
                    
                    childNode.GenericNodes.Add(genericNode);
                }
            }
            else if (type.IsArray)
            {
                var elementType = type.GetElementType();

                var arrayNode = new MemberNode(elementType, elementType);

                if (IsComplex(elementType))
                {
                    if (!_classMembers.Exists(info =>
                        info.DeclaringType == memberInfo.DeclaringType &&
                        info.MemberType == memberInfo.MemberType && string.Equals(info.Name, memberInfo.Name)))
                    {
                        _classMembers.Add(memberInfo);
                        arrayNode = InitMembers(arrayNode, level);
                    }
                    else if (level < _maxRepeatableNestingLevel)
                    {
                        arrayNode = InitMembers(arrayNode, level);
                    }
                }
                
                childNode.ArrayNode = arrayNode;
            }
            else
            {
                if (IsComplex(type))
                {
                    if (!_classMembers.Exists(info =>
                        info.DeclaringType == memberInfo.DeclaringType &&
                        info.MemberType == memberInfo.MemberType && string.Equals(info.Name, memberInfo.Name)))
                    {
                        _classMembers.Add(memberInfo);
                        childNode = InitMembers(childNode, level);
                    }
                    else if (level < _maxRepeatableNestingLevel)
                    {
                        childNode = InitMembers(childNode, level);
                    }

                }
            }

            return childNode;
        }
        
        private object Fill(MemberNode memberNode)
        {
            if (memberNode.ChildNodes.Count == 0 && memberNode.ConstructorNode is null)
            {
                if (memberNode.Type.IsValueType)
                {
                    return default;
                }
                else
                {
                    return null;
                }
            }
            var result = RuntimeHelpers.GetUninitializedObject(memberNode.Type);
            
            if (memberNode.ConstructorNode is not null)
            {
                var constructor = memberNode.ConstructorNode.ConstructorInfo;

                var parameters = new List<object>();
                foreach (var constructorNodeChild in memberNode.ConstructorNode.ChildNodes)
                {
                    parameters.Add(Generate(constructorNodeChild));
                }
                
                constructor.Invoke(result, parameters.ToArray());
            }

            foreach (var childNode in memberNode.ChildNodes)
            {
                if (childNode.MemberInfo is PropertyInfo propertyInfo)
                {
                    var value = propertyInfo.GetValue(result);
                    if (childNode.Type.IsValueType && Activator.CreateInstance(childNode.Type)!.Equals(value) ||
                        !childNode.Type.IsValueType && value is null)
                    {
                        propertyInfo.SetValue(result, Generate(childNode));
                    }
                }
                else if (childNode.MemberInfo is FieldInfo fieldInfo)
                {
                    var value = fieldInfo.GetValue(result);
                    if (childNode.Type.IsValueType && Activator.CreateInstance(childNode.Type)!.Equals(value) ||
                        !childNode.Type.IsValueType && value is null)
                    {
                        fieldInfo.SetValue(result, Generate(childNode));
                    }
                }
            }

            return result;
        }

        private object Generate(MemberNode memberNode)
        {
            var type = memberNode.Type;

            object result;
            
            if (_customGenerators.TryGetValue(memberNode.MemberInfo, out var customGenerator))
            {
                result = customGenerator.Generate();
            }
            else
            {
                if (type.IsGenericType)
                {
                    
                    var list = (IList)_collectionGenerator.Generate(memberNode.GenericNodes[0].Type);

                    if (IsComplex(memberNode.GenericNodes[0].Type))
                    {
                        for (var i = 0; i < list.Count; i++)
                        {
                            list[i] = Fill(memberNode.GenericNodes[0]);
                        }
                    }
                    else
                    {
                        if (_baseTypesGenerators.TryGetValue(memberNode.GenericNodes[0].Type, out var generator))
                        {
                            for (var i = 0; i < list.Count; i++)
                            {
                                list[i] = generator.Generate();
                            }
                        }
                    }

                    result =  list;
                }
                else if (type.IsArray)
                {
                    var array = (Array)_arrayGenerator.Generate(memberNode.ArrayNode.Type);

                    if (IsComplex(memberNode.ArrayNode.Type))
                    {
                        for (var i = 0; i < array.Length; i++)
                        {
                            array.SetValue(Fill(memberNode.ArrayNode), i);
                        }
                    }
                    else
                    {
                        if (_baseTypesGenerators.TryGetValue(memberNode.ArrayNode.Type, out var generator))
                        {
                            for (var i = 0; i < array.Length; i++)
                            {
                                array.SetValue(generator.Generate(), i);
                            }
                        }
                    }

                    result =  array;
                }
                else
                {
                    if (IsComplex(memberNode.Type))
                    {
                        result =  Fill(memberNode);
                    }
                    else
                    {
                        result = _baseTypesGenerators.TryGetValue(type, out var generator)
                            ? generator.Generate()
                            : RuntimeHelpers.GetUninitializedObject(type);
                    }
                }
            }

            return result;
        }

        private bool IsComplex(Type type)
        {
            return type.Namespace != null &&
                   !type.Namespace.StartsWith("System") &&
                   !type.IsArray && !type.IsGenericType;
        }
    }
}