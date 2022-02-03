using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public interface IDomainModelMapper<T1>
    {
        T1 AsDomainModel();
    }
}
