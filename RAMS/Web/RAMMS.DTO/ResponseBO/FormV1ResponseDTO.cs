﻿using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV1ResponseDTO
    {

        public int PkRefNo { get; set; }
        public int? ContNo { get; set; }
        public string Rmu { get; set; }
        public string RmuName { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string DivCode { get; set; }
        public string DivName { get; set; }
        public string RefId { get; set; }
        public string Crew { get; set; }
        public string Crewname { get; set; }
        public string ActCode { get; set; }
        public string ActName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Dt { get; set; }
        public bool SignSch { get; set; }
        public int UseridSch { get; set; }
        public string UsernameSch { get; set; }
        public string DesignationSch { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtSch { get; set; }
        public bool SignAgr { get; set; }
        public int UseridAgr { get; set; }
        public string UsernameAgr { get; set; }
        public string DesignationAgr { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtAgr { get; set; }
        public bool SignAck { get; set; }
        public int UseridAck { get; set; }
        public string UsernameAck { get; set; }
        public string DesignationAck { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtAck { get; set; }
        public string ServiceProvider { get; set; }
        public string Verifier { get; set; }
        public string Facilitator { get; set; }
        public string Remarks { get; set; }
        public string Source { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public string CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
        public string S1RefNoDetails { get; set; }

        //   public virtual ICollection<RmFormV1Dtl> RmFormV1Dtl { get; set; }

    }
}
