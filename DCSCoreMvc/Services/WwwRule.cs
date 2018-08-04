using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCSCoreMvc.Services
{
    public class WwwRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            var currentHost = req.Host;

            if (!currentHost.Host.StartsWith("www"))
            {
                var newHost = new HostString(currentHost.Host, currentHost.Port ?? 443);
                var newUrl = new StringBuilder().Append("https://www.").Append(newHost).Append(req.PathBase).Append(req.Path).Append(req.QueryString);
                context.HttpContext.Response.Redirect(newUrl.ToString(), false);
                context.Result = RuleResult.EndResponse;
            }

            //if (true)
            //{

            //    context.Result = RuleResult.EndResponse;
            //}

           
        }
    }
}
