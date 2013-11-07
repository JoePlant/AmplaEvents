using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AmplaEvents.Aggregator.Dispatcher
{
    internal static class TypeHelper
    {
        internal static IEnumerable<Type> GetBaseInterfaceType(Type type)
        {
            if (type == null)
                return new Type[0];

               var interfaces = type.GetInterfaces().ToList();

            foreach (var @interface in interfaces.ToArray())
            {
                interfaces.AddRange(GetBaseInterfaceType(@interface));
            }

            if (type.IsInterface)
            {
                interfaces.Add(type);
            }

            return interfaces.Distinct();
        }

        internal static bool DirectlyClosesGeneric(Type type, Type openType)
        {
            if (type == null)
                return false;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == openType)
            {
                return true;
            }

            return false;
        }

        internal static Type GetFirstGenericType<T>() where T : class
        {
            return GetFirstGenericType(typeof(T));
        }

        internal static Type GetFirstGenericType(Type type)
        {
            var messageType = type.GetGenericArguments().First();
            return messageType;
        }

        internal static MethodInfo GetMethod(Type type, string methodName)
        {
            var handleMethod = type.GetMethod(methodName);
            return handleMethod;
        }

        internal static bool IsAssignableFrom(Type type, Type specifiedType)
        {
            return type.IsAssignableFrom(specifiedType);
        }
    }
}