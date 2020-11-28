using System;
using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Byndyusoft.Reflection.Emit
{
    internal static class MethodRental
    {
        /// <see href="https://stackoverflow.com/questions/7299097/dynamically-replace-the-contents-of-a-c-sharp-method/36415711#36415711"/>
        public static void SwapMethodBodies(MethodInfo methodToInject, MethodInfo methodToReplace)
        {
            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);
            if (methodToReplace.IsVirtual)
            {
                ReplaceVirtualInner(methodToReplace, methodToInject);
            }
            else
            {
                ReplaceInner(methodToReplace, methodToInject);
            }
        }

        private static unsafe void ReplaceInner(MethodInfo methodToReplace, MethodInfo methodToInject)
        {
            if (IntPtr.Size == 4)
            {
                int* inj = (int*) methodToInject.MethodHandle.Value.ToPointer() + 2;
                int* tar = (int*) methodToReplace.MethodHandle.Value.ToPointer() + 2;
                *tar = *inj;
            }
            else
            {
                ulong* inj = (ulong*) methodToInject.MethodHandle.Value.ToPointer() + 1;
                ulong* tar = (ulong*) methodToReplace.MethodHandle.Value.ToPointer() + 1;
                *tar = *inj;
            }
        }

        private static unsafe void ReplaceVirtualInner(MethodInfo methodToReplace, MethodInfo methodToInject)
        {
            UInt64* methodDesc = (UInt64*) (methodToReplace.MethodHandle.Value.ToPointer());
            int index = (int) ((*methodDesc >> 32) & 0xFF);
            if (IntPtr.Size == 4)
            {
                uint* classStart = (uint*) methodToReplace.DeclaringType!.TypeHandle.Value.ToPointer();
                classStart += 10;
                classStart = (uint*) *classStart;
                uint* tar = classStart + index;

                uint* inj = (uint*) methodToInject.MethodHandle.Value.ToPointer() + 2;
                *tar = *inj;
            }
            else
            {
                ulong* classStart = (ulong*) methodToReplace.DeclaringType!.TypeHandle.Value.ToPointer();
                classStart += 8;
                classStart = (ulong*) *classStart;
                ulong* tar = classStart + index;

                ulong* inj = (ulong*) methodToInject.MethodHandle.Value.ToPointer() + 1;
                *tar = *inj;
            }
        }
    }
}