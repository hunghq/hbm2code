using System;

namespace Hbm2Code
{
    public class AttributeMapper
    {
        public AttributeMapper(string methodName = null, bool isStringValue = false, Func<Property, string, string> valueMapper = null)
            : this(methodName == null ? (Func<Property, string>)null : (_ => methodName), isStringValue, valueMapper)
        {
        }

        public AttributeMapper(Func<Property, string> methodNameMapper, bool isStringValue = false, Func<Property, string, string> valueMapper = null)
        {
            MethodNameMapper = methodNameMapper;
            IsStringValue = isStringValue;
            ValueMapper = valueMapper;
        }

        public Func<Property, string> MethodNameMapper { get; }
        public Func<Property, string, string> ValueMapper { get; }
        public bool IsStringValue { get; }
    }
}