using System.Runtime.InteropServices;

namespace ByteCodeExecutor
{
    public class X64BytecodeExecutor : BytecodeExecutor, IX64BytecodeExecutor
    {
        public long Execute(byte[] x64Bytecode)
        {
            if (8 != IntPtr.Size)
            {
                throw new Exception();
            }
            var codePointer = IntPtr.Zero;
            try
            {
                codePointer = VirtualAlloc(
                    IntPtr.Zero,
                    new UIntPtr((uint)x64Bytecode.Length),
                    AllocationType.COMMIT | AllocationType.RESERVE,
                    MemoryProtection.EXECUTE_READWRITE
                );
                Marshal.Copy(x64Bytecode, 0, codePointer, x64Bytecode.Length);
                var bytecodeMethod = (X64Delegate)Marshal.GetDelegateForFunctionPointer(codePointer, typeof(X64Delegate));
                return bytecodeMethod();
            }
            finally
            {
                if (codePointer != IntPtr.Zero)
                {
                    VirtualFree(codePointer, 0, 0x8000);
                }
            }
        }
    }
}