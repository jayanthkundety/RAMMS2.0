namespace RAMMS.MobileApps
{
    public class FormA
    {
    }

    public class EditViewModel
    {
        public string Type { get; set; }

        public int HdrFahPkRefNo { get; set; }

        public int HdrFahRefNo { get; set; }

        public string Id { get; set; }

        public string RoadCode { get; set; }

        public string Rmu { get; set; }

        public string RoadName { get; set; }

        public string Section { get; set; }

        public string AssetGroupCode { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public int? dtlserialNo { get; set; }

        ////DetailView
        //public int HeaderNo { get; set; }

        //public int? ReferenceId { get; set; }

        //public string AssetId { get; set; }

        //public DateTime? Dt { get; set; }

        //public int? Srno { get; set; }

        //public string SiteRef { get; set; }

        //public int? FromCh { get; set; }

        //public int? FromChDeci { get; set; }

        //public int? ToCh { get; set; }

        //public int? ToChDeci { get; set; }

        //public string DefCode { get; set; }

        //public string ActCode { get; set; }

        //public string Unit { get; set; }

        //public int? Length { get; set; }

        //public int? Width { get; set; }

        //public int? Height { get; set; }

        //public string Adp { get; set; }

        //public string Cdr { get; set; }

        //public string Priority { get; set; }

        //public int? Wi { get; set; }

        //public int? Ws { get; set; }

        //public int? Wtc { get; set; }

        //public int? Wc { get; set; }

        //public int? SftPs { get; set; }

        //public int? SftWis { get; set; }

        //public int? Rt { get; set; }

        //public string Remarks { get; set; }

        //public string FormhApp { get; set; }


        //public string Desc { get; set; }


    }
    public class DDRodeCode
    {
        public int RoadCode { get; set; }

        public string RoadName { get; set; }

        public string Rode_Description { get; set; }
    }

    public class AdvanceSearch
    {

        public string RoadCode { get; set; }

        public string RMU { get; set; }

        public string Asset_Type { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public string Chainage_From { get; set; }

        public string Chainage_To { get; set; }
    }
}