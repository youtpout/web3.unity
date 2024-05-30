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
            Assert.IsNotEmpty(generated);
        }
    }
}