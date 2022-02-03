using RAMMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    public class CDataTable
    {
        public string Name { get; set; }
        public string APIURL { get; set; }
        public List<CDataColumns> Columns { get; set; } = new List<CDataColumns>();
        //public string SearchSection { get; set; }
        public bool IsView { get; set; } = true;
        public bool IsModify { get; set; } = true;
        public bool IsDelete { get; set; } = true;
        public bool IsPrint { get; set; } = true;
        public int LeftFixedColumn { get; set; } = 0;
        public int RightFixedColumn { get; set; } = 0;
        public List<CDataColumnDefs> columnDefs { get; set; }
    }

    public class CDataColumns
    {
        public string data { get; set; }
        public string name { get { return _name; } set { _name = value; } }
        private string _name = "";
        private string _title = "";
        public string title { get { return _title; } set { _title = value; _name = value.Replace(" ", "_"); } }
        public bool autoWidth { get; set; } = true;
        public bool sortable { get; set; } = true;
        public bool visible { get; set; } = true;
        public string render { get; set; }        
        public string className { get; set; }
        public string ColumnGroup { get; set; }
        public bool IsFreeze { set { className += value ? " headcol" : ""; } }        
    }
    public class CDataColumnDefs
    {
        public CDataColumnDefs()
        {

        }
        public CDataColumnDefs(string targetCols, string orderDataCols)
        {
            SetDef(targetCols, orderDataCols);
        }
        public int[] targets { get; set; }
        public int[] orderData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetCols">targetCols --> Column index with comma seperate</param>
        /// <param name="datacols">Column index with comma seperate</param>
        public void SetDef(string targetCols, string orderDataCols)
        {
            if (!string.IsNullOrEmpty(targetCols))
                targets = targetCols.Split(',').Select(x => Utility.ToInt(x)).ToArray();
            if(!string.IsNullOrEmpty(orderDataCols))
                orderData = orderDataCols.Split(',').Select(x => Utility.ToInt(x)).ToArray();
        }
    }
}
