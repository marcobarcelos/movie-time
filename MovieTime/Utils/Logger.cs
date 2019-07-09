using System;
using System.Diagnostics;

namespace MovieTime.Utils
{
    public static class Logger
    {
        public static void LogError(Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }
}

