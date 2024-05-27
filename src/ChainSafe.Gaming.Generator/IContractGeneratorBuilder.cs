using ChainSafe.Gaming.Evm.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSafe.Gaming.Generator
{
    public interface IContractGeneratorBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abi"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        T Build<T>(string abi, string address)
            where T : GeneratedContract;
    }
}
