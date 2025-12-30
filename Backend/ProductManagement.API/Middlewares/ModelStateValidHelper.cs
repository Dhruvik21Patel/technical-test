namespace ProductManagement.API.Middlewares
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using ProductManagement.Common.Exceptions;

    public class ModelStateValidHelper : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ModelStateException(context.ModelState);
            }
        }
    }
}