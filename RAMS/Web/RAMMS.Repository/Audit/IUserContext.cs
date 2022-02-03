using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Repository
{
    public interface IUserContext
    {
        int UserID { get; set; }
        string UserName { get; }
        string IPAddress { get; }
        string ActionMessage { get; set; }
    }
}
