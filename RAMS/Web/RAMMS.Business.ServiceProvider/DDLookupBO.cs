using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Common;
///using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System.Threading.Tasks;
using RAMS.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.Serialization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RAMMS.Business.ServiceProvider
{
    public interface IDDLookupBO
    {
       // RmDdLookup ddLookup(RmDdLookup _RmDdLookup);
        List<SelectListItem> GetddLookup(RmDdLookup _RmDdLookup);
        List<SelectListItem> GetWeekNo();
        List<SelectListItem> GetMonth();
    }
    public class DDLookupBO : IDDLookupBO
    {
        //private readonly IDDLookupProvider _DDLookupProvider;

        public DDLookupBO()
        {
            //_DDLookupProvider = _ddLookup;
        }
        public List<SelectListItem> GetddLookup(RmDdLookup _RmDdLookup)
        {
            //if (_RmDdLookup.DdlType == "Asset Group" || _RmDdLookup.DdlType == "ASSET LISTING" || _RmDdLookup.DdlType == "Asset Type" || _RmDdLookup.DdlType == "Structure Code" || _RmDdLookup.DdlType == "Superstructure" ||
            //    _RmDdLookup.DdlType == "Parapet Type" || _RmDdLookup.DdlType == "Bearing Type" || _RmDdLookup.DdlType == "Expansion Type" || _RmDdLookup.DdlType == "Deck Type" ||
            //    _RmDdLookup.DdlType == "Abutment Type" || _RmDdLookup.DdlType == "Pier Type" || _RmDdLookup.DdlType == "Bound" || _RmDdLookup.DdlType == "Abutment Walls, Foundation" ||
            //    _RmDdLookup.DdlType == "Piers, Connectiong of primary components" || _RmDdLookup.DdlType == "Bearing, Bearing Seats, Bearing Diaphgrams" || _RmDdLookup.DdlType == "Beams, Girders, Trussess, Arches" ||
            //    _RmDdLookup.DdlType == "Deck Slab, Pavement" || _RmDdLookup.DdlType == "Signboard, Utilities" || _RmDdLookup.DdlType == "Waterway" ||
            //    _RmDdLookup.DdlType == "Drain Water Down Pipe, Drainage" || _RmDdLookup.DdlType == "Parapet, Railing" || _RmDdLookup.DdlType == "Kerb, Sidewalks, Approaches, Approch Slab" || _RmDdLookup.DdlType == "Expansion Joint"
            //    || _RmDdLookup.DdlType == "Slope Protections, Retaining Wall")
            //{
            //    return _DDLookupProvider.LoadProdData(_RmDdLookup).ToList();
            //}
            //if (_RmDdLookup.DdlType == "Distress Code")
            //{
            //    return _DDLookupProvider.LoadProdData(_RmDdLookup).ToList();
            //}
            //if (_RmDdLookup.DdlType == "Site Ref")
            //{
            //    return _DDLookupProvider.LoadProdData(_RmDdLookup).ToList();
            //}
            //if (_RmDdLookup.DdlType == "Priority")
            //{
            //    return _DDLookupProvider.LoadProdData(_RmDdLookup).ToList();
            //}
            //if (_RmDdLookup.DdlType == "Unit")
            //{
            //    return _DDLookupProvider.LoadProdData(_RmDdLookup).ToList();
            //}
            return null;//_DDLookupProvider.LoadProdData(_RmDdLookup).ToList(); ;
        }
        public List<SelectListItem> GetWeekNo()
        {
            List<SelectListItem> itemlist = new List<SelectListItem>();
            if(DateTime.IsLeapYear(DateTime.Now.Year))//for leap year 53 week no.
            {
                for (int i = 1; i <= 53; i++)
                {
                    SelectListItem ss = new SelectListItem();
                    ss.Value = i.ToString();
                    ss.Text = i.ToString();
                    itemlist.Add(ss);
                }
            }
            else
            {
                for (int i = 1; i <= 52; i++)
                {
                    SelectListItem ss = new SelectListItem();
                    ss.Value = i.ToString();
                    ss.Text = i.ToString();
                    itemlist.Add(ss);
                }
            }
           
            return itemlist;
        }
        public List<SelectListItem> GetMonth()
        {
            int iCurMonth = DateTime.Today.Month;
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList = DateTimeFormatInfo
                .InvariantInfo
                .MonthNames
                .Select((monthName, index) => new SelectListItem
                {
                   Value = (index + 1).ToString(),
                   Text = monthName,
                   Selected= (index+1==iCurMonth ?true:false)
                }).ToList();
            
            return itemList;
        }

    }
}
