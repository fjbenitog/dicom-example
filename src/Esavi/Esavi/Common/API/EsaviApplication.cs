using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace Esavi.Common.API
{
    public class EsaviApplication : IApplication
    {

        public Application? VMSApplication { get; private set; }
        public IUser CurrentUser { get; private set; }

        private EsaviApplication(Application? app, IUser currentUser)
        {
            VMSApplication = app;
            CurrentUser = currentUser;
        }

        public static EsaviApplication from(IUser user)
        {
            return new EsaviApplication(null, user);
        }

        public EsaviApplication Create()
        {

            using (Application app = Application.CreateApplication())
            {
                return new EsaviApplication(app, new EsaviUser(app.CurrentUser));
            }
        }

    }
}

