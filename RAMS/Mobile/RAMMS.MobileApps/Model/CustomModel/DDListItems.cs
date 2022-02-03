using System;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{
    public class DDListItems
    {
        public bool Disabled { get; set; }
        public string Group { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }

    }
    public class FormJDDListItems:ResponseBase
    {
        public bool Disabled { get; set; }
        public string Group { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class FormJAddFilters
    {
        public string HeaderNo { get; set; }
    }

    public class ADDFilters
    {
        public int StartPageNo { get; set; }
        public int RecordsPerPage { get; set; }
        public int sortOrder { get; set; }
        public FormJAddFilters Filters { get; set; }
    }

    public class LandingHomeResponseDTO
    {
        public int? LandingNodCount { get; set; }
        public int? LandingNcnCount { get; set; }
        public int? LandingNcrCount { get; set; }

    }
}