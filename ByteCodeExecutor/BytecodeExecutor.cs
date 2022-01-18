using System.Runtime.InteropServices;

namespace ByteCodeExecutor
{
    public abstract class BytecodeExecutor
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate int X86Delegate();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate long X64Delegate();

        [DllImport("kernel32.dll", SetLastError = true)]
        protected static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32")]
        protected static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, uint dwFreeType);

        [Flags]
        protected enum AllocationType : uint
        {
            COMMIT = 0x1000,
            RESERVE = 0x2000,
            RESET = 0x80000,
            LARGE_PAGES = 0x20000000,
            PHYSICAL = 0x400000,
            TOP_DOWN = 0x100000,
            WRITE_WATCH = 0x200000
        }

        [Flags]
        protected enum MemoryProtection : uint
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            EXECUTE_WRITECOPY = 0x80,
            NOACCESS = 0x01,
            READONLY = 0x02,
            READWRITE = 0x04,
            WRITECOPY = 0x08,
            GUARD_Modifierflag = 0x100,
            NOCACHE_Modifierflag = 0x200,
            WRITECOMBINE_Modifierflag = 0x400
        }
    }
}