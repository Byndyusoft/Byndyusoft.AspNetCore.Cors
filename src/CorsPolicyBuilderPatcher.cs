using System;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            var source = typeof(CorsPolicyBuilder).GetMethod(nameof(CorsPolicyBuilder.Build), bindings);
            var patch = typeof(PatchedCorsPolicyBuilder).GetMethod(nameof(PatchedCorsPolicyBuilder.Build), bindings);

            SwapMethodBodies(patch, source);

            _patched = true;
        }

        private class PatchedCorsPolicyBuilder : CorsPolicyBuilder
        {
            public new CorsPolicy Build()
            {
                var policyField =
                    typeof(CorsPolicyBuilder).GetField("_policy", BindingFlags.NonPublic | BindingFlags.Instance);
                return (CorsPolicy)policyField?.GetValue(this);
            }
        }

        /// <see href="https://stackoverflow.com/questions/7299097/dynamically-replace-the-contents-of-a-c-sharp-method/36415711#36415711"/>
        private static void SwapMethodBodies(MethodInfo methodToInject, MethodInfo methodToReplace)
        {
#if !DEBUG
            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);
#endif
            unsafe
            {
                if (IntPtr.Size == 4) // x86
                {
                    int* inj = (int*)methodToInject.MethodHandle.Value.ToPointer() + 2;
                    int* tar = (int*)methodToReplace.MethodHandle.Value.ToPointer() + 2;
#if DEBUG
                    byte* injInst = (byte*)*inj;
                    byte* tarInst = (byte*)*tar;
                    int* injSrc = (int*)(injInst + 1);
                    int* tarSrc = (int*)(tarInst + 1);
                    *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
#else
                    *tar = *inj;
#endif
                }
                else // x64
                {
                    long* inj = (long*)methodToInject.MethodHandle.Value.ToPointer() + 1;
                    long* tar = (long*)methodToReplace.MethodHandle.Value.ToPointer() + 1;
#if DEBUG
                    byte* injInst = (byte*)*inj;
                    byte* tarInst = (byte*)*tar;
                    int* injSrc = (int*)(injInst + 1);
                    int* tarSrc = (int*)(tarInst + 1);
                    *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
#else
                    *tar = *inj;
#endif
                }
            }
        }
    }
}