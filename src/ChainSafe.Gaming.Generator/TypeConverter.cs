namespace ChainSafe.Gaming.Generator
{
    /// <summary>
    /// Helper to convert solidity type to net type.
    /// </summary>
    public static class TypeConverter
    {
        /// <summary>
        /// Convert a solidity type to net type.
        /// </summary>
        /// <param name="typeName">solidity type.</param>
        /// <param name="outputArrayAsList">true if is an array type.</param>
        /// <returns>net type in string format.</returns>
        public static string Convert(this string typeName, bool outputArrayAsList = false)
        {
            int num = typeName.IndexOf("[");
            int numberOfArrays = 1;
            if (num > -1)
            {
                string typeName2 = typeName.Substring(0, num);
                if (outputArrayAsList)
                {
                    return GetListType(Convert(typeName2, true), numberOfArrays);
                }

                return GetArrayType(Convert(typeName2, false));
            }
            else
            {
                if (typeName == "bool")
                {
                    return GetBooleanType();
                }

                if (typeName.StartsWith("int"))
                {
                    if (typeName.Length == 3)
                    {
                        return GetBigIntegerType();
                    }

                    int num2 = int.Parse(typeName.Substring(3));
                    if (num2 > 64)
                    {
                        return GetBigIntegerType();
                    }

                    if (num2 <= 64 && num2 > 32)
                    {
                        return GetLongType();
                    }

                    if (num2 == 32)
                    {
                        return GetIntType();
                    }

                    if (num2 == 16)
                    {
                        return GetShortType();
                    }

                    if (num2 == 8)
                    {
                        return GetSByteType();
                    }
                }

                if (typeName.StartsWith("uint"))
                {
                    if (typeName.Length == 4)
                    {
                        return GetBigIntegerType();
                    }

                    int num3 = int.Parse(typeName.Substring(4));
                    if (num3 > 64)
                    {
                        return GetBigIntegerType();
                    }

                    if (num3 <= 64 && num3 > 32)
                    {
                        return GetULongType();
                    }

                    if (num3 == 32)
                    {
                        return GetUIntType();
                    }

                    if (num3 == 16)
                    {
                        return GetUShortType();
                    }

                    if (num3 == 8)
                    {
                        return GetByteType();
                    }
                }

                if (typeName == "address")
                {
                    return GetStringType();
                }

                if (typeName == "string")
                {
                    return GetStringType();
                }

                if (typeName == "bytes")
                {
                    return GetByteArrayType();
                }

                if (typeName.StartsWith("bytes"))
                {
                    return GetByteArrayType();
                }

                // object for unknown type like struct
                return "object";
            }
        }

        /// <summary>
        /// Return long type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetLongType()
        {
            return "long";
        }

        /// <summary>
        /// Return ulong type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetULongType()
        {
            return "ulong";
        }

        /// <summary>
        /// Return int type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetIntType()
        {
            return "int";
        }

        /// <summary>
        /// Return uint type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetUIntType()
        {
            return "uint";
        }

        /// <summary>
        /// Return short type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetShortType()
        {
            return "short";
        }

        /// <summary>
        /// Return ushort type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetUShortType()
        {
            return "ushort";
        }

        /// <summary>
        /// Return byte type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetByteType()
        {
            return "byte";
        }

        /// <summary>
        /// Return sbyte type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetSByteType()
        {
            return "sbyte";
        }

        /// <summary>
        /// Return byte array type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetByteArrayType()
        {
            return "byte[]";
        }

        /// <summary>
        /// Return string type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetStringType()
        {
            return "string";
        }

        /// <summary>
        /// Return bool type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetBooleanType()
        {
            return "bool";
        }

        /// <summary>
        /// Return BigInteger type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetBigIntegerType()
        {
            return "BigInteger";
        }

        /// <summary>
        /// Return array type.
        /// </summary>
        /// <returns>c# type.</returns>
        public static string GetArrayType(string type)
        {
            return type + "[]";
        }

        /// <summary>
        /// Return list type.
        /// </summary>
        /// <param name="type">c# array type.</param>
        /// <param name="numberOfArrays">array size.</param>
        /// <returns>c# type.</returns>
        public static string GetListType(string type, int numberOfArrays = 1)
        {
            string text = type;
            for (int i = 0; i < numberOfArrays; i++)
            {
                text = "List<" + text + ">";
            }

            return text;
        }
    }
}
