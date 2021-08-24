using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Filters.ResourceFilterVersionMangement
{
    public class Version1Expired : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.HttpContext.Request.Path.Value.ToLower().Contains("ver1"))
            {
                context.Result = new BadRequestObjectResult(
                     new
                     {
                         Version = new[] { "Version 1 has expired.Please chose another version" }
                     });

            }
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
