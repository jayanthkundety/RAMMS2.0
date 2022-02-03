using System;
using System.Collections.Generic;

namespace RAMMS.DTO.ResponseBO
{
    public class FormS2DetailResponseDTO
    {
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public decimal? PavedLength { get; set; }
        public decimal? UnPavedLength { get; set; }
        public string RoadLocationSequence { get; set; }
        public string Priority { get; set; }
        public int? Adp { get; set; }
        public int? CrewDayRequired { get; set; }
        public decimal? Target { get; set; }
        public int[] Weeks { get; set; }
        public string Remarks { get; set; }
        public int? HeaderId { get; set; }
        public string WorkQty { get; set; }

    }

    public class GridWeekInfo
    {
        public int WeekNo { get; set; }
        public bool IsChecked { get; set; }
    }
}
