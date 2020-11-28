using System.Reflection;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Byndyusoft.AspNetCore.Cors
{
    public class InsecuredCorsPolicyBuilder : CorsPolicyBuilder
    {
        private FieldInfo _policyField;

        internal InsecuredCorsPolicyBuilder()
        {
        }

        public new CorsPolicy Build()
        {
            _policyField ??= typeof(CorsPolicyBuilder).GetField("_policy", BindingFlags.NonPublic | BindingFlags.Instance);
            return (CorsPolicy)_policyField?.GetValue(this);
        }
    }
}