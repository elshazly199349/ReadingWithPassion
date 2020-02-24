using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.Utilities.CustomValidations
{
    public class ValidPhoneNumber:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var phoneValue = value.ToString();
            if (phoneValue.Length==11)
            {
                if (phoneValue.Substring(0,3)=="010" || phoneValue.Substring(0, 3) == "011" || phoneValue.Substring(0, 3) == "012")
                {
                    return true;
                }
                return false;
            }
            else if (phoneValue.Length == 12)
            {
                if (phoneValue.Substring(0, 4) == "2010" || phoneValue.Substring(0, 4) == "2011" || phoneValue.Substring(0, 4) == "2012")
                {
                    return true;
                }
                return false;
            }
            else if (phoneValue.Length==14)
            {
                if (phoneValue.Substring(0, 6) == "002010" || phoneValue.Substring(0, 6) == "002011" || phoneValue.Substring(0, 6) == "002012")
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
