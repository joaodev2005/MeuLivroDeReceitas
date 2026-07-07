namespace MyRecipeBook.Domain.Extensions;

public static class StringExtension
{
    public static bool IsNotEmpty(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static bool IsEmpty(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
}
