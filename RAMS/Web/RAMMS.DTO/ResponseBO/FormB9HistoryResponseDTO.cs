using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormB9HistoryResponseDTO
    {
        public int B9dshPkRefNo { get; set; }
        public int? B9dshB9dsPkRefNo { get; set; }
        public string B9dshFeature { get; set; }
        public string B9dshCode { get; set; }
        public string B9dshName { get; set; }
        public decimal? B9dshCond1 { get; set; }
        public decimal? B9dshCond2 { get; set; }
        public decimal? B9dshCond3 { get; set; }
        public int? B9dshUnitOfService { get; set; }
        public string B9dshRemarks { get; set; }
        public int? B9dshRevisionNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? B9dshRevisionDate { get; set; }
        public int? B9dshUserId { get; set; }
        public string B9dshUserName { get; set; }

    }
}
