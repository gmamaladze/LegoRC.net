using System;
using Gsiot.Server;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.WebApi
{
    public class Class1
    {
        public void TestX()
        {
            var server = new HttpServer()
            {
                Port = 80,
                RequestRouting =
                {
                    {
                        "GET /",
                        context =>
                        {
                            Handler(context);
                        }
                    }
                }

            };

            server.Run();
        }

        private void Handler(RequestHandlerContext context)
        {
            const string defaultPageName = "index.html";
            var path = context.RequestUri;
            var suffix = path == string.Empty ? defaultPageName : path.Substring(1);

            context.SetResponse("hello", "text/plain");
        }
    }
}
