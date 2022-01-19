using System.Collections.Immutable;

namespace AssemblerCompiler
{
    public class X64AssemblerGrammar
    {
        public ImmutableDictionary<string, int> InstructionsArgumentsCount { get; }
        public ImmutableDictionary<string, ImmutableArray<byte>> RegistersBytes { get; }
        public X64AssemblerGrammar()
        {
            var instructionsArgumentsCount = new Dictionary<string, int>()
            {

            };
            InstructionsArgumentsCount = instructionsArgumentsCount.ToImmutableDictionary();
            var registersBytes = new Dictionary<string, ImmutableArray<byte>>()
            {
                { "rax", new byte[] { 0x48, 0xFF, 0xC0 }.ToImmutableArray() },
                { "rbx", new byte[] { 0x48, 0xFF, 0xC3 }.ToImmutableArray() },
                { "rcx", new byte[] { 0x48, 0xFF, 0xC1 }.ToImmutableArray() },
                { "rdx", new byte[] { 0x48, 0xFF, 0xC2 }.ToImmutableArray() },
                { "rsi", new byte[] { 0x48, 0xFF, 0xC6 }.ToImmutableArray() },
                { "rdi", new byte[] { 0x48, 0xFF, 0xC7 }.ToImmutableArray() },
                { "rbp", new byte[] { 0x48, 0xFF, 0xC5 }.ToImmutableArray() },
                { "rsp", new byte[] { 0x48, 0xFF, 0xC4 }.ToImmutableArray() },
            };
            RegistersBytes = registersBytes.ToImmutableDictionary();

        }
    }
    public class X64AssemblerCompiler
    {
        private readonly X64AssemblerGrammar _grammar;
        public X64AssemblerCompiler()
        {
            _grammar = new X64AssemblerGrammar();
        }

        private byte[] CompileCodeLine(string codeLine)
        {
            var tokens = codeLine.Split(' ').ToArray();
            var instruction = tokens[0];
            if ("ret" == instruction)
            {
                return new byte[] { 0xC3 };
            }
            var registerName = tokens[1].Substring(0, 3);
            var firstRegisterBytes = _grammar.RegistersBytes[registerName].ToArray();
            if ("inc" == instruction)
            {
                return firstRegisterBytes;
            }
            else if ("dec" == instruction)
            {
                firstRegisterBytes[2] += 8;
                return firstRegisterBytes;
            }
            else if ("xor" == instruction)
            {
                var firstRegisterNumber = (byte) (firstRegisterBytes[2] & 0x0F);
                var secondRegisterNumber = (byte) (_grammar.RegistersBytes[tokens[2]][2] & 0x0F);
                var xorEnd = (byte)(0xC0 + firstRegisterNumber + 8 * secondRegisterNumber);
                return new byte[] { 0x48, 0x31, xorEnd };
            }
            else if ("mov" == instruction)
            {
                var number = BitConverter.GetBytes(int.Parse(tokens[2]));
                var movReg = new byte[] { firstRegisterBytes[0], 0xC7, firstRegisterBytes[2] };
                return movReg.Concat(number).ToArray();
            }
            return null;
        }
        public byte[] Compile(string assemblerCode)
        {
            var codeLines = assemblerCode.Split(Environment.NewLine).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            var bytecode = codeLines.Select(CompileCodeLine).Aggregate(new List<byte>(), (current, line) =>
            {
                current.AddRange(line);
                return current;
            }).ToArray();
            return bytecode;
        }
    }
}