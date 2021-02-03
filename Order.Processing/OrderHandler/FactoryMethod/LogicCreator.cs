using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Hangfire.Logging;

namespace Order
{
    public class LogicCreator : ILogicCreator
    {
        private static readonly Dictionary<string, IOrderHandler> orderHandlers = new Dictionary<string, IOrderHandler>();
        static LogicCreator()
        {

            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(s => typeof(IOrderHandler).IsAssignableFrom(s) && Attribute.IsDefined(s,typeof(TypeNameAttribute)));

            foreach (var type in types)
            {
                var systemType = ((TypeNameAttribute) Attribute.GetCustomAttribute(type, typeof(TypeNameAttribute)))
                    .SystemType;
                if(string.IsNullOrEmpty(systemType))
                    throw new Exception("TypeNameAttribute value is not defined");
                orderHandlers.Add(systemType,(IOrderHandler)Activator.CreateInstance(type));
            }
        }
        public IOrderHandler CreateLogic(Order order)
        {
            return orderHandlers[order.SystemType];
        }
    }
}