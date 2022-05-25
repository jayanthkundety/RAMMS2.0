using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IFormF3Repository : IRepositoryBase<RmFormF3Hdr>
    {
        int SaveFormF3(RmFormF3Hdr FormF3);

        //   Task<IEnumerable<RmFormF3Dtl>> FindFormF3DtlByID(int Id);
        int? DeleteFormF3Dtl(int Id);
        int? SaveFormF3Dtl(RmFormF3Dtl FormF3Dtl);


    }
}
