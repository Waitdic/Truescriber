using System;
using System.Globalization;

namespace Truescriber.Common.Helpers
{
    public static class TaskHelper
    {
        public static string BytesToSizeString(long taskSize)
        {
            double size = taskSize;
            string[] format = { "B", "KB", "MB", "GB" };
            for (var i = 0; i < format.Length; i++)
            {
                if (size / 1024 == 0 || i == 2)
                {
                    size = Math.Round(size, 1);
                    return size.ToString(CultureInfo.InvariantCulture) + " " + format[i];
                }

                size /= 1024;
            }
            return null;
        }
    }
}
