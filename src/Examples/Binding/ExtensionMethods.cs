namespace Binding;

public static class ExtensionMethods
{
    public static string Debug(this object value)
    {
        return $"Type: {value.GetType().Name}, Value: {value}";
    }
}