using System;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Repository
{
    public class FormFSDetailRepository : RepositoryBase<RmFormFsInsDtl>, IFormFSDetailRepository
    {
        public FormFSDetailRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }
        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<FormFSDetailRequestDTO> filterOptions)
        {
            return await (from s in _context.RmFormFsInsDtl where s.FsdFshPkRefNo == filterOptions.Filters.FshPkRefNo && s.FsdActiveYn == true select s).LongCountAsync();
        }
        public async Task<List<FormFSDetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormFSDetailRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmFormFsInsDtl
                         where s.FsdFshPkRefNo == filterOptions.Filters.FshPkRefNo && s.FsdActiveYn == true
                         orderby s.FsdFeature
                         select s);
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new FormFSDetailRequestDTO
            {
                PkRefNo = s.FsdPkRefNo,
                FshPkRefNo = s.FsdFshPkRefNo,
                Feature = s.FsdFeature,
                GrpType = s.FsdGrpType,
                StrucCode = s.FsdStrucCode,
                Width = s.FsdWidth,
                Length = s.FsdLength,
                Condition1 = s.FsdCondition1,
                Condition2 = s.FsdCondition2,
                Condition3 = s.FsdCondition3,
                Needed = s.FsdNeeded,
                Unit = s.FsdUnit,
                Remarks = s.FsdRemarks,
                ModBy = s.FsdModBy,
                ModDt = s.FsdModDt,
                CrBy = s.FsdCrBy,
                CrDt = s.FsdCrDt,
                SubmitSts = s.FsdSubmitSts,
                ActiveYn = s.FsdActiveYn.Value,
                GroupCode = s.FsdGrpCode
            }).ToList();
        }

        public async Task<List<FormFSDetailRequestDTO>> GetRecordList(int headerId)
        {
            var query = (from s in _context.RmFormFsInsDtl
                         where s.FsdFshPkRefNo == headerId
                         && s.FsdActiveYn == true
                         orderby s.FsdFeature
                         select s);
            var lst = await query.ToListAsync();
            return lst.Select(s => new FormFSDetailRequestDTO
            {
                PkRefNo = s.FsdPkRefNo,
                FshPkRefNo = s.FsdFshPkRefNo,
                Feature = s.FsdFeature,
                GrpType = s.FsdGrpType,
                StrucCode = s.FsdStrucCode,
                Width = s.FsdWidth,
                Length = s.FsdLength,
                Condition1 = s.FsdCondition1,
                Condition2 = s.FsdCondition2,
                Condition3 = s.FsdCondition3,
                Needed = s.FsdNeeded,
                Unit = s.FsdUnit,
                Remarks = s.FsdRemarks,
                ModBy = s.FsdModBy,
                ModDt = s.FsdModDt,
                CrBy = s.FsdCrBy,
                CrDt = s.FsdCrDt,
                SubmitSts = s.FsdSubmitSts,
                ActiveYn = s.FsdActiveYn.Value,
                GroupCode = s.FsdGrpCode
            }).ToList();
        }

        private IQueryable<RmDdLookup> GetAsset()
        {
            return (from d in _context.RmDdLookup
                    where d.DdlActiveYn == true && d.DdlType == "Asset Type"
                    select d);
        }
        public int BulkInsert(List<RmFormFsInsDtl> details, int headerid)
        {
            foreach (RmFormFsInsDtl detail in details)
                detail.FsdFshPkRefNo = new int?(headerid);
            this._context.RmFormFsInsDtl.AddRange(details);
            return this._context.SaveChanges();
        }


        public List<RmFormFsInsDtl> GetDetailsforInsert(int headerid,int userid,RmFormFsInsHdr hdr)
        {
            List<RmDdLookup> list = this.GetAsset().Where(s => s.DdlTypeCode == "CW").ToList<RmDdLookup>();
            List<RmFormFsInsDtl> source = new List<RmFormFsInsDtl>();
            foreach (RmDdLookup rmDdLookup in list)
            {
                RmFormFsInsDtl carriageWayDetail = this.GetCarriageWayDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, userid, headerid, hdr);
                if (carriageWayDetail != null)
                    source.Add(carriageWayDetail);
            }
            IQueryable<RmDdLookup> asset1 = this.GetAsset().Where(s => s.DdlTypeCode == "CLM");
            foreach (RmDdLookup rmDdLookup in asset1)
            {
                RmFormFsInsDtl lineMarkingDetail = this.GetCenterLineMarkingDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, userid, headerid, hdr);
                if (lineMarkingDetail != null)
                    source.Add(lineMarkingDetail);
            }
            IQueryable<RmDdLookup> asset2 = this.GetAsset().Where(s => s.DdlTypeCode == "ELM");
            foreach (RmDdLookup rmDdLookup in asset2)
            {
                RmFormFsInsDtl lineMarkingDetail1 = this.GetEDGELineMarkingDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "L", userid, headerid, hdr);
                if (lineMarkingDetail1 != null)
                    source.Add(lineMarkingDetail1);
                RmFormFsInsDtl lineMarkingDetail2 = this.GetEDGELineMarkingDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "R", userid, headerid, hdr);
                if (lineMarkingDetail2 != null)
                    source.Add(lineMarkingDetail2);
            }
            IQueryable<RmDdLookup> asset3 = this.GetAsset().Where(s => s.DdlTypeCode == "DI");
            foreach (RmDdLookup rmDdLookup in asset3)
            {
                RmFormFsInsDtl ditchDetail1 = this.GetDitchDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "L", userid, headerid, hdr);
                if (ditchDetail1 != null)
                    source.Add(ditchDetail1);
                RmFormFsInsDtl ditchDetail2 = this.GetDitchDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "R", userid, headerid, hdr);
                if (ditchDetail2 != null)
                    source.Add(ditchDetail2);
            }
            IQueryable<RmDdLookup> asset4 = this.GetAsset().Where(s => s.DdlTypeCode == "DR");
            foreach (RmDdLookup rmDdLookup in asset4)
            {
                RmFormFsInsDtl drainDetail1 = this.GetDrainDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "L", userid, headerid, hdr);
                if (drainDetail1 != null)
                    source.Add(drainDetail1);
                RmFormFsInsDtl drainDetail2 = this.GetDrainDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "R", userid, headerid, hdr);
                if (drainDetail2 != null)
                    source.Add(drainDetail2);
            }
            IQueryable<RmDdLookup> asset5 = this.GetAsset().Where(s => s.DdlTypeCode == "SH");
            foreach (RmDdLookup rmDdLookup in asset5)
            {
                RmFormFsInsDtl shoulderDetail1 = this.GetShoulderDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "L", userid, headerid, hdr);
                if (shoulderDetail1 != null)
                    source.Add(shoulderDetail1);
                RmFormFsInsDtl shoulderDetail2 = this.GetShoulderDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, "R", userid, headerid, hdr);
                if (shoulderDetail2 != null)
                    source.Add(shoulderDetail2);
            }
            IQueryable<RmDdLookup> asset6 = this.GetAsset().Where(s => s.DdlTypeCode == "RS");
            foreach (RmDdLookup rmDdLookup in asset6)
            {
                RmFormFsInsDtl roadStudsDetail = this.GetRoadStudsDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeCode, "L", userid, headerid, hdr);
                if (roadStudsDetail != null)
                    source.Add(roadStudsDetail);
                this.GetRoadStudsDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeCode, "M", userid, headerid, hdr);
                if (roadStudsDetail != null)
                    source.Add(roadStudsDetail);
                this.GetRoadStudsDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeCode, "R", userid, headerid, hdr);
                if (roadStudsDetail != null)
                    source.Add(roadStudsDetail);
            }
            IQueryable<RmDdLookup> asset7 = this.GetAsset().Where(s => s.DdlTypeCode == "CV");
            foreach (RmDdLookup rmDdLookup in asset7)
            {
                RmFormFsInsDtl culvertDetail = this.GetCulvertDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, userid, headerid, hdr);
                if (culvertDetail != null)
                    source.Add(culvertDetail);
            }
            IQueryable<RmDdLookup> asset8 = this.GetAsset().Where(s => s.DdlTypeCode == "BR");
            foreach (RmDdLookup rmDdLookup in asset8)
            {
                RmFormFsInsDtl bridgeDetail = this.GetBridgeDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, userid, headerid, hdr);
                if (bridgeDetail != null)
                    source.Add(bridgeDetail);
            }
            IQueryable<RmDdLookup> asset9 = this.GetAsset().Where (s => s.DdlTypeCode == "GR");
            foreach (RmDdLookup rmDdLookup in asset9)
            {
                RmFormFsInsDtl guardialDetail = this.GetGuardialDetail(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, userid, headerid, hdr);
                if (guardialDetail != null)
                    source.Add(guardialDetail);
            }
            IQueryable<RmDdLookup> asset10 = this.GetAsset().Where(s => s.DdlTypeCode == "SG");
            foreach (RmDdLookup rmDdLookup in asset10)
            {
                RmFormFsInsDtl signs = this.GetSigns(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, userid, headerid, hdr);
                if (signs != null)
                    source.Add(signs);
            }
            IQueryable<RmDdLookup> asset11 = this.GetAsset().Where(s => s.DdlTypeCode == "RW");
            foreach (RmDdLookup rmDdLookup in asset11)
            {
                RmFormFsInsDtl retainingWall = this.GetRetainingWall(rmDdLookup.DdlTypeDesc, rmDdLookup.DdlTypeValue, userid, headerid, hdr);
                if (retainingWall != null)
                    source.Add(retainingWall);
            }
            return source.Where(s => s != null).ToList();
        }


        public RmFormFsInsDtl GetCarriageWayDetail(string grptype, string StructureCode, int userid, int headerid, RmFormFsInsHdr hdr)
        {

            var count = (from o in _context.RmFormFcInsDtl
                         join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                         where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "CW" && o.FcidActiveYn == true && h.FcihActiveYn == true
                         && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihRoadCode == hdr.FshRoadCode
                         select 1).Count();
            decimal condition1 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "CW" && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 1
                                  select 1).Count();
            decimal condition2 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype
                                  && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidAiAssetGrpCode == "CW" && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 2
                                  select 1).Count();
            decimal condition3 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype
                                  && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidAiAssetGrpCode == "CW" && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 3
                                  select 1).Count();
            double Length = (double)(condition1 + condition2 + condition3);
            var json = (from h in _context.RmFormFcInsHdr
                        where h.FcihRoadCode == hdr.FshRoadCode
                        && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihActiveYn == true
                        && h.FcihAssetTypes != null && h.FcihAssetTypes != ""
                        select h.FcihAssetTypes).FirstOrDefault();
            double avgWidth = 0;
            if (!string.IsNullOrEmpty(json))
            {
                var AvgWidth = Common.Utility.JDeSerialize<FormAssetTypesDTO>(json ?? "");

                if (AvgWidth.ContainsKey("CW"))
                {
                    var cw = AvgWidth["CW"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue(grptype))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                double.TryParse(c["AvgWidth"], out avgWidth);
                                break;
                            }
                        }
                    }
                }
            }

            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "CARRIAGE WAY",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "CW",
                    FsdLength = Length,
                    FsdWidth = avgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "km",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }


        public RmFormFsInsDtl GetCenterLineMarkingDetail(string grptype, string StructureCode, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormFcInsDtl
                         join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                         where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "CLM" && h.FcihActiveYn == true
                         && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidActiveYn == true
                         && h.FcihRoadCode == hdr.FshRoadCode
                         select 1).Count();
            decimal condition1 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "CLM"
                                  && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 1
                                  select 1).Count();
            decimal condition2 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "CLM"
                                  && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 2
                                  select 1).Count();
            decimal condition3 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "CLM"
                                  && h.FcihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 3
                                  select 1).Count();
            double Length = (double)(condition1 + condition2 + condition3);
            var json = (from h in _context.RmFormFcInsHdr
                        where h.FcihRoadCode == hdr.FshRoadCode
                        && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihActiveYn == true
                        && h.FcihAssetTypes != null && h.FcihAssetTypes != ""
                        select h.FcihAssetTypes).FirstOrDefault();
            double avgWidth = 0;
            if (!string.IsNullOrEmpty(json))
            {
                var AvgWidth = Common.Utility.JDeSerialize<FormAssetTypesDTO>(json ?? "");

                if (AvgWidth.ContainsKey("CLM"))
                {
                    var cw = AvgWidth["CLM"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue(grptype))
                        {
                            if (c.ContainsKey("AvgWidth"))
                            {
                                double.TryParse(c["AvgWidth"], out avgWidth);
                                break;
                            }
                        }
                    }
                }
            }

            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "CENTER LINE MARKING",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "CLM",
                    FsdLength = Length,
                    FsdWidth = avgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "km",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }

        public RmFormFsInsDtl GetEDGELineMarkingDetail(string grptype, string StructureCode, string LR, int userid, int headerid, RmFormFsInsHdr hdr)
        {

            var count = (from o in _context.RmFormFcInsDtl
                         join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                         where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "ELM" && o.FcidActiveYn == true
                         && h.FcihRoadCode == hdr.FshRoadCode && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihActiveYn == true
                         && o.FcidAiBound == LR
                         select 1).Count();
            decimal condition1 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "ELM" && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidCondition == 1
                                  && o.FcidAiBound == LR
                                  select 1).Count();
            decimal condition2 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == "ELM" && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidCondition == 2
                                  && o.FcidAiBound == LR
                                  select 1).Count();
            decimal condition3 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && h.FcihYearOfInsp == hdr.FshYearOfInsp && o.FcidAiAssetGrpCode == "ELM" && o.FcidActiveYn == true && h.FcihActiveYn == true
                                  && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 3
                                  && o.FcidAiBound == LR
                                  select 1).Count();
            double Length = (double)(condition1 + condition2 + condition3);
            var json = (from h in _context.RmFormFcInsHdr
                        where h.FcihRoadCode == hdr.FshRoadCode && h.FcihActiveYn == true
                        && h.FcihAssetTypes != null && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihAssetTypes != ""
                        select h.FcihAssetTypes).FirstOrDefault();
            double avgWidth = 0;
            if (!string.IsNullOrEmpty(json))
            {
                var AvgWidth = Common.Utility.JDeSerialize<FormAssetTypesDTO>(json ?? "");

                if (AvgWidth.ContainsKey("ELM"))
                {
                    var cw = AvgWidth["ELM"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue(grptype))
                        {
                            if (LR == "L" && c.ContainsKey("LAvgWidth"))
                            {
                                double.TryParse(c["LAvgWidth"], out avgWidth);
                                break;
                            }
                            else if (LR == "R" && c.ContainsKey("RAvgWidth"))
                            {
                                double.TryParse(c["RAvgWidth"], out avgWidth);
                                break;
                            }
                        }
                    }
                }
            }

            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "EDGELINE MARKING",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "ELM_" + LR,
                    FsdLength = Length,
                    FsdWidth = avgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "km",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
            {
                return null;
            }
        }

        public RmFormFsInsDtl GetDrainDetail(string grptype, string StructureCode, string LR, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormFdInsDtl
                         join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                         where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                         && h.FdihYearOfInsp == hdr.FshYearOfInsp
                         && o.FdidAiAssetGrpCode == "DR" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                         select 1).Count();
            var Length = (from o in _context.RmFormFdInsDtl
                          join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                          where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                          && h.FdihYearOfInsp == hdr.FshYearOfInsp
                          && o.FdidAiAssetGrpCode == "DR" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                          select o.FdidLength).Sum();
            var AvgWidth = (from o in _context.RmFormFdInsDtl
                            join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                            where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                          && h.FdihYearOfInsp == hdr.FshYearOfInsp
                          && o.FdidAiAssetGrpCode == "DR" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                            select o.FdidWidth).Average();
            decimal condition1 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "DR" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 1
                                  select 1).Count();
            decimal condition2 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "DR" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 2
                                  select 1).Count();
            decimal condition3 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "DR" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 3
                                  select 1).Count();

            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "DRAIN",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "DR_" + LR,
                    FsdLength = Length,
                    FsdWidth = AvgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "km",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
            {
                return null;
            }
        }

        public RmFormFsInsDtl GetDitchDetail(string grptype, string StructureCode, string LR, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormFdInsDtl
                         join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                         where h.FdihRoadCode == hdr.FshRoadCode
                         && h.FdihYearOfInsp == hdr.FshYearOfInsp && o.FdidAiGrpType == grptype && o.FdidAiAssetGrpCode == "DI"
                         && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                         select 1).Count();
            var Length = (from o in _context.RmFormFdInsDtl
                          join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                          where h.FdihRoadCode == hdr.FshRoadCode
                          && h.FdihYearOfInsp == hdr.FshYearOfInsp && o.FdidAiGrpType == grptype && o.FdidAiAssetGrpCode == "DI"
                          && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                          select o.FdidLength).Sum();
            var AvgWidth = (from o in _context.RmFormFdInsDtl
                            join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                            where h.FdihRoadCode == hdr.FshRoadCode
                          && h.FdihYearOfInsp == hdr.FshYearOfInsp && o.FdidAiGrpType == grptype && o.FdidAiAssetGrpCode == "DI"
                          && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                            select o.FdidWidth).Average();
            decimal condition1 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "DI" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 1
                                  select 1).Count();
            decimal condition2 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "DI" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 2
                                  select 1).Count();
            decimal condition3 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "DI" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 3
                                  select 1).Count();
            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "DITCH",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "DI_" + LR,
                    FsdLength = Length,
                    FsdWidth = AvgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "km",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }

        public RmFormFsInsDtl GetShoulderDetail(string grptype, string StructureCode, string LR, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormFdInsDtl
                         join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                         where h.FdihRoadCode == hdr.FshRoadCode
                         && h.FdihYearOfInsp == hdr.FshYearOfInsp && o.FdidAiGrpType == grptype && o.FdidAiAssetGrpCode == "SH"
                         && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                         select 1).Count();
            var Length = (from o in _context.RmFormFdInsDtl
                          join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                          where h.FdihRoadCode == hdr.FshRoadCode
                          && h.FdihYearOfInsp == hdr.FshYearOfInsp && o.FdidAiGrpType == grptype && o.FdidAiAssetGrpCode == "SH"
                          && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                          select o.FdidLength).Sum();
            var AvgWidth = (from o in _context.RmFormFdInsDtl
                            join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                            where h.FdihRoadCode == hdr.FshRoadCode
                          && h.FdihYearOfInsp == hdr.FshYearOfInsp && o.FdidAiGrpType == grptype && o.FdidAiAssetGrpCode == "SH"
                          && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                            select o.FdidWidth).Average();
            decimal condition1 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "SH" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 1
                                  select 1).Count();
            decimal condition2 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "SH" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 2
                                  select 1).Count();
            decimal condition3 = (from o in _context.RmFormFdInsDtl
                                  join h in _context.RmFormFdInsHdr on o.FdidFdihPkRefNo equals h.FdihPkRefNo
                                  where h.FdihRoadCode == hdr.FshRoadCode && o.FdidAiGrpType == grptype
                                  && h.FdihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FdidAiAssetGrpCode == "SH" && o.FdidAiBound == LR && o.FdidActiveYn == true && h.FdihActiveYn == true
                                  && o.FdidCondition == 3
                                  select 1).Count();
            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "SHOULDER",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "SH_" + LR,
                    FsdLength = Length,
                    FsdWidth = AvgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "km",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }

        public RmFormFsInsDtl GetRoadStudsDetail(string grptype, string StructureCode, string LR, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormFcInsDtl
                         join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                         where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == StructureCode && o.FcidActiveYn == true && h.FcihActiveYn == true
                          && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihRoadCode == hdr.FshRoadCode
                         select 1).Count();
            decimal condition1 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == StructureCode && o.FcidActiveYn == true && h.FcihActiveYn == true
                                   && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 1
                                  select o.FcidCondition).Count();
            decimal condition2 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == StructureCode && o.FcidActiveYn == true && h.FcihActiveYn == true
                                   && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 2
                                  select 1).Count();
            decimal condition3 = (from o in _context.RmFormFcInsDtl
                                  join h in _context.RmFormFcInsHdr on o.FcidFcihPkRefNo equals h.FcihPkRefNo
                                  where o.FcidAiGrpType == grptype && o.FcidAiAssetGrpCode == StructureCode && o.FcidActiveYn == true && h.FcihActiveYn == true
                                   && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihRoadCode == hdr.FshRoadCode && o.FcidCondition == 3
                                  select 1).Count();
            double Length = (double)(condition1 + condition2 + condition3);
            //string LR = grptype == "Left" ? "L" : grptype == "Right" ? "R" : "C";
            var json = (from h in _context.RmFormFcInsHdr
                        where h.FcihRoadCode == hdr.FshRoadCode
                         && h.FcihYearOfInsp == hdr.FshYearOfInsp && h.FcihActiveYn == true
                        && h.FcihAssetTypes != null && h.FcihAssetTypes != ""
                        select h.FcihAssetTypes).FirstOrDefault();
            double avgWidth = 0;
            if (!string.IsNullOrEmpty(json))
            {
                var AvgWidth = Common.Utility.JDeSerialize<FormAssetTypesDTO>(json ?? "");

                if (AvgWidth.ContainsKey("RS"))
                {
                    var cw = AvgWidth["RS"];
                    foreach (var c in cw)
                    {
                        if (c.ContainsValue(grptype))
                        {
                            if (LR == "L" && c.ContainsKey("LAvgWidth"))
                            {
                                double.TryParse(c["LAvgWidth"], out avgWidth);
                                break;
                            }
                            else if (LR == "R" && c.ContainsKey("RAvgWidth"))
                            {
                                double.TryParse(c["RAvgWidth"], out avgWidth);
                                break;
                            }
                        }
                    }
                }
            }

            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "ROAD STUDS",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "R_" + LR,
                    FsdLength = Length,
                    FsdWidth = avgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "km",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
            {
                return null;
            }
        }

        public RmFormFsInsDtl GetCulvertDetail(string grptype, string StructureCode, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormF4InsDtl
                         join h in _context.RmFormF4InsHdr on o.FivadFivahPkRefNo equals h.FivahPkRefNo
                         where
                         h.FivahRoadCode == hdr.FshRoadCode
                          && h.FivahYearOfInsp == hdr.FshYearOfInsp && o.FivadActiveYn == true && h.FivahActiveYn == true && o.FivadStrucCode == StructureCode
                         select 1).Count();
            var Length = (from o in _context.RmFormF4InsDtl
                          join h in _context.RmFormF4InsHdr on o.FivadFivahPkRefNo equals h.FivahPkRefNo
                          where
                          h.FivahRoadCode == hdr.FshRoadCode
                           && h.FivahYearOfInsp == hdr.FshYearOfInsp && o.FivadActiveYn == true && o.FivadStrucCode == StructureCode && h.FivahActiveYn == true
                          select o.FivadLength)?.Sum();
            var AvgWidth = (from o in _context.RmFormF4InsDtl
                            join h in _context.RmFormF4InsHdr on o.FivadFivahPkRefNo equals h.FivahPkRefNo
                            where h.FivahRoadCode == hdr.FshRoadCode && h.FivahYearOfInsp == hdr.FshYearOfInsp && o.FivadActiveYn == true && h.FivahActiveYn == true && o.FivadStrucCode == StructureCode
                            select o.FivadWidth).Average();
            decimal condition1 =
                (from o in _context.RmFormF4InsDtl
                 join h in _context.RmFormF4InsHdr on o.FivadFivahPkRefNo equals h.FivahPkRefNo
                 where h.FivahRoadCode == hdr.FshRoadCode && h.FivahYearOfInsp == hdr.FshYearOfInsp && o.FivadActiveYn == true && h.FivahActiveYn == true && o.FivadStrucCode == StructureCode
                 && o.FivadCondition == 1
                 select 1).Count();
            decimal condition2 =
                (from o in _context.RmFormF4InsDtl
                 join h in _context.RmFormF4InsHdr on o.FivadFivahPkRefNo equals h.FivahPkRefNo
                 where h.FivahRoadCode == hdr.FshRoadCode && h.FivahYearOfInsp == hdr.FshYearOfInsp && o.FivadActiveYn == true && h.FivahActiveYn == true && o.FivadStrucCode == StructureCode
                 && o.FivadCondition == 2
                 select 1).Count();
            decimal condition3 =
                (from o in _context.RmFormF4InsDtl
                 join h in _context.RmFormF4InsHdr on o.FivadFivahPkRefNo equals h.FivahPkRefNo
                 where h.FivahRoadCode == hdr.FshRoadCode && h.FivahYearOfInsp == hdr.FshYearOfInsp && o.FivadActiveYn == true && h.FivahActiveYn == true && o.FivadStrucCode == StructureCode
                 && o.FivadCondition == 3
                 select 1).Count();
            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "CULVERTS",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = StructureCode,
                    FsdLength = Length,
                    FsdWidth = null,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "nr",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }

        public RmFormFsInsDtl GetBridgeDetail(string grptype, string StructureCode, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormF5InsDtl
                         join h in _context.RmFormF5InsHdr on o.FvadFvahPkRefNo equals h.FvahPkRefNo
                         where h.FvahRoadCode == hdr.FshRoadCode
                         && h.FvahYearOfInsp == hdr.FshYearOfInsp && o.FvadActiveYn == true && h.FvahActiveYn == true && o.FvadStrucCode == StructureCode
                         select 1).Count();
            var Length = (from o in _context.RmFormF5InsDtl
                          join h in _context.RmFormF5InsHdr on o.FvadFvahPkRefNo equals h.FvahPkRefNo
                          where h.FvahRoadCode == hdr.FshRoadCode
                          && h.FvahYearOfInsp == hdr.FshYearOfInsp && o.FvadActiveYn == true && h.FvahActiveYn == true && o.FvadStrucCode == StructureCode
                          select o.FvadLength)?.Sum();
            var AvgWidth = (from o in _context.RmFormF5InsDtl
                            join h in _context.RmFormF5InsHdr on o.FvadFvahPkRefNo equals h.FvahPkRefNo
                            where h.FvahRoadCode == hdr.FshRoadCode
                            && h.FvahYearOfInsp == hdr.FshYearOfInsp && o.FvadActiveYn == true && h.FvahActiveYn == true && o.FvadStrucCode == StructureCode
                            select o.FvadWidth).Average();
            decimal condition1 =
                (from o in _context.RmFormF5InsDtl
                 join h in _context.RmFormF5InsHdr on o.FvadFvahPkRefNo equals h.FvahPkRefNo
                 where h.FvahRoadCode == hdr.FshRoadCode
                 && h.FvahYearOfInsp == hdr.FshYearOfInsp && o.FvadActiveYn == true && h.FvahActiveYn == true && o.FvadStrucCode == StructureCode
                 && o.FvadCondition == 1
                 select 1).Count();
            decimal condition2 =
               (from o in _context.RmFormF5InsDtl
                join h in _context.RmFormF5InsHdr on o.FvadFvahPkRefNo equals h.FvahPkRefNo
                where h.FvahRoadCode == hdr.FshRoadCode
                && h.FvahYearOfInsp == hdr.FshYearOfInsp && o.FvadActiveYn == true && h.FvahActiveYn == true && o.FvadStrucCode == StructureCode
                && o.FvadCondition == 2
                select 1).Count();
            decimal condition3 =
               (from o in _context.RmFormF5InsDtl
                join h in _context.RmFormF5InsHdr on o.FvadFvahPkRefNo equals h.FvahPkRefNo
                where h.FvahRoadCode == hdr.FshRoadCode
                && h.FvahYearOfInsp == hdr.FshYearOfInsp && o.FvadActiveYn == true && h.FvahActiveYn == true && o.FvadStrucCode == StructureCode
                && o.FvadCondition == 3
                select 1).Count();

            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = condition1,
                    FsdCondition2 = condition2,
                    FsdCondition3 = condition3,
                    FsdFeature = "BRIDGES",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = StructureCode,
                    FsdLength = Length,
                    FsdWidth = AvgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "nr",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }

        public RmFormFsInsDtl GetGuardialDetail(string grptype, string StructureCode, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormF2GrInsDtl
                         join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                         where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                         && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                         select 1).Count();
            var Length = (from o in _context.RmFormF2GrInsDtl
                          join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                          where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                          && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                          select o.FgridLength)?.Sum();
            var AvgWidth = 0;
            double? condition1 = (from o in _context.RmFormF2GrInsDtl
                                  join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                                  where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                                  select o.FgridGrCondition1)?.Sum();
            double? condition2 = (from o in _context.RmFormF2GrInsDtl
                                  join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                                  where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                                  select o.FgridGrCondition2)?.Sum();
            double? condition3 = (from o in _context.RmFormF2GrInsDtl
                                  join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                                  where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                                  select o.FgridGrCondition3)?.Sum();
            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = (decimal?)condition1,
                    FsdCondition2 = (decimal?)condition2,
                    FsdCondition3 = (decimal?)condition3,
                    FsdFeature = "GUARDRAIL",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = StructureCode,
                    FsdLength = Length,
                    FsdWidth = AvgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "m",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }

        //RW/S
        public RmFormFsInsDtl GetRetainingWall(string grptype, string StructureCode, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormR1Hdr
                         where o.Fr1hAiRdCode == hdr.FshRoadCode && o.Fr1hYearOfInsp == hdr.FshYearOfInsp
                         && o.Fr1hActiveYn == true && o.Fr1hAiStrucCode == StructureCode
                         select 1).Count();
            var Length = (from o in _context.RmFormR1Hdr
                          join h in _context.RmAllassetInventory on o.Fr1hAidPkRefNo equals h.AiPkRefNo
                          where o.Fr1hAiRdCode == hdr.FshRoadCode && o.Fr1hYearOfInsp == hdr.FshYearOfInsp
                          && o.Fr1hActiveYn == true && o.Fr1hAiStrucCode == StructureCode
                          select h.AiLength)?.Sum();
            var AvgWidth = 0;
            double? condition1 = (from o in _context.RmFormR1Hdr
                                  where o.Fr1hAiRdCode == hdr.FshRoadCode && o.Fr1hYearOfInsp == hdr.FshYearOfInsp
                                  && o.Fr1hActiveYn == true && o.Fr1hAiStrucCode == StructureCode
                                  select o.Fr1hCondRating)?.Sum();

            double? condition2 = 0; /* (from o in _context.RmFormF2GrInsDtl
                                  join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                                  where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                                  select o.FgridGrCondition2)?.Sum();*/
            double? condition3 = 0; /* (from o in _context.RmFormF2GrInsDtl
                                  join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                                  where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                                  select o.FgridGrCondition3)?.Sum();*/
            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = (decimal?)condition1,
                    FsdCondition2 = (decimal?)condition2,
                    FsdCondition3 = (decimal?)condition3,
                    FsdFeature = "RETAINING WALL",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "rw",
                    FsdLength = Length,
                    FsdWidth = AvgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "m",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }

        public RmFormFsInsDtl GetSigns(string grptype, string StructureCode, int userid, int headerid, RmFormFsInsHdr hdr)
        {
            var count = (from o in _context.RmFormG1Hdr
                         where o.Fg1hRdCode == hdr.FshRoadCode && o.Fg1hYearOfInsp == hdr.FshYearOfInsp
                         && o.Fg1hActiveYn == true && o.Fg1hStrucCode == StructureCode
                         select 1).Count();

            var Length = 0; // (from o in _context.RmFormG1Hdr
            //              where o.Fg1hRdCode == hdr.FshRoadCode && o.Fg1hYearOfInsp == hdr.FshYearOfInsp
            //              && o.Fg1hActiveYn == true && o.Fg1hStrucCode == StructureCode 
            //              select o.Fg1hLocChKm)?.Sum();
            var AvgWidth = 0;
            double? condition1 = (from o in _context.RmFormG1Hdr
                                  where o.Fg1hRdCode == hdr.FshRoadCode && o.Fg1hYearOfInsp == hdr.FshYearOfInsp
                                  && o.Fg1hActiveYn == true && o.Fg1hStrucCode == StructureCode
                                  select o.Fg1hCondRating)?.Sum();
            double? condition2 = 0; /* (from o in _context.RmFormF2GrInsDtl
                                  join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                                  where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                                  select o.FgridGrCondition2)?.Sum();*/
            double? condition3 = 0; /* (from o in _context.RmFormF2GrInsDtl
                                  join h in _context.RmFormF2GrInsHdr on o.FgridFgrihPkRefNo equals h.FgrihPkRefNo
                                  where h.FgrihRoadCode == hdr.FshRoadCode && h.FgrihYearOfInsp == hdr.FshYearOfInsp
                                  && o.FgridActiveYn == true && o.FgridGrCode == StructureCode && h.FgrihActiveYn == true
                                  select o.FgridGrCondition3)?.Sum();*/
            if (count > 0)
            {
                var detail = new RmFormFsInsDtl
                {
                    FsdActiveYn = true,
                    FsdCondition1 = (decimal?)condition1,
                    FsdCondition2 = (decimal?)condition2,
                    FsdCondition3 = (decimal?)condition3,
                    FsdFeature = "SIGNS",
                    FsdFshPkRefNo = headerid,
                    FsdGrpType = grptype,
                    FsdGrpCode = "sg",
                    FsdLength = Length,
                    FsdWidth = AvgWidth,
                    FsdStrucCode = StructureCode,
                    FsdSubmitSts = false,
                    FsdUnit = "nr",
                    FsdCrBy = userid,
                    FsdCrDt = DateTime.UtcNow,
                    FsdModBy = userid,
                    FsdModDt = DateTime.UtcNow
                };
                return detail;
            }
            else
                return null;
        }
    }
}
