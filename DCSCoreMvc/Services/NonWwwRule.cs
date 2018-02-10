using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using System.Text;

namespace DCSCoreMvc.Services
{
    public class NonWwwRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            var currentHost = req.Host;
            if (currentHost.Host.StartsWith("www."))
            {
                var newHost = new HostString(currentHost.Host.Substring(4), currentHost.Port ?? 443);
                var newUrl = new StringBuilder().Append("https://").Append(newHost).Append(req.PathBase).Append(req.Path).Append(req.QueryString);
                context.HttpContext.Response.Redirect(newUrl.ToString(), false);
                context.Result = RuleResult.EndResponse;
            }
            else
            {
                if (req.Scheme == "http")
                {
                    var newUrl = $"http://www.{currentHost.Host}{req.PathBase}{req.Path}{req.QueryString}";
                        //new StringBuilder()
                        //.Append("https://")
                        //.Append("www.")
                        //.Append(new HostString(currentHost.Host))
                        //.Append(req.PathBase)
                        //.Append(req.Path)
                        //.Append(req.QueryString);
                    context.HttpContext.Response.Redirect(newUrl.ToString(), false);
                    context.Result = RuleResult.EndResponse;
                }
            }
            //else
            //{
            //    var newHost = new HostString(currentHost.Host, currentHost.Port ?? 443);
            //    var newUrl = new StringBuilder().Append("https://").Append(newHost).Append(req.PathBase).Append(req.Path).Append(req.QueryString);
            //    context.HttpContext.Response.Redirect(newUrl.ToString(), false);
            //    context.Result = RuleResult.EndResponse;
            //}
        }
    }
}
