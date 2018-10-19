using System;
using System.Collections.Generic;

namespace Library.Utils
{
    public class EnumUtils
    {
        public IList<IntIdStringNamePair> GetValueNameList<T>()
        {
            int[] values = (int[])Enum.GetValues(typeof(T));
            string[] names = Enum.GetNames(typeof(T));
            List <IntIdStringNamePair> result = new List<IntIdStringNamePair>();
            for (int i = 0; i < values.Length; i++)
            {
                result.Add(new IntIdStringNamePair(values[i], names[i]));
            }
            return result;
        }
    }
}
