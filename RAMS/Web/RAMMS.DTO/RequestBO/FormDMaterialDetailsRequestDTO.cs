using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.RequestBO
{
    public class FormDMaterialDetailsRequestDTO
    {
        [MapTo("FdmdPkRefNo")]
        public int No { get; set; }

        [MapTo("FdmdFdhPkRefNo")]
        public int FdmdFdhPkRefNo { get; set; }

        [MapTo("FdmdSrno")]
        public int? SerialNo { get; set; }

        [MapTo("FdmdMtCode")]
        public string MaterialCode { get; set; }

        [MapTo("FdmdMtDesc")]
        public string MaterialDesc { get; set; }


        [MapTo("FdmdMtQty")]
        public decimal? Quantity { get; set; }

        [MapTo("FdmdMtUnit")]
        public string Unit { get; set; }

        [MapTo("FdmdModBy")]
        public string ModifiedBy { get; set; }

        [MapTo("FdmdModDt")]
        public DateTime? ModifiedDate { get; set; }

        [MapTo("FdmdCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FdmdCrDt")]
        public DateTime? CreatedDate { get; set; }

        [MapTo("FdmdSubmitSts")]
        public bool SubmitStatus { get; set; }

        [MapTo("FdmdActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FdmdCodeDesc")]
        public string CodeDesc { get; set; }

        public FormDHeaderRequestDTO FormDmdFDHRefNoNavigation { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
