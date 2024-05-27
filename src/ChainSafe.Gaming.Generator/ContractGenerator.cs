using Nethereum.ABI.ABIDeserialisation;
using System.Linq;
using System.Text;

namespace ChainSafe.Gaming.Generator
{
    public class ContractGenerator
    {
        public static object SpaceUtils { get; private set; }

        public static string Generate(string abi, string contractName)
        {
            var contractAbi = ABIDeserialiserFactory.DeserialiseContractABI(abi);
            return GenerateClass(contractAbi, contractName);
        }

        private static string GenerateClass(Nethereum.ABI.Model.ContractABI contractAbi, string contractName)
        {
            return
                           $@"using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Signers;
using ChainSafe.Gaming.Web3.Analytics;
using ChainSafe.Gaming.Web3.Core.Evm;
using System.Numerics;
using System.Threading.Tasks;

namespace ChainSafe.Gaming
{{
    public partial class {contractName}: GeneratedContract
    {{
        public {contractName}(string abi, string address, IRpcProvider provider, ISigner signer = null, ITransactionExecutor transactionExecutor = null, IAnalyticsClient analyticsClient = null) : base(abi, address, provider, signer, transactionExecutor, analyticsClient)
        {{
        }}

{GenerateMethods(contractAbi.Functions)}
    }}
}}";
        }

        private static string GenerateMethods(Nethereum.ABI.Model.FunctionABI[] functionsList)
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


        private static string GenerateFunction(Nethereum.ABI.Model.FunctionABI functionAbi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"        public async ");
            if (functionAbi.OutputParameters?.Length > 0)
            {
                // if we have output parameter we return the first
                foreach (var outputParameter in functionAbi.OutputParameters)
                {
                    // todo support multiple output
                    sb.Append($"Task<{outputParameter.Type.ToString()}>");
                    break;
                }
            }
            else
            {
                // if we have don't output parameter we return object array
                sb.Append("Task<object[]>");
            }
            sb.Append($" {functionAbi.Name}(");
            if (functionAbi.InputParameters.Length > 0)
            {
                // support type
                var inputs = functionAbi.InputParameters.Select(x => $"string {x.Name}");
                sb.Append($"{string.Join(",", inputs)}");
            }
            sb.Append($")");
            sb.AppendLine();
            sb.AppendLine($"        {{");

            if (functionAbi.Constant)
            {
                sb.Append($"            object[] response = await this.Contract.Call(\"{functionAbi.Name}\"");

            }
            else
            {
                sb.Append($"            object[] response = await this.Contract.Send(\"{functionAbi.Name}\"");
            }

            if (functionAbi.InputParameters.Length > 0)
            {
                // generate the Object[] parameter
                var inputs = functionAbi.InputParameters.Select(x => x.Name);
                sb.Append($", new Object[] {{{string.Join(",", inputs)}}}");
            }
            sb.Append(");");
            sb.AppendLine();
            if (functionAbi.OutputParameters?.Length > 0)
            {
                // todo support multiple output
                sb.Append($"            return response[0] as {functionAbi.OutputParameters[0].Type.ToString()};");
            }
            else
            {
                sb.Append($"            return response;");
            }
            sb.AppendLine();
            sb.Append($"        }}");
            sb.AppendLine();
            return sb.ToString();
        }
    }

}
