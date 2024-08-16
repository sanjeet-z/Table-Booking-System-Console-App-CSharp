using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BAL.Validations
{
    public class TableBookingValidations
    {
        public static bool IsValidName(string name, int minLength, int maxLength)
        {
            if (name.IsNullOrEmpty())
            {
                return false;
            }
            else if (name.Length < minLength || name.Length > maxLength)
            {
                return false;
            }
            else if (!Regex.IsMatch(name, "[^a-zA-Z ]+"))
            {
                return true;
            }
            return false;
        }
        public static bool IsValidDate(string date)
        {
            if (date.Count(c => c == '/') == 2)
            {
                bool isValidFormat1 = DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _);

                if (isValidFormat1)
                {
                    DateTime inputDate = DateTime.Parse(date);
                    DateTime today = DateTime.Now;
                    DateTime nextMonth = DateTime.Today.AddMonths(1);

                    if (today > inputDate || nextMonth < inputDate)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidNumber(int number, int lowerLimit, int upperLimit)
        {
            if (number >= lowerLimit && number <= upperLimit)
            {
                return true;
            }
            return false;
        }
        public static bool IsValidEmail(string email)
        {
            if (email.IsNullOrEmpty())
            {
                return false;
            }
            else if (Regex.IsMatch(email, "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$"))
            {
                return true;
            }
            return false;
        }
        public static bool IsValidPhoneNumber(long phoneNumberr)
        {
            string phoneNumber = phoneNumberr.ToString();
            if (phoneNumber.IsNullOrEmpty())
            {
                return false;
            }
            else if (Regex.IsMatch(phoneNumber, "^[0-9]{10}$"))
            {
                return true;
            }
            return false;
        }
        public static bool IsValidOccassionType(char type)
        {
            if (type == 'B' || type == 'A' || type == 'R' || type == 'O' || type == 'b' || type == 'a' || type == 'r' || type == 'o')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidBookingType(char type)
        {
            if (type == 'B' || type == 'L' || type == 'D' || type == 'b' || type == 'l' || type == 'd')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidPaymentMode(string type)
        {
            if (type == "C" || type == "CC" || type == "G" || type == "c" || type == "cc" || type == "g")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsValidCoupon(string coupon)
        {
            if (Regex.IsMatch(coupon.ToUpper(), "^[A-Z]{2}\\d{2}[A-Z]\\d$"))
            {
                return true;
            }
            return false;
        }
    }
}
