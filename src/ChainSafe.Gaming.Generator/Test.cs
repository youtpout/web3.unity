using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Signers;
using ChainSafe.Gaming.Web3.Analytics;
using ChainSafe.Gaming.Web3.Core.Evm;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChainSafe.Gaming.Generator
{
    public class NftTest : GeneratedContract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NftTest"/> class.
        /// </summary>
        /// <param name="abi">The abi.</param>
        /// <param name="address">The contract address.</param>
        /// <param name="provider">The RPC provider.</param>
        /// <param name="signer">The signer.</param>
        /// <param name="transactionExecutor">Transaction executor.</param>
        public NftTest(string abi, string address, IRpcProvider provider, ISigner signer = null, ITransactionExecutor transactionExecutor = null, IAnalyticsClient analyticsClient = null)
            : base(abi, address, provider, signer, transactionExecutor, analyticsClient)
        {
        }

        /// <summary>
        /// Mint a new nft.
        /// </summary>
        /// <param name="id">id of the nft</param>
        /// <returns>id of the new nft</returns>
        public async Task<BigInteger> Mint(BigInteger id)
        {
            object[] response = await Contract.Send("mint", new object[] { id });
            return BigInteger.Parse(response[0].ToString());
        }
    }
}
