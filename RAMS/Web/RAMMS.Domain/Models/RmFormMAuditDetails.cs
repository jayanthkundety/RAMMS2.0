using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormMAuditDetails
    {
        public int FmadPkRefNo { get; set; }
        public int? FmadFmhPkRefNo { get; set; }
        public int? FmadCategory { get; set; }
        public string FmadActivityCode { get; set; }
        public string FmadActivityName { get; set; }
        public string FmadActivityFor { get; set; }
        public bool? FmadIsEditable { get; set; }
        public int? FmadTallyBox { get; set; }
        public int? FmadWeightage { get; set; }
        public int? FmadTotal { get; set; }
        public int? FmadA1tallyBox { get; set; }
        public int? FmadA1total { get; set; }
        public int? FmadA2tallyBox { get; set; }
        public int? FmadA2total { get; set; }
        public int? FmadA3tallyBox { get; set; }
        public int? FmadA3total { get; set; }
        public int? FmadA4tallyBox { get; set; }
        public int? FmadA4total { get; set; }
        public int? FmadA5tallyBox { get; set; }
        public int? FmadA5total { get; set; }
        public int? FmadA6tallyBox { get; set; }
        public int? FmadA6total { get; set; }
        public int? FmadA7tallyBox { get; set; }
        public int? FmadA7total { get; set; }
        public int? FmadA8tallyBox { get; set; }
        public int? FmadA8total { get; set; }
        public int? FmadB1tallyBox { get; set; }
        public int? FmadB1total { get; set; }
        public int? FmadB2tallyBox { get; set; }
        public int? FmadB2total { get; set; }
        public int? FmadB3tallyBox { get; set; }
        public int? FmadB3total { get; set; }
        public int? FmadB4tallyBox { get; set; }
        public int? FmadB4total { get; set; }
        public int? FmadB5tallyBox { get; set; }
        public int? FmadB5total { get; set; }
        public int? FmadB6tallyBox { get; set; }
        public int? FmadB6total { get; set; }
        public int? FmadB7tallyBox { get; set; }
        public int? FmadB7total { get; set; }
        public int? FmadB8tallyBox { get; set; }
        public int? FmadB8total { get; set; }
        public int? FmadB9tallyBox { get; set; }
        public int? FmadB9total { get; set; }
        public int? FmadC1tallyBox { get; set; }
        public int? FmadC1total { get; set; }
        public int? FmadC2tallyBox { get; set; }
        public int? FmadC2total { get; set; }
        public int? FmadD1tallyBox { get; set; }
        public int? FmadD1total { get; set; }
        public int? FmadD2tallyBox { get; set; }
        public int? FmadD2total { get; set; }
        public int? FmadD3tallyBox { get; set; }
        public int? FmadD3total { get; set; }
        public int? FmadD4tallyBox { get; set; }
        public int? FmadD4total { get; set; }
        public int? FmadD5tallyBox { get; set; }
        public int? FmadD5total { get; set; }
        public int? FmadD6tallyBox { get; set; }
        public int? FmadD6total { get; set; }
        public int? FmadD7tallyBox { get; set; }
        public int? FmadD7total { get; set; }
        public int? FmadD8tallyBox { get; set; }
        public int? FmadD8total { get; set; }
        public int? FmadE1tallyBox { get; set; }
        public int? FmadE1total { get; set; }
        public int? FmadE2tallyBox { get; set; }
        public int? FmadE2total { get; set; }
        public int? FmadF1tallyBox { get; set; }
        public int? FmadF1total { get; set; }
        public int? FmadF2tallyBox { get; set; }
        public int? FmadF2total { get; set; }
        public int? FmadF3tallyBox { get; set; }
        public int? FmadF3total { get; set; }
        public int? FmadF4tallyBox { get; set; }
        public int? FmadF4total { get; set; }
        public int? FmadF5tallyBox { get; set; }
        public int? FmadF5total { get; set; }
        public int? FmadF6tallyBox { get; set; }
        public int? FmadF6total { get; set; }
        public int? FmadF7tallyBox { get; set; }
        public int? FmadF7total { get; set; }
        public int? FmadG1tallyBox { get; set; }
        public int? FmadG1total { get; set; }
        public int? FmadG2tallyBox { get; set; }
        public int? FmadG2total { get; set; }
        public int? FmadG3tallyBox { get; set; }
        public int? FmadG3total { get; set; }
        public int? FmadG4tallyBox { get; set; }
        public int? FmadG4total { get; set; }
        public int? FmadG5tallyBox { get; set; }
        public int? FmadG5total { get; set; }
        public int? FmadG6tallyBox { get; set; }
        public int? FmadG6total { get; set; }
        public int? FmadG7tallyBox { get; set; }
        public int? FmadG7total { get; set; }
        public int? FmadG8tallyBox { get; set; }
        public int? FmadG8total { get; set; }
        public int? FmadG9tallyBox { get; set; }
        public int? FmadG9total { get; set; }
        public int? FmadG10tallyBox { get; set; }
        public int? FmadG10total { get; set; }
        public int? FmadModBy { get; set; }
        public DateTime? FmadModDt { get; set; }
        public int? FmadCrBy { get; set; }
        public DateTime? FmadCrDt { get; set; }

        public virtual RmFormMHdr FmadFmhPkRefNoNavigation { get; set; }
    }
}
