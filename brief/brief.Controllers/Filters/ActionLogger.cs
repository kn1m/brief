namespace brief.Controllers.Filters
{
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using log4net;

    public class ActionLogger : ActionFilterAttribute
    {
        private readonly ILog _logger;
        private string _controllerName;
        private string _actionName;

        public ActionLogger(ILog logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            _actionName = actionContext.ActionDescriptor.ActionName;
            _logger.Info(
                   $"Action {_actionName} in {_controllerName} controller started");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            _logger.Info(
                   $"Action {_actionName} in {_controllerName} controller was executed");
        }
    }
}
