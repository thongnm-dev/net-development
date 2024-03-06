using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Net.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetValueString<T>(this T value) where T : struct, IConvertible
        {
            var intance = typeof(T);

            var member = intance.GetMember(value.ToString()?? "").FirstOrDefault();

            if (member == null) return "";

            var memberAttribute = member.GetCustomAttributes(false)?.OfType<EnumMemberAttribute>().FirstOrDefault();
            if (memberAttribute == null) return "";

            return memberAttribute.Value?? "";
        }
    }
}
