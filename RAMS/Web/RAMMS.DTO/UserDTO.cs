using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
