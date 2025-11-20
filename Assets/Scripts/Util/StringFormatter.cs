using System;
using System.Reflection;
using System.Text.RegularExpressions;

public static class StringFormatter {
    public static string FormatWithObject(string template, object obj) {
        if (obj == null)
            return template;

        // Pattern to match {fieldName} or {propertyName}
        string pattern = @"\{(\w+)\}";

        return Regex.Replace(template, pattern, match => {
            string memberName = match.Groups[1].Value;
            Type type = obj.GetType();

            // Try to get as a property first
            PropertyInfo property = type.GetProperty(memberName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (property != null) {
                object value = property.GetValue(obj);
                return value?.ToString() ?? "null";
            }

            // Try to get as a field
            FieldInfo field = type.GetField(memberName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null) {
                object value = field.GetValue(obj);
                return value?.ToString() ?? "null";
            }

            // If neither found, leave the placeholder unchanged
            return match.Value;
        });
    }
}
