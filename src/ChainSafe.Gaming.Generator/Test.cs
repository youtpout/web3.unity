using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Signers;
using ChainSafe.Gaming.Generator;
using ChainSafe.Gaming.Web3.Analytics;
using ChainSafe.Gaming.Web3.Core.Evm;
using NBitcoin;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace ChainSafe.Gaming
{
    public partial class NFTContract : GeneratedContract
    {
        public NFTContract(string abi, string address, IRpcProvider provider, ISigner signer = null, ITransactionExecutor transactionExecutor = null, IAnalyticsClient analyticsClient = null) : base(abi, address, provider, signer, transactionExecutor, analyticsClient)
        {
        }

        public object[] approve(string to, string tokenId)
        {
            object[] response = this.Contract.Send(approve, new Object[] { to, tokenId });
            return response;
        }
        public object[] renounceOwnership()
        {
            object[] response = this.Contract.Send(renounceOwnership);
            return response;
        }
        public object[] safeMint(string to)
        {
            object[] response = this.Contract.Send(safeMint, new Object[] { to });
            return response;
        }
        public object[] safeTransferFrom(string from, string to, string tokenId)
        {
            object[] response = this.Contract.Send(safeTransferFrom, new Object[] { from, to, tokenId });
            return response;
        }
        public object[] safeTransferFrom(string from, string to, string tokenId, string data)
        {
            object[] response = this.Contract.Send(safeTransferFrom, new Object[] { from, to, tokenId, data });
            return response;
        }
        public object[] setApprovalForAll(string operator, string approved)
        {
            object[] response = this.Contract.Send(setApprovalForAll, new Object[] {operator, approved });
            return response;
        }
        public object[] transferFrom(string from, string to, string tokenId)
        {
            object[] response = this.Contract.Send(transferFrom, new Object[] { from, to, tokenId });
            return response;
        }
        public object[] transferOwnership(string newOwner)
        {
            object[] response = this.Contract.Send(transferOwnership, new Object[] { newOwner });
            return response;
        }
        public uint256 balanceOf(string owner)
        {
            object[] response = this.Contract.Call(balanceOf, new Object[] { owner });
            return response[0] as uint256;
        }
        public address getApproved(string tokenId)
        {
            object[] response = this.Contract.Call(getApproved, new Object[] { tokenId });
            return response[0] as address;
        }
        public bool isApprovedForAll(string owner, string operator)
        {
            object[] response = this.Contract.Call(isApprovedForAll, new Object[] { owner,operator});
            return response[0] as bool;
        }
        public string name()
        {
            object[] response = this.Contract.Call(name);
            return response[0] as string;
        }
        public address owner()
        {
            object[] response = this.Contract.Call(owner);
            return response[0] as address;
        }
        public address ownerOf(string tokenId)
        {
            object[] response = this.Contract.Call(ownerOf, new Object[] { tokenId });
            return response[0] as address;
        }
        public bool supportsInterface(string interfaceId)
        {
            object[] response = this.Contract.Call(supportsInterface, new Object[] { interfaceId });
            return response[0] as bool;
        }
        public string symbol()
        {
            object[] response = this.Contract.Call(symbol);
            return response[0] as string;
        }
        public string tokenURI(string tokenId)
        {
            object[] response = this.Contract.Call(tokenURI, new Object[] { tokenId });
            return response[0] as string;
        }


    }
}