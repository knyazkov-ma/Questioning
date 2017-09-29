using System;

namespace Questioning.Helpers
{
    public static class StringExtensions
    {
        public static string GetStringConstraintMsg(this string val, bool notEmpty, int maxLength, int minLength)
        {
            if (notEmpty)
            {
                if (val == null || val.Trim() == "")
                    return Resource.Text_EmptyConstraintMsg;
                else if (val.Length > maxLength)
                    return String.Format(Resource.Text_LengthMaxConstraintMsg, val.Length, maxLength);
                else if (minLength > 0 && val.Length < minLength)
                    return String.Format(Resource.Text_LengthMinConstraintMsg, val.Length, minLength);
            }
            else
            {
                if (val != null && val.Trim() != "" && val.Length > maxLength)
                    return String.Format(Resource.Text_LengthMaxConstraintMsg, val.Length, maxLength);
                else if (val != null && val.Trim() != "" && minLength > 0 && val.Length < minLength)
                    return String.Format(Resource.Text_LengthMinConstraintMsg, val.Length, minLength);
            }

            return null;
        }

    }
}
