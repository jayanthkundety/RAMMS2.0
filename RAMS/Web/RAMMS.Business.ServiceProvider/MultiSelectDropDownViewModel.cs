using System;
using System.Collections.Generic;
using System.Text;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;

namespace RAMMS.Business.ServiceProvider
{
    public class MultiSelectDropDownViewModel
    {
        public List<string> AiStrucSuper_mltySelect { get; set; }
        public List<string> AiParapetType_mltySelect { get; set; }
        public List<string> AiBearingType_mltySelect { get; set; }
        public List<string> AiExpanType_mltySelect { get; set; }
        public List<string> AiDeckType_mltySelect { get; set; }
        public List<string> AiAbutType_mltySelect { get; set; }

        public List<string> AiPierType_mltySelect { get; set; }
        public List<string> AiAbutFound_mltySelect { get; set; }
        public List<string> AiPiersPrimComp_mltySelect { get; set; }
        public List<string> AiBearingSeatDiaphg_mltySelect { get; set; }
        public List<string> AiBeamsGridTrusArch_mltySelect { get; set; }
        public List<string> AiDeckPavement_mltySelect { get; set; }

        public List<string> AiUtilities_mltySelect { get; set; }
        public List<string> AiWaterway_mltySelect { get; set; }
        public List<string> AiWaterDownpipe_mltySelect { get; set; }
        public List<string> AiParapetRailing_mltySelect { get; set; }
        public List<string> AiSidewalksAppSlab_mltySelect { get; set; }
        public List<string> AiExpanJoint_mltySelect { get; set; }
        public List<string> AiSlopeRetainWall_mltySelect { get; set; }

   // public List<RmDdLookup> FullSelectedList { get; set; }
    }
}
