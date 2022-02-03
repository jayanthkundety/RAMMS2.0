using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RAMMS.Business.ServiceProvider;
using RAMMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace RAMMS.Web.UI
{
    public static class CHTMLControls
    {

        public static object GetHtmlAttribute(string id, string className, bool IsEnabled)
        {
            return IsEnabled ? new { @id = id, @class = className } as object : new { @id = id, @class = className, @disabled = "disabled" } as object;
        }        
        public static string GetUserDropdownFormat(int? id, string name)
        {
            return !id.HasValue || id == 0 ? "" : (id == 99999999 ? id + "-Others" : id + "-" + name);
        }
        public static IHtmlContent DropDownList(this IHtmlHelper html, IEnumerable<CSelectListItem> selList, string expression, string optionLabel, object htmlAttributes, string valueCol = "")
        {
            var pinfo = htmlAttributes.GetType().GetProperties();
            string key, val = string.Empty;
            StringBuilder obj = new StringBuilder();
            obj.Append("<select name=\"" + expression + "\"");
            string strOption = string.Empty;
            foreach (var prop in pinfo)
            {
                key = prop.Name;
                val = Utility.ToString(prop.GetValue(htmlAttributes));
                obj.Append(" " + key + "=\"" + val + "\"");
            }
            obj.Append(">");
            if (!string.IsNullOrEmpty(optionLabel))
            {
                obj.Append("<option value=''>" + optionLabel + "</option>");
            }
            if (selList.Count() > 0)
            {
                var lstGroup = selList.Where(x => !string.IsNullOrEmpty(x.Group)).Select(x => x.Group).Distinct();
                if (lstGroup.Count() > 0 && 1 == 2)
                {
                    lstGroup = lstGroup.OrderBy(x => x);
                    foreach (string gname in lstGroup)
                    {
                        strOption = "<optgroup label=\"" + gname + "\">";
                        selList.Where(x => x.Group == gname).ToList().ForEach((CSelectListItem item) =>
                        {
                            strOption += "<option cvalue=\"" + item.CValue + "\"";
                            switch (valueCol)
                            {
                                case "Item1":
                                    strOption += " value=\"" + item.Item1 + "\"";
                                    break;
                                case "Item2":
                                    strOption += " value=\"" + item.Item2 + "\"";
                                    break;
                                case "Item3":
                                    strOption += " value=\"" + item.Item3 + "\"";
                                    break;
                                case "Code":
                                    strOption += " value=\"" + item.Code + "\"";
                                    break;
                                case "CValue":
                                    strOption += " value=\"" + item.CValue + "\"";
                                    break;
                                case "FromKm":
                                    strOption += " value=\"" + item.FromKm + "\"";
                                    break;
                                case "FromM":
                                    strOption += " value=\"" + item.FromM + "\"";
                                    break;
                                case "ToKm":
                                    strOption += " value=\"" + item.ToKm + "\"";
                                    break;
                                case "ToM":
                                    strOption += " value=\"" + item.ToM + "\"";
                                    break;
                                default:
                                    strOption += " value=\"" + item.Value + "\"";
                                    break;
                            }
                            strOption += string.IsNullOrEmpty(item.Item1) ? "" : " item1=\"" + item.Item1 + "\"";
                            strOption += string.IsNullOrEmpty(item.Item2) ? "" : " item2=\"" + item.Item2 + "\"";
                            strOption += string.IsNullOrEmpty(item.Item3) ? "" : " item3=\"" + item.Item3 + "\"";
                            strOption += string.IsNullOrEmpty(item.Code) ? "" : " Code=\"" + item.Code + "\"";
                            strOption += item.PKId == 0 ? "" : " pid=\"" + item.PKId.ToString() + "\"";
                            strOption += item.FromKm == 0 ? " FromKm=0" : " FromKm=\"" + item.FromKm + "\"";
                            strOption += string.IsNullOrEmpty(item.FromM) ? "" : " FromM=\"" + item.FromM + "\"";
                            strOption += item.ToKm == 0 ? " Tokm=0" : " ToKm=\"" + item.ToKm + "\"";
                            strOption += string.IsNullOrEmpty(item.ToM) ? "" : " ToM=\"" + item.ToM + "\"";
                            strOption += ">" + item.Text + "</option>";
                        });
                        strOption += "</optgroup>";
                        obj.Append(strOption);
                    }
                }
                else
                {
                    foreach (var item in selList)
                    {
                        strOption = "<option cvalue=\"" + item.CValue + "\"";
                        if (item.Selected) { strOption += " selected=\"selected\" "; }
                        switch (valueCol)
                        {
                            case "Item1":
                                strOption += " value=\"" + item.Item1 + "\"";
                                break;
                            case "Item2":
                                strOption += " value=\"" + item.Item2 + "\"";
                                break;
                            case "Item3":
                                strOption += " value=\"" + item.Item3 + "\"";
                                break;
                            case "Code":
                                strOption += " value=\"" + item.Code + "\"";
                                break;
                            case "CValue":
                                strOption += " value=\"" + item.CValue + "\"";
                                break;
                            case "FromKm":
                                strOption += " value=\"" + item.FromKm + "\"";
                                break;
                            case "FromM":
                                strOption += " value=\"" + item.FromM + "\"";
                                break;
                            case "ToKm":
                                strOption += " value=\"" + item.ToKm + "\"";
                                break;
                            case "ToM":
                                strOption += " value=\"" + item.ToM + "\"";
                                break;
                            default:
                                strOption += " value=\"" + item.Value + "\"";
                                break;
                        }
                        strOption += string.IsNullOrEmpty(item.Item1) ? "" : " item1=\"" + item.Item1 + "\"";
                        strOption += string.IsNullOrEmpty(item.Item2) ? "" : " item2=\"" + item.Item2 + "\"";
                        strOption += string.IsNullOrEmpty(item.Item3) ? "" : " item3=\"" + item.Item3 + "\"";
                        strOption += string.IsNullOrEmpty(item.Code) ? "" : " Code=\"" + item.Code + "\"";
                        strOption += item.PKId == 0 ? "" : " pid=\"" + item.PKId.ToString() + "\"";
                        strOption += item.FromKm == 0 ? " FromKm=0" : " FromKm=\"" + item.FromKm + "\"";
                        strOption += string.IsNullOrEmpty(item.FromM) ? "" : " FromM=\"" + item.FromM + "\"";
                        strOption += item.ToKm == 0 ? " ToKm=0" : " ToKm=\"" + item.ToKm + "\"";
                        strOption += string.IsNullOrEmpty(item.ToM) ? "" : " ToM=\"" + item.ToM + "\"";
                        strOption += ">" + item.Text + "</option>";
                        obj.Append(strOption);
                    }
                }
            }
            obj.Append("</select>");
            return html.Raw(obj.ToString());
        }
    }
}
