using Esavi.Common.API;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
namespace Esavi.Example
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Execute(EsaviApplication.Create());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
            }
        }
        static void Execute(EsaviApplication app)
        {
            string message =
            "Current user is " + app.CurrentUser.Id + "\n\n" +
            "The number of patients in the database is " +
            "Press enter to quit...\n";
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}