using System;
using System.Reflection;

namespace SimpleHTTP.Tests.Editor {
    public static class ReflectionHelper {
        public static void SetPrivateField(Type type, object instance, string fieldName, object value) {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                     | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);

            // Execution
            field.SetValue(instance, value);
        }
    }
}
