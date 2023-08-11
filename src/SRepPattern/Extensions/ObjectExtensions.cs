using System.Reflection;

namespace SRepPattern.Extensions;

public static class ObjectExtensions
{
    public static string? GetPropertyValue<T>(this T obj, string propertyName)
    {
        var property = obj!.GetType().GetProperty(propertyName);
        var value = property is null ? null: property!.GetValue(obj)!;
        return value is null ? null : value.ToString();
    }

    public static object? GetPropertyToObject<T>(this T obj, string propertyName) where T : class
    {
        var property = obj!.GetType().GetProperty(propertyName);
        var value = property is null ? null: property.GetValue(obj);
        return value!;
    }

    public static void SetPropertyValue<T>(this T obj, string propertyName, object? value)
    {
        var property = obj!.GetType().GetProperty(propertyName);
        if(property is not null)
            property!.SetValue(obj, value, null);
    }

    public static PropertyInfo? GetProperty<T>(this T obj, string propertyName)
    {
        var type = obj!.GetType();
        return type.GetProperty(propertyName);
    }
}
