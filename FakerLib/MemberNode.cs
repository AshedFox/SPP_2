using System;
using System.Collections.Generic;
using System.Reflection;

namespace FakerLib
{
    public class MemberNode
    {
        public MemberInfo MemberInfo { get; }
        public Type Type { get; }
        public MemberNode ArrayNode { get; set; }
        public List<MemberNode> GenericNodes { get; } = new();
        public List<MemberNode> ChildNodes { get; } = new();
        public ConstructorNode ConstructorNode { get; set; }

        public MemberNode(MemberInfo memberInfo, Type type)
        {
            MemberInfo = memberInfo;
            Type = type;
        }
    }
}