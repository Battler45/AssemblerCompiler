using AssemblerCompiler;
using ByteCodeExecutor;
using System;
using Xunit;

namespace AssemblerCompilerTest
{
    public class X64AssemblerCompilerTest
    {
        private static X64BytecodeExecutor _executor = new X64BytecodeExecutor();
        private static X64AssemblerCompiler _compiler = new X64AssemblerCompiler();
        [Fact]
        public void Compile_CodeReturning0_Return0()
        {
            var code = $"mov rax, 1{Environment.NewLine}dec rax{Environment.NewLine}ret";
            var bytecod = _compiler.Compile(code);
            var result = _executor.Execute(bytecod);
            Assert.Equal(0, result);
        }
        [Fact]
        public void Compile_CodeReturning1_Return1()
        {
            var code = $"mov rax, 1{Environment.NewLine}ret";
            var bytecod = _compiler.Compile(code);
            var result = _executor.Execute(bytecod);
            Assert.Equal(1, result);
        }
    }
}