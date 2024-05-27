using ChainSafe.Gaming.Evm.Contracts;
using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Signers;
using ChainSafe.Gaming.Web3.Analytics;
using ChainSafe.Gaming.Web3.Core.Evm;

namespace ChainSafe.Gaming.Generator
{
    public class GeneratedContract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratedContract"/> class.
        /// </summary>
        /// <param name="abi">The abi.</param>
        /// <param name="address">The contract address.</param>
        /// <param name="provider">The RPC provider.</param>
        /// <param name="signer">The signer.</param>
        /// <param name="transactionExecutor">Transaction executor.</param>
        public GeneratedContract(string abi, string address, IRpcProvider provider, ISigner signer = null, ITransactionExecutor transactionExecutor = null, IAnalyticsClient analyticsClient = null)
        {
            Contract = new Contract(abi, address, provider, signer, transactionExecutor, analyticsClient);
        }

        /// <summary>
        /// Gets the contract.
        /// </summary>
        public Contract Contract { get; private set; }
    }
}
