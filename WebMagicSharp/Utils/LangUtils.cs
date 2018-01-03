using System;
namespace WebMagicSharp.Utils
{
    public static class LangUtils
    {
        public static int HASH_SEED = 17;
        public static int HASH_OFFSET = 37;

        /** Disabled default constructor. */

        public static int HashCode(int seed, int hashcode)
        {
            return seed * HASH_OFFSET + hashcode;
        }

        public static int HashCode(int seed, bool b)
        {
            return HashCode(seed, b ? 1 : 0);
        }

        public static int HashCode(int seed, object obj)
        {
            return HashCode(seed, obj != null ? obj.GetHashCode() : 0);
        }

        /**
         * Check if two objects are equal.
         *
         * @param obj1 first object to compare, may be {@code null}
         * @param obj2 second object to compare, may be {@code null}
         * @return {@code true} if the objects are equal or both null
         */
        public new static bool Equals(object obj1, object obj2)
        {
            return obj1 == null ? obj2 == null : obj1.Equals(obj2);
        }

        /**
         * Check if two object arrays are equal.
         * <ul>
         * <li>If both parameters are null, return {@code true}</li>
         * <li>If one parameter is null, return {@code false}</li>
         * <li>If the array lengths are different, return {@code false}</li>
         * <li>Compare array elements using .equals(); return {@code false} if any comparisons fail.</li>
         * <li>Return {@code true}</li>
         * </ul>
         *
         * @param a1 first array to compare, may be {@code null}
         * @param a2 second array to compare, may be {@code null}
         * @return {@code true} if the arrays are equal or both null
         */
        public static bool Equals(object[] a1, object[] a2)
        {
            if (a1 == null)
            {
                return a2 == null;
            }
            else
            {
                if (a2 != null && a1.Length == a2.Length)
                {
                    for (int i = 0; i < a1.Length; i++)
                    {
                        if (!Equals(a1[i], a2[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
