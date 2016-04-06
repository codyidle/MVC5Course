using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class Action執行時間Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.StartTime = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.EndTime = DateTime.Now;

            var timeSpan = (DateTime)filterContext.Controller.ViewBag.EndTime - (DateTime)filterContext.Controller.ViewBag.StartTime;

            filterContext.Controller.ViewBag.TimeSpan = timeSpan;
            base.OnActionExecuted(filterContext);
        }
    }
}