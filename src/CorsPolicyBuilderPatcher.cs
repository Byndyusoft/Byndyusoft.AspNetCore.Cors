using System.Reflection;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Byndyusoft.AspNetCore.Cors
{
    internal static class CorsPolicyBuilderPatcher
    {
        private static bool _patched;

        public static void Patch()
        {
            if (_patched)
                return;

            var bindings = BindingFlags.Instance | BindingFlags.Public;
            var methodToReplace = typeof(CorsPolicyBuilder).GetMethod(nameof(CorsPolicyBuilder.Build), bindings);
            var methodToInject =
                typeof(InsecuredCorsPolicyBuilder).GetMethod(nameof(InsecuredCorsPolicyBuilder.Build), bindings);

            MethodRental.SwapMethodBodies(methodToInject, methodToReplace);

            _patched = true;
        }
    }
}