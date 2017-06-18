namespace brief.Library.Helpers
{
    using System;

    public static class StringExtensions
    {
        public static T ConvertToEnum<T>(this string input) where T : struct 
            => (T)Enum.Parse(typeof(T), input);
    }
}
