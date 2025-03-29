using System;
using System.Reflection;

public class ReflectionHelper {
    public static void SetPrivateField(Type type, object instance, string fieldName, object value) {
        BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                 | BindingFlags.Static;
        FieldInfo field = type.GetField(fieldName, bindFlags);

        // Execution
        field.SetValue(instance, value);
    }

    // Update is called once per frame
    void Update() { }
}
