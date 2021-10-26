using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGenerator.ActionFilters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var param = context.ActionArguments.SingleOrDefault(p => p.Value is Pics);

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
