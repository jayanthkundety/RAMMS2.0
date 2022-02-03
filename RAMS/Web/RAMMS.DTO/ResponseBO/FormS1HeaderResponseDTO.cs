using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.ResponseBO
{
    public class FormS1HeaderResponseDTO
    {
        public FormS1HeaderResponseDTO()
        {
         
        }
        [MapTo("FsihPkRefNo")]
        public int No { get; set; }

        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
       
    }
}
