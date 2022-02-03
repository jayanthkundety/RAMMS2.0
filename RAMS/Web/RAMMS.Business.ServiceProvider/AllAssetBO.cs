using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Common;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System.Threading.Tasks;
using RAMS.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RAMMS.Business.ServiceProvider
{
    public interface IAllAssetBO
    {
        int  ImportExcelAssetBO(List<RmAllassetInventory> excelAllAssetExcel);
        RmAllassetInventory BridgeEditByIdBO(int id);

        List<string> mltySelectedItems(string selectedItems);
        string MltySelectJoin(string selectedItems);
    }
    public class AllAssetBO : IAllAssetBO
    {

        public int ImportExcelAssetBO(List<RmAllassetInventory> excelAllAssetExcel)
        {
            int _AllAssetExcelSave = 0;
            foreach (var element in excelAllAssetExcel)
                for (int i = 0; i < excelAllAssetExcel.Count; i++)
                {
                    try
                    {
                        _AllAssetExcelSave = 0;
                        _AllAssetExcelSave = 1;
                    }
                    catch (Exception ex)
                    {
                        _AllAssetExcelSave = 500;
                    }

                }
            return _AllAssetExcelSave;


        }
        public RmAllassetInventory BridgeEditByIdBO(int id)
        {
            return null;
        }


        public List<string> mltySelectedItems(string selectedItems)
        {
            List<string> SelectedItems = new List<string>();
            if (!string.IsNullOrEmpty(selectedItems))
            {
                var strColAiStructSuper = selectedItems.Split(',');
                foreach (var item in strColAiStructSuper)
                {
                    SelectedItems.Add(item);

                }
            }
            return SelectedItems;
        }

        public string MltySelectJoin(string selectedItems)
        {
            string concat=null;
            if (selectedItems!=null)
            {
                concat= string.Join(",", selectedItems);
            }

            return concat;
        }
    }
}
