using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IFormW2Service
    {
        Task<int> Save(FormW2ResponseDTO formN1HeaderBO);

        Task<FormW2ResponseDTO> FindW2ByID(int id);

        int Delete(int id);

        Task<int> Update(FormW2ResponseDTO fornmDDtlDTO);
    }
}
