using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public static class ParameterUtil
    {
        public static void CheckArgument(bool expression)
        {
            if (!expression)
            {
                throw new ArgumentException();
            }
        }

        public static void CheckArgument(bool expression, object errorMessage)
        {
            if (!expression)
            {
                throw new ArgumentException(errorMessage.ToString());
            }
        }

        public static void CheckArgument(bool expression, string errorMessageTemplate,
            object errorMessageArgs)
        {
            if (!expression)
            {
                var message = string.Format(errorMessageTemplate, errorMessageArgs);
                throw new ArgumentException(message, errorMessageArgs.ToString());
            }
        }

        public static void CheckState(bool expression)
        {
            if (!expression)
            {
                throw new Exception();
            }
        }

        public static void CheckState(bool expression, object errorMessage)
        {
            if (!expression)
            {
                throw new Exception(errorMessage.ToString());
            }

        }

        public static void CheckState(bool expression, string errorMessageTemplate, object errorMessageArgs)
        {
            if (!expression)
            {
                var message = string.Format(errorMessageTemplate, errorMessageArgs);
                throw new Exception(message +"; parameter:" + errorMessageArgs.ToString());
            }
        }

        public static T CheckNotNull<T>(T reference)
        {
            if (reference == null)
            {
                throw new NullReferenceException();
            }
            return reference;
        }

        public static T CheckNotNull<T>(T reference, object errorMessage)
        {
            if (reference == null)
            {
                throw new NullReferenceException(errorMessage.ToString());
            }
            return reference;
        }

        public static T CheckNotNull<T>(T reference, string errorMessageTemplate, object errorMessageArgs)
        {
            if (reference == null)
            {
                var message = string.Format(errorMessageTemplate, errorMessageArgs);
                throw new NullReferenceException(message + "; parameter:" + errorMessageArgs.ToString());
            }
            return reference;
        }

    }
}
