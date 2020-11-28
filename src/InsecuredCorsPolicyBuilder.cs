using System.Reflection;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Byndyusoft.AspNetCore.Cors
{
    public class InsecuredCorsPolicyBuilder : CorsPolicyBuilder
    {
        internal InsecuredCorsPolicyBuilder()
        {
        }

        public new CorsPolicy Build()
        {
            var policyField =
                typeof(CorsPolicyBuilder).GetField("_policy", BindingFlags.NonPublic | BindingFlags.Instance);
            return (CorsPolicy) policyField?.GetValue(this);
        }
    }
}