﻿using Nethereum.Generators.Core;
using Nethereum.Generators.CQS;
using Nethereum.Generators.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSafe.Gaming.Generator.Templates
{
    /// <summary>
    /// Create custom template to generate class compatible with chainsafe sdk
    /// https://github.com/Nethereum/Nethereum/blob/319e1b7faa6eaab5523187799a2831b0ed7e4d96/generators/Nethereum.Generators/Service/CSharp/ServiceCSharpTemplate.cs#L4
    /// </summary>
    public class ServiceChainsafeTemplate : ClassTemplateBase<ServiceModel>
    {
        private FunctionChainsafeTemplate _functionChainsafeTemplate;

        public ServiceChainsafeTemplate(ServiceModel model) : base(model)
        {
            _functionChainsafeTemplate = new FunctionChainsafeTemplate(model);
            ClassFileTemplate = new CSharpClassFileTemplate(Model, this);
        }

        public override string GenerateClass()
        {
            return
               $@"{SpaceUtils.OneTab}public partial class {Model.GetTypeName()}: ContractWeb3ServiceBase
{SpaceUtils.OneTab}{{
{SpaceUtils.TwoTabs}public {Model.GetTypeName()}(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
{SpaceUtils.TwoTabs}{{
{SpaceUtils.TwoTabs}}}
{SpaceUtils.NoTabs}
{_functionChainsafeTemplate.GenerateMethods()}
{SpaceUtils.OneTab}}}";
        }
    }
}

