using System.Collections.Generic;
using System.Reflection;

namespace FakerLib
{
    public class ConstructorNode
    {
        public ConstructorInfo ConstructorInfo { get; }
        public List<MemberNode> ChildNodes { get; } = new();

        public ConstructorNode(ConstructorInfo constructorInfo)
        {
            ConstructorInfo = constructorInfo;
        }
    }
}