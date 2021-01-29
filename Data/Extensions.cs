using System;
using System.ComponentModel;
using System.Linq;

namespace SOATApiReact.Data
{
    public static class Extensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item) where TEnum : struct
        {
            return item.GetType()
                .GetField(item.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;
        } 
    }
}