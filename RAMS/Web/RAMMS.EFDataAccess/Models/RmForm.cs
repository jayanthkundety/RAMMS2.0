using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RmForm
    {
        public RmForm()
        {
            RmSchedule = new HashSet<RmSchedule>();
        }

        public int Pk { get; set; }
        public int? RmFormCategoryPk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual RmFormCategory RmFormCategoryPkNavigation { get; set; }
        public virtual ICollection<RmSchedule> RmSchedule { get; set; }
    }
}
