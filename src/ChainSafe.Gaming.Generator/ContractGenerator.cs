using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChainSafe.Gaming.Generator
{
    public class ContractGenerator
    {
        public static void Generate(string abi)
        {
            var contractAbi = new Nethereum.Generators.Net.GeneratorModelABIDeserialiser().DeserialiseABI(abi);
        }


    }
}
