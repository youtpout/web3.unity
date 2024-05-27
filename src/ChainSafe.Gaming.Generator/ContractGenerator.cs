using ChainSafe.Gaming.Generator.Templates;
using Nethereum.Generators.Model;
using Nethereum.Generators.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChainSafe.Gaming.Generator
{
    public class ContractGenerator
    {
        public static string Generate(string abi, string contractName)
        {
            var contractAbi = new Nethereum.Generators.Net.GeneratorModelABIDeserialiser().DeserialiseABI(abi);
            var classModel = new ServiceModel(contractAbi, contractName, null, contractName, contractName, contractName);
            var template = new ServiceChainsafeTemplate(classModel);
            return template.GenerateClass();
        }


    }
}
