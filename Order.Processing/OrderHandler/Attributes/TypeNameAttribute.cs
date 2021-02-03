using System;

namespace Order
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class TypeNameAttribute : Attribute
    {
        private string _systemType;
        public TypeNameAttribute(string systemType)
        {
            _systemType = systemType;
        }

        public string SystemType => _systemType;
    }
}