using System.ComponentModel.DataAnnotations;

namespace Assessment3.Shared.Attributes;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            // Check if the DateTime value is greater than the current date and time
            return dateTime > DateTime.Now;
        }

        return false; // Return false for non-DateTime values
    }
}