using System.Threading.Tasks;
using PulseAudio.Client.Internal;

namespace PulseAudio.Client
{
    public static class Pulse
    {
        public static Task<IContext> CreateContext(string applicationName, string server)
        {
            return Connect(new MainLoop(), applicationName, server);
        }

        private static Task<IContext> Connect(MainLoop loop, string applicationName, string server)
        {
            var ctx = new UnconnectedContext(loop, applicationName);
            return ctx.Connect(server);
        }
    }
}