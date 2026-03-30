using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace Esavi.Common.API
{
    public class EsaviUser : IUser
    {
        public User? WMSUser { get; private set; }

        public string Id { get; }

        internal EsaviUser(User user)
        {
            WMSUser = user;
            Id = user.Id;
        }

        public EsaviUser(string id)
        {
            Id = id;
        }

    }
}