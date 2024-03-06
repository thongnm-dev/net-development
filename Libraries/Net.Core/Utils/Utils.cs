using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Net.Core.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Check character is phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string phoneNumber, string pattern = "")
        {
            return Regex.IsMatch(phoneNumber, pattern);
        }

        /// <summary>
        /// Check character is email
        /// </summary>
        /// <param name="iEmail"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsEmail(string iEmail, string pattern = "")
        {
            return Regex.IsMatch(iEmail, pattern);
        }
    }
}
