using Xunit;
using ByteCodeExecutor;

namespace X64BytecodeExecutorTest
{
    public class X64BytecodeExecutorTest
    {
        private static X64BytecodeExecutor _executor = new X64BytecodeExecutor();
        [Fact]
        public void Execute_x64BytecodeReturn0_Return0()
        {
            byte[] x64BytecodeReturn0 =
            {
                0x48, 0xC7, 0xC0, 0x00, 0x00, 0x00, 0x00, //mov rax,0x0
                0xc3 // ret
            };
            var result = _executor.Execute(x64BytecodeReturn0);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Execute_x64BytecodeReturn0_Return1()
        {
            byte[] x64BytecodeReturn1 =
            {
                0x48, 0xC7, 0xC0, 0x01, 0x00, 0x00, 0x00, //mov rax,0x1
                0xc3 // ret
            };
            var result = _executor.Execute(x64BytecodeReturn1);
            Assert.Equal(1, result);
        }
    }
}