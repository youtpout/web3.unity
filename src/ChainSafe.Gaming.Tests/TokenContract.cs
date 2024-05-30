#pragma warning disable
using System.Numerics;
using System.Threading.Tasks;
using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Signers;
using ChainSafe.Gaming.Generator;
using ChainSafe.Gaming.Web3.Analytics;
using ChainSafe.Gaming.Web3.Core.Evm;

namespace ChainSafe.Gaming
{

    /// <summary>
    /// Generated with chainsafe contract generator.
    /// </summary>
    public partial class TokenContract : GeneratedContract
    {
        public TokenContract(string abi, string address, IRpcProvider provider, ISigner signer = null, ITransactionExecutor transactionExecutor = null, IAnalyticsClient analyticsClient = null) : base(abi, address, provider, signer, transactionExecutor, analyticsClient)
        {
        }

        public async Task<string> Name()
        {
            object[] response = await this.Contract.Call("name", new object[] { });
            return (string)response[0];
        }

        public async Task<bool> Approve(string _spender, BigInteger _value)
        {
            object[] response = await this.Contract.Send("approve", new object[] { _spender, _value });
            return (bool)response[0];
        }

        public async Task<BigInteger> Totalsupply()
        {
            object[] response = await this.Contract.Call("totalSupply", new object[] { });
            return (BigInteger)response[0];
        }

        public async Task<bool> Transferfrom(string _from, string _to, BigInteger _value)
        {
            object[] response = await this.Contract.Send("transferFrom", new object[] { _from, _to, _value });
            return (bool)response[0];
        }

        public async Task<byte> Decimals()
        {
            object[] response = await this.Contract.Call("decimals", new object[] { });
            return (byte)response[0];
        }

        public async Task<BigInteger> Balanceof(string _owner)
        {
            object[] response = await this.Contract.Call("balanceOf", new object[] { _owner });
            return (BigInteger)response[0];
        }

        public async Task<string> Symbol()
        {
            object[] response = await this.Contract.Call("symbol", new object[] { });
            return (string)response[0];
        }

        public async Task<bool> Transfer(string _to, BigInteger _value)
        {
            object[] response = await this.Contract.Send("transfer", new object[] { _to, _value });
            return (bool)response[0];
        }

        public async Task<BigInteger> Allowance(string _owner, string _spender)
        {
            object[] response = await this.Contract.Call("allowance", new object[] { _owner, _spender });
            return (BigInteger)response[0];
        }

    }
}