using System;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class SelectedRoadCodeResponseData
    {
        public int No { get; set; }
        public string FeatureId { get; set; }
        public string DivisionCode { get; set; }
        public string RmuCode { get; set; }
        public string SecName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string FrmLoc { get; set; }
        public string ToLoc { get; set; }
        public int FrmCh { get; set; }
        public int FrmChDeci { get; set; }
        public int ToCh { get; set; }
        public int ToChDeci { get; set; }
        public int LengthPaved { get; set; }
        public int LengthUnpaved { get; set; }
        public string Owner { get; set; }
        public object ModBy { get; set; }
        public DateTime ModDt { get; set; }
        public object CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public bool ActiveYn { get; set; }
        public int SecCode { get; set; }
        public string RmuName { get; set; }
        public object AssetId { get; set; }
    }

    public class SelectedRoadCodeResponse: ResponseBase
    {
        public SelectedRoadCodeResponseData data { get; set; }
    }

    public class FormJAddRefrenceNumber
    {
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public string data { get; set; }
    }


}

