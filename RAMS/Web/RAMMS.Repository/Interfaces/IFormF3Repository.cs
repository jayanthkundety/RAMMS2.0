﻿using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormF3Repository : IRepositoryBase<RmFormF3Hdr>
    {
        //   Task<IEnumerable<RmFormF3Dtl>> FindFormF3DtlByID(int Id);

        Task<List<FormF3DtlGridDTO>> GetFormF3DtlGridList(FilteredPagingDefinition<FormF3DtlResponseDTO> filterOptions);

        int LoadG1G2Data(FormF3ResponseDTO FormF3);
        int? DeleteFormF3Dtl(int Id);
        int? SaveFormF3Dtl(RmFormF3Dtl FormF3Dtl);


    }
}
