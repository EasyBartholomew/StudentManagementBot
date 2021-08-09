using System;
using System.Text;


namespace StudentManagementBot.Data.ObjectModel.Implementation.Helpers
{
    internal static class TextFormatHelper
    {
        public static string FormatPersonalData(this string personalData)
        {
            if (personalData == null)
                throw new ArgumentNullException(nameof(personalData));

            if (string.IsNullOrWhiteSpace(personalData))
                throw new ArgumentException();

            var sb = new StringBuilder(personalData.Trim());
            sb[0] = char.ToUpper(sb[0]);

            return personalData.ToString();
        }
    }
}
