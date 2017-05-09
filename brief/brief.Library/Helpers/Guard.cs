namespace brief.Library.Helpers
{
    using System;

    public static class Guard
    {
        public static void AssertNotNull<T>(T subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }
        }
    }
}
