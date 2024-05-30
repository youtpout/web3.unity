namespace ChainSafe.Gaming.Generator
{
    using System;
    using ChainSafe.Gaming.Evm.Providers;
    using ChainSafe.Gaming.Evm.Signers;
    using ChainSafe.Gaming.Web3.Analytics;
    using ChainSafe.Gaming.Web3.Core.Evm;

    public class ContractGeneratorBuilder : IContractGeneratorBuilder
    {
        private readonly IRpcProvider rpcProvider;
        private readonly ISigner signer;
        private readonly ITransactionExecutor transactionExecutor;
        private readonly IAnalyticsClient analyticsClient; // Added analytics client

        public ContractGeneratorBuilder(IRpcProvider rpcProvider, IAnalyticsClient analyticsClient)
            : this(rpcProvider, null, null, analyticsClient)
        {
        }

        public ContractGeneratorBuilder(IRpcProvider rpcProvider, ISigner signer, IAnalyticsClient analyticsClient)
            : this(rpcProvider, signer, null, analyticsClient)
        {
        }

        public ContractGeneratorBuilder(IRpcProvider rpcProvider, ISigner signer, ITransactionExecutor transactionExecutor, IAnalyticsClient analyticsClient)
        {
            this.rpcProvider = rpcProvider;
            this.signer = signer;
            this.transactionExecutor = transactionExecutor;
            this.analyticsClient = analyticsClient; // Initialize analytics client
        }

        public ISigner Signer => signer;

        public T Build<T>(string abi, string address)
              where T : GeneratedContract
        {
            return (T)Activator.CreateInstance(typeof(T), abi, address, rpcProvider, Signer, transactionExecutor, analyticsClient); // Pass analytics client to Contract
        }
    }
}
