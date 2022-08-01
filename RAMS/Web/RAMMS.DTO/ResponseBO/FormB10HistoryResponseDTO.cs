using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormB10HistoryResponseDTO
    {
        public int B10dphPkRefNo { get; set; }
        public int? B10dphB10dpPkRefNo { get; set; }
        public string B10dphFeature { get; set; }
        public string B10dphCode { get; set; }
        public string B10dphName { get; set; }
        public decimal? B10dphCond1 { get; set; }
        public decimal? B10dphCond2 { get; set; }
        public decimal? B10dphCond3 { get; set; }
        public int? B10dphUnitOfService { get; set; }
        public string B10dphRemarks { get; set; }
        public int? B10dphRevisionNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? B10dphRevisionDate { get; set; }
        public int? B10dphUserId { get; set; }
        public string B10dphUserName { get; set; }



        

    }
}
