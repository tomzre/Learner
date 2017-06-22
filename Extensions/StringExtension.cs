using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;


namespace Learner.Extensions
{
    public static class StringExtension
    {
        public static bool NotEmpty(this string value) => !value.Empty();

        public static bool Empty(this string value) => string.IsNullOrWhiteSpace(value);

        public static bool IsEmail(this string emailAddress)
        {
            if (emailAddress.NotEmpty())
            {
                try
                {
                    MailAddress m = new MailAddress(emailAddress);
                    return true;
                }
                catch(FormatException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
    }
}