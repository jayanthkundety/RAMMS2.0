using System.Threading.Tasks;

namespace RAMMS.MobileApps
{
    public class DataAccess : IDataAccess
    {
        private ILocalDatabase _localDatabase;
        private IRestApi _restApi;

        public DataAccess(ILocalDatabase localDatabase, IRestApi restApi)
        {
            _restApi = restApi;
            _localDatabase = localDatabase;
        }

        public async Task UserLoggedOut()
        {
            var user = await _localDatabase.QuerySingle<RmUsers>(u => u.UsrUserName == AppState.UserCred.UsrUserName);

            if (user != null)
            {
                user.IsLoggedIn = false;
                // await _localDatabase.UpdateAsync(user);
            }
        }
    }
}