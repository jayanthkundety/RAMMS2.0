using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IProcessService
    {
        Task<int> Save(DTO.RequestBO.ProcessDTO process);
        void SaveNotification(RmUserNotification notification, bool isContextSave);

        List<Dictionary<string, object>> AuditHistory(string formName, int RefId);
    }
}
