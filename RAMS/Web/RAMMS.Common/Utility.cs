using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RAMMS.Common
{
    public class Utility
    {
        public static string NullIntToString(int? value)
        {
            return value.HasValue ? value.Value.ToString() : "";
        }
        public static string ToString(DateTime? value, string format)
        {
            if (value.HasValue) { return value.Value.ToString(format, new System.Globalization.CultureInfo("en-US")); }
            else { return string.Empty; }
        }
        /// <summary>
        /// Convert from object to string
        /// </summary>
        /// <param name="value">object value</param>
        /// <returns>string</returns>
        public static string ToString(object value)
        {
            if (value == null)
                return string.Empty;
            else
                return Convert.ToString(value);
        }
        public static string ToString(object value, string defaultValue)
        {
            if (value == null)
                return defaultValue;
            else
                return Convert.ToString(value);

        }
        public static string ToSQLString(object value)
        {
            if (value == null)
                return string.Empty;
            else
                return Convert.ToString(value).Replace("'", "''");
        }
        public static string ToSQLQueryString(object value)
        {
            if (value == null)
                return string.Empty;
            else
                return Convert.ToString(value).Replace("[", "[[]");
        }
        /// <summary>
        /// Convert from object to integer
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <returns>integer</returns>
        public static int ToInt(object value)
        {
            return ToInt(ToString(value).Trim());
        }
        public static int? ToNullInt(object value)
        {
            return ToInt(ToString(value).Trim());
        }
        /// <summary>
        /// Convert from string value to integer
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <returns>integer</returns>
        public static int ToInt(string value)
        {
            int iVal = 0;
            if (value != null && value.Length > 0)
            {
                int.TryParse(value, out iVal);
            }
            return iVal;
        }

        /// <summary>
        /// Covert Object to Bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(object value)
        {
            bool iBool = false;
            string strVal = ToString(value);
            if (strVal == "on" || strVal == "1") { return true; }
            else if (bool.TryParse(strVal, out iBool))
            { return iBool; }
            else { return false; }

        }

        /// <summary>
        /// Convert Int to Bool
        /// </summary>
        /// <param name="iValue"></param>
        /// <returns></returns>
        public static bool ToBool(int iValue)
        {
            if (iValue == 1) { return true; }
            else { return false; }
        }


        /// <summary>
        /// Covert Object to Date Time
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(object value)
        {
            DateTime iDate;
            if (DateTime.TryParse(Utility.ToString(value), out iDate))
            {
                return iDate;
            }
            else
            {
                return null;
            }
        }
        public static DateTime? ToDateTime(string date, string format)
        {
            try
            {
                return DateTime.ParseExact(date, format, null);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Convert object to Long datatype 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(object value)
        {
            return ToLong(ToString(value));
        }

        /// <summary>
        /// Convert string to Long datatype 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(string value)
        {
            long iVal = 0;
            if (value != null && value.Length > 0)
            {
                Int64.TryParse(value, out iVal);
            }
            return iVal;
        }

        /// <summary>
        /// Convert object to float datatype 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToFloat(object value)
        {
            return ToFloat(ToString(value));
        }

        /// <summary>
        /// Convert object to string datatype 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToFloat(string value)
        {
            float iVal = 0;
            if (value != null && value.Length > 0)
            {
                float.TryParse(value, out iVal);
            }
            return iVal;
        }


        /// <summary>
        /// Convert String to Decimal Value with 2 default decimal point after "." DOT
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(object value, int decimals = 2)
        {
            Decimal iVal = 0;
            if (value != null && value != "")
            {
                if (Decimal.TryParse(ToString(value), out iVal))
                {
                    iVal = Decimal.Round(iVal, decimals);
                }
            }
            return iVal;
        }

        /// <summary>
        /// Convert String to Decimal Value with 2 default decimal point after "." DOT
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(object value)
        {
            Decimal iVal = 0;
            if (value != null && value != "")
            {
                Decimal.TryParse(ToString(value), out iVal);
            }
            return iVal;
        }
        public static JsonSerializerOptions JsonOption
        {
            get
            {
                return new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = null
                };
            }
        }
        public static string JSerialize(object model)
        {
            return System.Text.Json.JsonSerializer.Serialize(model, JsonOption);
        }
        public static T JDeSerialize<T>(string model)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(model, JsonOption);
        }
        public static List<string> GetAlphabets(int SeqNumber)
        {
            List<string> lstResult = new List<string>();
            string strData = string.Empty;
            for (var seq = 0; seq < SeqNumber; seq++)
            {
                strData = seq == 0 ? "" : ((char)(seq + 64)) + "";
                for (int i = 65; i <= 90; i++)
                {
                    lstResult.Add(strData + ((char)i));
                }
            }
            return lstResult;
        }
        public const string GridDateFormat = "dd/MM/yyyy";

        public static List<Dictionary<string, object>> ProcessLog(string logs)
        {
            if (!string.IsNullOrEmpty(logs))
                return Utility.JDeSerialize<List<Dictionary<string, object>>>(logs);
            return new List<Dictionary<string, object>>();
        }

        public static string ProcessLog(string logs, string title, string Status, string ApprovedBy, string Comments, DateTime? ApproveDate, string CreatedBy)
        {
            List<Dictionary<string, object>> lstLog = string.IsNullOrEmpty(logs) ? new List<Dictionary<string, object>>() : Utility.JDeSerialize<List<Dictionary<string, object>>>(logs);
            lstLog.Add(Utility.ProcessLog(title, Status, ApprovedBy, Comments, ApproveDate, CreatedBy));
            return JSerialize(lstLog);
        }
        public static Dictionary<string, object> ProcessLog(string title, string Status, string ApprovedBy, string Comments, DateTime? ApproveDate, string CreatedBy)
        {
            Dictionary<string, object> log = new Dictionary<string, object>();
            log.Add("title", title);
            log.Add("Status", Status);
            log.Add("AppBy", ApprovedBy);
            log.Add("Comments", Comments);
            log.Add("AppDt", ApproveDate);
            log.Add("LogDt", DateTime.Now);
            log.Add("LogBy", CreatedBy);
            return log;
        }
    }
}
