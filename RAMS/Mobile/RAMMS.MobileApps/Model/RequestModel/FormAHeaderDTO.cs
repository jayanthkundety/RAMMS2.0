using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormAHeaderDTO
    {
        public FormAHeaderDTO()
        {
            FormADetails = new List<FormADetailsDTO>();
        }

        public int No { get; set; }
        
        public string Id { get; set; }
        public string FahRoadCode { get; set; }
        public string FahRmu { get; set; }
        public List<FormADetailsDTO> FormADetails { get; set; }
    }
}
