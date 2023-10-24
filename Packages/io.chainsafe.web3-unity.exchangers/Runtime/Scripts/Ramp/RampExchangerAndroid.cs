using UnityEngine;

namespace ChainSafe.Gaming.Exchangers
{
    public class RampExchangerAndroid : RampExchanger
    {
        private readonly AndroidJavaObject _pluginInstance;
        private readonly AndroidJavaObject _unityActivity;
        private readonly AndroidJavaClass _unityClass;

        public RampExchangerAndroid(RampData rampData) : base(rampData)
        {
            _unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _unityActivity = _unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            _pluginInstance = new AndroidJavaObject("io.chainsafe.web3.exchangers.ramp.RampPluginInstance");
            _pluginInstance.CallStatic("setUnityActivity", _unityActivity);
            _pluginInstance.CallStatic("setUnityImplementation", new RampUnityBridge());
        }

        public override void OpenRamp()
        {
            var flowClass = new AndroidJavaClass("network.ramp.sdk.facade.Flow");
            var onrampFlow = flowClass.GetStatic<AndroidJavaObject>("ONRAMP");
            var configObject = new AndroidJavaObject("network.ramp.sdk.facade.Config",
                _rampData.HostAppName,
                _rampData.HostLogoUrl,
                _rampData.Url,
                _rampData.SwapAsset,
                _rampData.OfframpAsset,
                _rampData.SwapAmount,
                _rampData.FiatCurrency,
                _rampData.FiatValue,
                _rampData.UserAddress,
                _rampData.UserEmailAddress,
                _rampData.SelectedCountryCode,
                _rampData.DefaultAsset,
                _rampData.WebhookStatusUrl,
                _rampData.HostApiKey,
                onrampFlow, // Assuming the default flow is ONRAMP
                new AndroidJavaObject(
                    "java.util.HashSet"), // Empty set for 'enabledFlows' for simplicity; adjust if needed
                _rampData.OfframpWebHookV3Url,
                null, // Assuming `UseSendCryptoCallbackVersion` is nullable bool
                null);
            _pluginInstance.Call("startRamp", configObject);
        }
    }

    public class RampUnityBridge : AndroidJavaProxy
    {
        public RampUnityBridge() : base("io.chainsafe.web3.exchangers.ramp.RampUnityBridge")
        {
        }

        private void offrampSendCrypto(OfframpAssetInfo asset, string s, string s1)
        {
            Debug.LogError("OFF RAMP SENT CRYPTO");
        }

        private void onOffRampSaleCreated(OffRampSaleData offrampSale, string s, string s1)
        {
            Debug.LogError("OFF RAMP SALE");
        }

        private void onPurchaseCreated(OnRampPurchaseData purchase, string s, string s1)
        {
            Debug.LogError("OFF RAMP PURCHASE CREATED!!!!");
        }

        private void onPurchaseFailed()
        {
        }

        private void onWidgetClose()
        {
        }
    }


    public class RampConfiguration
    {
        public string containerNode;
        public string defaultAsset;
        public string fiatCurrency;
        public string fiatValue;
        public string finalUrl;
        public string hostApiKey;
        public string hostAppName;
        public string hostLogoUrl;
        public string offrampAsset;
        public string offrampWebhookV3Url;
        public string selectedCountryCode;
        public string swapAmount;
        public string swapAsset;
        public string url;
        public string userAddress;
        public string userEmailAddress;
        public bool useSendCryptoCallbackVersion;
        public string webhookStatusUrl;

        public RampConfiguration(
            string containerNode, string defaultAsset, string fiatCurrency,
            string fiatValue, string finalUrl, string hostApiKey,
            string hostAppName, string hostLogoUrl, string offrampAsset,
            string offrampWebhookV3Url, string selectedCountryCode,
            string swapAmount, string swapAsset, string url,
            string userAddress, string userEmailAddress,
            bool useSendCryptoCallbackVersion, string webhookStatusUrl
        )
        {
            this.containerNode = containerNode;
            this.defaultAsset = defaultAsset;
            this.fiatCurrency = fiatCurrency;
            this.fiatValue = fiatValue;
            this.finalUrl = finalUrl;
            this.hostApiKey = hostApiKey;
            this.hostAppName = hostAppName;
            this.hostLogoUrl = hostLogoUrl;
            this.offrampAsset = offrampAsset;
            this.offrampWebhookV3Url = offrampWebhookV3Url;
            this.selectedCountryCode = selectedCountryCode;
            this.swapAmount = swapAmount;
            this.swapAsset = swapAsset;
            this.url = url;
            this.userAddress = userAddress;
            this.userEmailAddress = userEmailAddress;
            this.useSendCryptoCallbackVersion = useSendCryptoCallbackVersion;
            this.webhookStatusUrl = webhookStatusUrl;
        }
    }
}