namespace brief.Library.Helpers
{
    using System;

    public static class Guard
    {
        public static void AssertNotNull<T>(T subject, string name)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
