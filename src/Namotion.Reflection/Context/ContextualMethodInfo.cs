using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Namotion.Reflection
{
    /// <summary>
    /// A method info with contextual information.
    /// </summary>
    public class ContextualMethodInfo : ContextualMemberInfo
    {
        private string? _name;
        private bool? _isValueType;
        private ContextualParameterInfo[]? _parameters;

        internal ContextualMethodInfo(MethodInfo methodInfo, ref int nullableFlagsIndex)
            : base(methodInfo, methodInfo.ReturnType, ref nullableFlagsIndex)
        {
            MethodInfo = methodInfo;
        }

        /// <summary>
        /// Gets the type context's method info.
        /// </summary>
        public MethodInfo MethodInfo { get; }

        /// <summary>
        /// Gets the cached field name.
        /// </summary>
        public override string Name => _name ?? (_name = MethodInfo.Name);

        /// <summary>
        /// Gets the type context's member info.
        /// </summary>
        public override MemberInfo MemberInfo => MethodInfo;

        /// <summary>
        /// Gets a value indicating whether the System.Type is a value type.
        /// </summary>
        public bool IsValueType => _isValueType ?? ((bool)(_isValueType = TypeInfo.IsValueType));

        public ContextualParameterInfo[] ContextualParameters => _parameters ?? (_parameters = MethodInfo
            .GetParameters()
            .Select(p => p.ToContextualParameter())
            .ToArray());

        public override object? GetValue(object? obj)
        {
            throw new NotSupportedException("Cannot get the value of a method");
        }

        public override void SetValue(object? obj, object? value)
        {
            throw new NotSupportedException("Cannot set the value of a method");
        }
    }
}
