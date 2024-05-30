using System;
using System.Diagnostics;
using System.IO;
using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Transactions;
using ChainSafe.Gaming.Evm.Utils;
using ChainSafe.Gaming.Generator;
using ChainSafe.Gaming.Tests.Node;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using NUnit.Framework;

namespace ChainSafe.Gaming.Tests
{
    /// <summary>
    /// Unit tests for ChainSafe.Gaming.Generator library, testing file generation.
    /// </summary>
    [TestFixture]
    public class ChainsafeGenerator
    {
        private readonly string erc20ABI = File.ReadAllText(Directory.GetCurrentDirectory() + "../../../../Resources/Erc20.abi.json");
        private readonly string expectedClass = "#pragma warning disable\r\nusing System.Numerics;\r\nusing System.Threading.Tasks;\r\nusing ChainSafe.Gaming.Evm.Providers;\r\nusing ChainSafe.Gaming.Evm.Signers;\r\nusing ChainSafe.Gaming.Web3.Analytics;\r\nusing ChainSafe.Gaming.Web3.Core.Evm;\r\n\r\nnamespace ChainSafe.Gaming\r\n{\r\n\r\n    /// <summary>\r\n    /// Generated with chainsafe contract generator.\r\n    /// </summary>\r\n    public partial class TokenContract: GeneratedContract\r\n    {\r\n        public TokenContract(string abi, string address, IRpcProvider provider, ISigner signer = null, ITransactionExecutor transactionExecutor = null, IAnalyticsClient analyticsClient = null) : base(abi, address, provider, signer, transactionExecutor, analyticsClient)\r\n        {\r\n        }\r\n\r\n        public async Task<string> Name() \r\n        {\r\n            object[] response = await this.Contract.Call(\"name\", new object[] {});\r\n            return (string)response[0]; \r\n        }\r\n\r\n        public async Task<bool> Approve(string _spender, BigInteger _value) \r\n        {\r\n            object[] response = await this.Contract.Send(\"approve\", new object[] {_spender, _value});\r\n            return (bool)response[0]; \r\n        }\r\n\r\n        public async Task<BigInteger> Totalsupply() \r\n        {\r\n            object[] response = await this.Contract.Call(\"totalSupply\", new object[] {});\r\n            return (BigInteger)response[0]; \r\n        }\r\n\r\n        public async Task<bool> Transferfrom(string _from, string _to, BigInteger _value) \r\n        {\r\n            object[] response = await this.Contract.Send(\"transferFrom\", new object[] {_from, _to, _value});\r\n            return (bool)response[0]; \r\n        }\r\n\r\n        public async Task<byte> Decimals() \r\n        {\r\n            object[] response = await this.Contract.Call(\"decimals\", new object[] {});\r\n            return (byte)response[0]; \r\n        }\r\n\r\n        public async Task<BigInteger> Balanceof(string _owner) \r\n        {\r\n            object[] response = await this.Contract.Call(\"balanceOf\", new object[] {_owner});\r\n            return (BigInteger)response[0]; \r\n        }\r\n\r\n        public async Task<string> Symbol() \r\n        {\r\n            object[] response = await this.Contract.Call(\"symbol\", new object[] {});\r\n            return (string)response[0]; \r\n        }\r\n\r\n        public async Task<bool> Transfer(string _to, BigInteger _value) \r\n        {\r\n            object[] response = await this.Contract.Send(\"transfer\", new object[] {_to, _value});\r\n            return (bool)response[0]; \r\n        }\r\n\r\n        public async Task<BigInteger> Allowance(string _owner, string _spender) \r\n        {\r\n            object[] response = await this.Contract.Call(\"allowance\", new object[] {_owner, _spender});\r\n            return (BigInteger)response[0]; \r\n        }\r\n\r\n    }\r\n}";

        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }

        // Various unit tests for ChainSafe.Gaming Web3 library methods

        /// <summary>
        /// Test method to generate c# class from abi.
        /// </summary>
        [Test]
        public void TestGenerateClass()
        {
            var generated = ContractGenerator.Generate(erc20ABI, "TokenContract");
            Debug.WriteLine(generated);
            Assert.AreEqual(expectedClass, generated);
        }
    }
}