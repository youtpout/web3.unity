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
        private readonly string nft721ABI = File.ReadAllText(Directory.GetCurrentDirectory() + "../../../../Resources/Erc721.abi.json");

        // Various unit tests for ChainSafe.Gaming Web3 library methods

        /// <summary>
        /// Test method to get the network information.
        /// </summary>
        [Test]
        public void GenerateClass()
        {
            var generated = ContractGenerator.Generate(nft721ABI, "NFTContract");
            Assert.IsNotEmpty(generated);
        }
    }
}