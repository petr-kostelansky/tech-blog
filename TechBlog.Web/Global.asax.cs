using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Routing;
using TechBlog.Web.App_Start;
using TechBlog.Web.Common;
using TechBlog.Contract.Exceptions;
using TechBlog.Interfaces.Domain.Share;
using TechBlog.Domain.Share.Services;

namespace TechBlog.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WindsorActivator.PreStart();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            WindsorActivator.Shutdown();
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            IEmailService emailService = new EmailService();
            List<string> to = new List<string> { "email@domain.com" };

            var code = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500;
            if (code == 404 || ex is ServiceException)
            {
                string notFoundPath = null;
                try
                {
                    notFoundPath = Context.Request.RequestContext.HttpContext.Request.Url.PathAndQuery;
                }
                catch (Exception)
                {
                    notFoundPath = ex.Message;
                }

                NotFoundLog.Instance.Info(notFoundPath);
#if !DEBUG
                emailService.SendEmail(to, "NotFound", notFoundPath);
#endif
                Response.Redirect("~/error/notfound");

                // Clear the error from the server
                Server.ClearError();
                return;
            }

            Log.Instance.Error(ex, "GlobalError");
#if !DEBUG
            emailService.SendEmail(to, "GlobalError", ex.Message + ex.StackTrace);
#endif
            Response.Redirect("~/Error/ApplicationError");
            Server.ClearError();
        }
    }
}
