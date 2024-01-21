using System.ComponentModel;
using System.Dynamic;

namespace BookStore.Common.Extensions;

public static class ObjectExtensions
{
    public static IDictionary<string, object> ToDictionary(this object obj)
    {
        if (obj is null) return new Dictionary<string, object>();

        return TypeDescriptor.GetProperties(obj)
            .OfType<PropertyDescriptor>()
            .ToDictionary(
                prop => prop.Name,
                prop => prop.GetValue(obj)
            );
    }

    public static object Merge(this object destination, object source)
    {
        var dicSource = source.ToDictionary();
        var dicDest = destination.ToDictionary();
        var result = new ExpandoObject();
        var dicResult = (IDictionary<string, object>)result;

        foreach (var pair in dicSource.Concat(dicDest))
        {
            dicResult[pair.Key] = pair.Value;
        }

        return result;
    }

    public static bool HasProperty(this object obj, string propertyName)
    {
        var data = obj as ExpandoObject;
        if (data != null)
        {
            return ((IDictionary<string, object>)obj).ContainsKey(propertyName);
        }

        return obj.ToDictionary().ContainsKey(propertyName);
    }

    public static dynamic ToDynamic(this object value)
    {
        IDictionary<string, object> expando = new ExpandoObject();

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
        {
            expando.Add(property.Name, property.GetValue(value));
        }

        return (ExpandoObject)expando;
    }

    public static bool IsNull(this object obj) => obj == null;

    public static bool IsNotNull(this object obj) => obj != null;

    public static bool IsNotNullOrEmpty(this object obj) =>
        obj.IsNotNull() && obj.ToString().IsNotEmpty();    
}

