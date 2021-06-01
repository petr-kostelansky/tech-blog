using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TechBlog.Contract.Exceptions;
using TechBlog.Web.Common;
using TechBlog.Web.Common.Enums;

namespace TechBlog.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string ErrorMessage
        {
            get { return TempData[TempDataEnum.ErrorMessage.ToString()] as string; }
            set { TempData[TempDataEnum.ErrorMessage.ToString()] = value; }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            ViewBag.IsRelease = true;

#if DEBUG
            ViewBag.IsRelease = false;
#endif

            return base.BeginExecute(requestContext, callback, state);
        }


        [HttpGet]
        public ActionResult MyRedirect(string url)
        {
            return Redirect(url);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception != null)
            {
                if (Request.IsAjaxRequest())
                {
                    ResolveAjaxException(filterContext, filterContext.Exception);
                }
                else
                {
                    ResolveException(filterContext, filterContext.Exception);
                }
            }

            if (!filterContext.ExceptionHandled)
            {
                base.OnException(filterContext);
            }
        }

        private void ResolveException(ExceptionContext filterContext, Exception ex)
        {
            string message;
            HttpStatusCode statusCode;
            ActionResult result = null;

            if (ex is ValidationException)
            {
                var vex = ex as ValidationException;
                message = vex.ValidationErrors.Aggregate(new StringBuilder("<ul>"), (sb, text) => sb.AppendFormat("<li>{0}</li>", text), sb => sb.Append("</ul>").ToString());
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (ex is ServiceException)
            {
                var vex = ex as ServiceException;
                message = vex.Message;
                statusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                Log.Instance.Error(ex);
                message = null; //ex.Message;
                statusCode = HttpStatusCode.InternalServerError;
            }

            ErrorMessage = message;
            filterContext.Result = result ?? RedirectToAction("ApplicationError", "Error", new { Area = "" });
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        private void ResolveAjaxException(ExceptionContext filterContext, Exception ex)
        {
            string message;
            HttpStatusCode statusCode;

            if (ex is ValidationException)
            {
                var vex = ex as ValidationException;
                message = vex.ValidationErrors.Aggregate(new StringBuilder("<ul>"), (sb, text) => sb.AppendFormat("<li>{0}</li>", text), sb => sb.Append("</ul>").ToString());
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (ex is ServiceException)
            {
                var vex = ex as ServiceException;
                message = vex.Message;
                statusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                Log.Instance.Error(ex);
                message = ex.Message;
                statusCode = HttpStatusCode.InternalServerError;
#if DEBUG
                //message += CreateDebugMessage(ex);
#endif
            }

            filterContext.Result = Json(message, JsonRequestBehavior.AllowGet);
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        protected void HandleException(Exception ex)
        {
            string message = null;
            if (ex is ValidationException)
            {
                var vex = ex as ValidationException;
                message = vex.ValidationErrors.Aggregate(new StringBuilder("<ul style=\"margin: 0\">"), (sb, text) => sb.AppendFormat("<li>{0}</li>", text), sb => sb.Append("</ul>").ToString());
            }
            else if (ex is ServiceException)
            {
                Log.Instance.Warn(ex);
                var vex = ex as ServiceException;
                message = vex.Message;
            }
            else
            {
                Log.Instance.Error(ex);
                message = "Internal error";
            }

            ErrorMessage = message;
        }
    }
}