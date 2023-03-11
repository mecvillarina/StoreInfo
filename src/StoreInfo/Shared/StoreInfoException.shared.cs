﻿using System;

namespace Plugin.StoreInfo
{
    internal class StoreInfoException : Exception
    {
        public StoreInfoException(string message)
            : base(message)
        {
        }

        public StoreInfoException(Exception innerException)
            : base("", innerException)
        {
        }

        public StoreInfoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
