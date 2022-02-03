using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class DDLookUpService : IDDLookUpService
    {
        //private readonly IRepositoryUnit _repoUnit;
        //private readonly IMapper _mapper;
        private IDDLookUpRepository _lookupRepo;
        public DDLookUpService(IDDLookUpRepository repo)
        {
            //_repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _lookupRepo = repo;
        }
        public async Task<IEnumerable<CSelectListItem>> GetDdLookup(params string[] type)
        {
            string[] valueCode = new string[] { "RMU" };
            string[] value = new string[] { "Month", "RD_Name", "Week No", "Year" };
            string[] valuePK = new string[] { "Act-FormS2" };
            string[] textCodeDesc = new string[] { "Section Code", "Act-FormD", "RMU" };
            string[] textCodeValue = new string[] { "Act-FormS2" };
            string[] textDescValue = new string[] { "Bound", "Asset Type" };
            string[] groupDesc = new string[] { "Act-FormS2" };

            //Loop to get the DDL_Type_VALUE  in Value(Dropdown) Format(DDLType^Value/DDLType~Code^Value)
            foreach (string typ in type)
            {
                if (typ.Contains("~") && typ.Contains("^"))
                {
                    string[] val = typ.Split('~');
                    valueCode = valueCode.Where(x => x != val[0]).ToArray();
                    value = value.Concat(new[] { val[0] }).ToArray();
                    type = type.Select(x => { if (x.Contains("~")) { x = val[0]; } return x; }).ToArray();
                }
                else if (typ.Contains("^"))
                {
                    valueCode = valueCode.Where(x => x != typ.Split("^")[0]).ToArray();
                    value = value.Concat(new[] { typ.Split("^")[0] }).ToArray();
                    type = type.Select(x => { if (x.Contains("^")) { x = x.Split("^")[0]; } return x; }).ToArray();
                }
                else if (typ.Contains("~"))
                {
                    type = type.Select(x => { if (x.Contains("~")) { x = x.Split("~")[0]; } return x; }).ToArray();
                }
            }  
            
            string[] lstType = type.Where(x => x.Contains("~")).ToArray();
            if (lstType != null && lstType.Length > 0)
            {
                type = type.Where(x => !x.Contains("~")).ToArray();
            }
            else
            {
                lstType = new string[] { };
            }
            return await _lookupRepo.FindAsync(con => (type.Contains(con.DdlType) || (lstType != null && lstType.Contains(con.DdlType + "~" + con.DdlTypeCode))) && con.DdlActiveYn == true,
                sel => new CSelectListItem()
                {
                    Key = sel.DdlType,
                    Value = valuePK.Contains(sel.DdlType) ? sel.DdlPkRefNo.ToString() : (valueCode.Contains(sel.DdlType) ? sel.DdlTypeCode : (value.Contains(sel.DdlType) ? sel.DdlTypeValue : sel.DdlTypeDesc)),
                    Text = textCodeDesc.Contains(sel.DdlType) ? sel.DdlTypeCode + "-" + sel.DdlTypeDesc : (textCodeValue.Contains(sel.DdlType) ? sel.DdlTypeCode + "-" + sel.DdlTypeValue : (textDescValue.Contains(sel.DdlType) ? sel.DdlTypeValue + "-" + sel.DdlTypeDesc : sel.DdlTypeDesc)),
                    Code = sel.DdlTypeCode,
                    CValue = sel.DdlTypeValue,
                    PKId = sel.DdlPkRefNo,
                    Group = groupDesc.Contains(sel.DdlType) ? sel.DdlTypeDesc : ""
                });
        }
        public object GetDdlLookupByCode(string TypeCode)
        {
            return _lookupRepo.FindAsync(x => (x.DdlTypeCode == TypeCode), s => new
            {
                Id = s.DdlPkRefNo,
                Name = s.DdlType,
                Code = s.DdlTypeCode,
                Desc = s.DdlTypeDesc,
                Remarks = s.DdlTypeRemarks,
                Value = s.DdlTypeValue,
                OValue = s.DdlTypeValue == "-1" ? "0" : s.DdlTypeValue
            }).Result.ToList().OrderBy(x => x.OValue);
        }
        public async Task<IEnumerable<SelectListItem>> GetDdLookupValue(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeValue.ToString(),
                        Text = list.DdlTypeValue.ToString(),

                    });
                }
                return ddLookUpItem.ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        public async Task<IEnumerable<SelectListItem>> GetDdLookup(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeDesc.ToString(),
                        Text = list.DdlTypeDesc.ToString(),
                        //Selected = list.DdlTypeDesc == "Miri" && DdLookUp.Type == "RMU" ? true : false
                    });
                }
                return ddLookUpItem.ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        public async Task<IEnumerable<SelectListItem>> GetLookUpCodeTextConcat(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeCode.ToString(),
                        Text = list.DdlTypeCode.ToString() + "-" + list.DdlTypeValue.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetLookUpTextDescConcat(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeValue.ToString(),
                        Text = list.DdlTypeValue.ToString() + "-" + list.DdlTypeDesc.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public async Task<IEnumerable<SelectListItem>> GetLookUpTextConcat(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeValue.ToString(),
                        Text = list.DdlTypeCode.ToString() + "-" + list.DdlTypeValue.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        // Temp service for Demo purpose (GetDdTempLookUp)........//

        public async Task<IEnumerable<SelectListItem>> GetDdTempLookup(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeDesc.ToString(),
                        Text = list.DdlTypeDesc.ToString(),
                        //Selected = list.DdlTypeDesc == "Miri" && DdLookUp.Type == "RMU" ? true : false
                    });
                }
                return ddLookUpItem.ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public async Task<IEnumerable<SelectListItem>> GetDdDescValue(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeValue.ToString(),
                        Text = list.DdlTypeDesc.ToString()
                    });
                }
                return ddLookUpItem.ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public async Task<IEnumerable<SelectListItem>> GetLookUpCodeDesc(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeCode.ToString(),
                        Text = list.DdlTypeValue.ToString()
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetLookUpCodeText(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeValue.ToString(),
                        Text = list.DdlTypeCode.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetLookUpValueDesc(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeValue.ToString(),
                        Text = list.DdlTypeDesc.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetLookUpCodeDescription(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlTypeCode.ToString(),
                        Text = list.DdlTypeCode.ToString() + " - " + list.DdlTypeDesc.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<string> GetDDLValueforTypeAndDesc(DDLookUpDTO DdLookUp)
        {
            return await _lookupRepo.GetDesc(DdLookUp);
        }

        public async Task<IEnumerable<SelectListItem>> GetAllDefectCode()
        {
            var defcode = await _lookupRepo.GetDefCode();
            return defcode;
        }
        public async Task<IEnumerable<SelectListItem>> GetLookUpIdDesc(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlPkRefNo.ToString(),
                        Text = list.DdlTypeCode.ToString() + " - " + list.DdlTypeDesc.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetLookUpIdValueDesc(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                var ddList = await _lookupRepo.GetDdLookUp(DdLookUp);
                foreach (var list in ddList)
                {
                    ddLookUpItem.Add(new SelectListItem
                    {
                        Value = list.DdlPkRefNo.ToString(),
                        Text = list.DdlTypeValue.ToString() + " - " + list.DdlTypeDesc.ToString(),
                    });
                }
                return ddLookUpItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<RmDdLookup>> GetLookups(DDLookUpDTO DdLookUp)
        {
            var ddLookUpItem = new List<SelectListItem>();
            try
            {
                return await _lookupRepo.GetDdLookUp(DdLookUp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
