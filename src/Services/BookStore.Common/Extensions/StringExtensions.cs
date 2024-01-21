namespace BookStore.Common.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static T ToEnum<T>(this string value, string defaultValue = null) where T : struct
    {
        if (value.IsEmpty() && defaultValue.IsEmpty()) return default(T);

        T result;
        return !Enum.TryParse(value.GetValueOrDefault(defaultValue), true, out result) ? default(T) : result;
    }

    public static string GetValueOrDefault(this string str, string defaultValue)
    {
        return str.IsNotNullOrEmpty() ? str : defaultValue;
    }

    public static string GetValueOrDefault(this string str, Func<string> defaultValue)
    {
        return str.IsNotEmpty() ? str : defaultValue();
    }

    public static string ToEnumName<TEnum>(this string value)
    {
        return Enum.GetName(typeof(TEnum), value.ToInt());
    }    

    public static bool IsNotEmpty(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    public static int ToInt(this string value)
    {
        if (!int.TryParse(value, out var number))
        {
            number = 0;
        }

        return number;
    }    
}
