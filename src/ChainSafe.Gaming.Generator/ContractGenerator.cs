using Nethereum.ABI.ABIDeserialisation;
using Nethereum.ABI.Model;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChainSafe.Gaming.Generator
{
    /// <summary>
    /// Helper to convert solidity abi to c# class.
    /// </summary>
    public static class ContractGenerator
    {

        /// <summary>
        /// Gerate a c# class from abi to facilitate smartcontracts call.
        /// </summary>
        /// <param name="abi">the json abi.</param>
        /// <param name="contractName">the contract name.</param>
        /// <returns>the c# class in string format.</returns>
        public static string Generate(this string abi, string contractName)
        {
            var contractAbi = ABIDeserialiserFactory.DeserialiseContractABI(abi);
            return GenerateClass(contractAbi, contractName);
        }

        private static string GenerateClass(Nethereum.ABI.Model.ContractABI contractAbi, string contractName)
        {
            return $@"
#pragma warning disable
using System.Numerics;
using System.Threading.Tasks;
using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Signers;
using ChainSafe.Gaming.Web3.Analytics;
using ChainSafe.Gaming.Web3.Core.Evm;

namespace ChainSafe.Gaming
{{

    /// <summary>
    /// Generated with chainsafe contract generator.
    /// </summary>
    public partial class {contractName}: GeneratedContract
    {{
        public {contractName}(string abi, string address, IRpcProvider provider, ISigner signer = null, ITransactionExecutor transactionExecutor = null, IAnalyticsClient analyticsClient = null) : base(abi, address, provider, signer, transactionExecutor, analyticsClient)
        {{
        }}
{GenerateMethods(contractAbi.Functions)}
    }}
}}";
        }

        private static string GenerateMethods(FunctionABI[] functionsList)
        {
            StringBuilder sb = new StringBuilder();
            if (functionsList?.Length > 0)
            {
                foreach (var function in functionsList)
                {
                    sb.AppendLine(GenerateFunction(function));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }


        private static string GenerateFunction(FunctionABI functionAbi)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US", false);
            TextInfo textInfo = cultureInfo.TextInfo;
            return $@"
        public async {GetFunctionReturnType(functionAbi)} {textInfo.ToTitleCase(functionAbi.Name)}({GetFunctionParameters(functionAbi)}) 
        {{
            {GetFunctionCall(functionAbi)}
            {GetFunctionCallReturnType(functionAbi)} 
        }}";
        }

        private static string GetFunctionReturnType(FunctionABI functionAbi)
        {
            if (functionAbi.OutputParameters?.Length == 1)
            {
                // if we have one output parameter we return the corespondant type
                var outputParameter = functionAbi.OutputParameters[0];
                return $"Task<{outputParameter.Type.Convert()}>";
            }
            else
            {
                // return default type if we don't have only one return type
                return "Task<object[]>";
            }
        }

        private static string GetFunctionParameters(FunctionABI functionAbi)
        {
            if (functionAbi.InputParameters.Length > 0)
            {
                // support type
                var inputs = functionAbi.InputParameters.Select(x => $"{x.Type.Convert()} {x.Name}");
                return $"{string.Join(", ", inputs)}";
            }
            else
            {
                return string.Empty;
            }
        }

        private static string GetFunctionCall(FunctionABI functionAbi)
        {
            var inputText = string.Empty;
            if (functionAbi.InputParameters?.Length > 0)
            {
                var inputs = functionAbi.InputParameters.Select(x => $"{x.Name}");
                inputText = string.Join(", ", inputs);
            }

            if (functionAbi.Constant)
            {
                return $"object[] response = await this.Contract.Call(\"{functionAbi.Name}\", new object[] {{{inputText}}});";
            }
            else
            {
                return $"object[] response = await this.Contract.Send(\"{functionAbi.Name}\", new object[] {{{inputText}}});";
            }
        }

        private static string GetFunctionCallReturnType(FunctionABI functionAbi)
        {
            if (functionAbi.OutputParameters?.Length == 1)
            {
                // if we have one output parameter we return the corespondant type
                var outputParameter = functionAbi.OutputParameters[0];
                return $"return ({outputParameter.Type.Convert()})response[0];";
            }
            else
            {
                // return default type if we don't have only one return type
                return "return response;";
            }
        }
    }
}
