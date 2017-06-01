﻿using System;
using System.Collections.Generic;

namespace MobileMart.DB.ExtentionMethods
{
    public static class ExtentionMethods
    {
        public static string ToConcatenatedString(
            this List<string> list
            , string separator)
        {
            return String.Join(separator, list);
        }

        public static string ToConcatenatedString(
            this string[] list
            , string separator)
        {
            return String.Join(separator, list);
        }
    }
}