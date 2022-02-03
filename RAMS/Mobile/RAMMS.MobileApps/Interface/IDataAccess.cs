using System.Threading.Tasks;

namespace RAMMS.MobileApps
{
    public interface IDataAccess
    {
        Task UserLoggedOut();
    }
}