using System;
using System.Collections.Generic;
using System.Text;
namespace RAMMS.Common.RefNumber
{
    public class FormRefNumber
    {
        public const string NewRunningNumber = "????";
        public const string FormS1Header = "MM/Form S1/{RMU}/{Date}/{WeekNo}/{" + NewRunningNumber + "}";
        public const string FormS1Detail = "MM/Form S1/{RMU}/{Date}/{WeekNo}/{S1PKID}/{" + NewRunningNumber + "}";
        public const string FormS2Header = "MM/Form S2/{RMU}/{Quarter}/{Year}/{ActCode}/{" + NewRunningNumber + "}";
        public const string FormDHeader = "ERT/FORM D/{WeekNo}-{MonthNo}-{Year}/{CrewUnit}/{" + NewRunningNumber + "}";
        public const string FormC1C2 = "CI/Form C1/C2/{AssetID}/{Year}";
        public const string FormF4Header = "CI/Form F4/{RoadCode}/{Year}";
        public const string FormF5Header = "CI/Form F5/{RoadCode}/{Year}";
        public const string FormFCHeader = "CI/Form FC/{RoadCode}/{Year}";
        public const string FormB1B2 = "CI/Form B1/B2/{AssetID}/{Year}";
        public const string FormFDHeader = "CI/Form FD/{RoadCode}/{Year}";
        public static string GetRefNumber(FormType type, IDictionary<string, string> values)
        {
            string format = GetFormat(type);
            foreach (var item in values)
            {
                format = format.Replace("{" + item.Key + "}", item.Value);
            }
            return format;
        }
        public static string GetRawRefNumber(FormType type)
        {
            string format = GetFormat(type);
            return format.Replace("{" + NewRunningNumber + "}", NewRunningNumber);
        }
        private static string GetFormat(FormType type)
        {
            string format = string.Empty;
            switch (type)
            {
                case FormType.FormS1Header:
                    format = FormS1Header;
                    break;
                case FormType.FormS1Details:
                    format = FormS1Detail;
                    break;
                case FormType.FormS2Header:
                    format = FormS2Header;
                    break;
                case FormType.FormDHeader:
                    format = FormDHeader;
                    break;
                case FormType.FormC1C2:
                    format = FormC1C2;
                    break;
                case FormType.FormF4Header:
                    format = FormF4Header;
                    break;
                case FormType.FormF5Header:
                    format = FormF5Header;
                    break;
                case FormType.FormFCHeader:
                    format = FormFCHeader;
                    break;
                case FormType.FormB1B2:
                    format = FormB1B2;
                    break;
                case FormType.FormFDHeader:
                    format = FormFDHeader;
                    break;
            }
            return format;
        }
    }
    public enum FormType
    {
        FormS1Header,
        FormS1Details,
        FormS2Header,
        FormDHeader,
        FormB1B2,
        FormC1C2,
        FormF4Header,
        FormF5Header,
        FormFCHeader,
        FormFDHeader
    }

}

