
using UnityEngine;

namespace ChainSafe.Gaming.Exchangers
{
    public class RampExchangerAndroid : RampExchanger
    {
        private AndroidJavaClass _unityClass;
        private AndroidJavaObject _unityActivity;
        private AndroidJavaObject _pluginInstance;
        
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
            _pluginInstance.Call("startRamp", (RampConfiguration)_rampData);
        }
    }
    
    public class RampUnityBridge : AndroidJavaProxy
    {
        public RampUnityBridge() : base("io.chainsafe.web3.exchangers.ramp.RampUnityBridge") { }

        void offrampSendCrypto(OfframpAssetInfo asset, string s, string s1)
        {
            Debug.LogError("OFF RAMP SENT CRYPTO");
        }

        void onOffRampSaleCreated(OffRampSaleData offrampSale, string s, string s1)
        {
            Debug.LogError("OFF RAMP SALE");

        }

        void onPurchaseCreated(OnRampPurchaseData purchase, string s, string s1)
        {
            Debug.LogError("OFF RAMP PURCHASE CREATED!!!!");

        }

        void onPurchaseFailed()
        {
            
        }

        void onWidgetClose()
        {
            
        }
    }
    
    public class RampConfiguration
{
    public readonly string SwapAsset;
    public readonly string OfframpAsset;
    public readonly string SwapAmount;
    public readonly string FiatCurrency;
    public readonly string FiatValue;
    public readonly string UserAddress;
    public readonly string HostLogoUrl;
    public readonly string HostAppName;
    public readonly string UserEmailAddress;
    public readonly string SelectedCountryCode;
    public readonly string DefaultAsset;
    public readonly string Url;
    public readonly string WebhookStatusUrl;
    public readonly string FinalUrl;
    public readonly string ContainerNode;
    public readonly string HostApiKey;
    public readonly string OfframpWebHookV3Url;
    public readonly bool UseSendCryptoCallbackVersion;

    public RampConfiguration(
        string swapAsset, 
        string offrampAsset, 
        string swapAmount,
        string fiatCurrency,
        string fiatValue,
        string userAddress,
        string hostLogoUrl,
        string hostAppName,
        string userEmailAddress,
        string selectedCountryCode,
        string defaultAsset,
        string url,
        string webhookStatusUrl,
        string finalUrl,
        string containerNode,
        string hostApiKey,
        string offrampWebHookV3Url,
        bool useSendCryptoCallbackVersion)
    {
        SwapAsset = swapAsset;
        OfframpAsset = offrampAsset;
        SwapAmount = swapAmount;
        FiatCurrency = fiatCurrency;
        FiatValue = fiatValue;
        UserAddress = userAddress;
        HostLogoUrl = hostLogoUrl;
        HostAppName = hostAppName;
        UserEmailAddress = userEmailAddress;
        SelectedCountryCode = selectedCountryCode;
        DefaultAsset = defaultAsset;
        Url = url;
        WebhookStatusUrl = webhookStatusUrl;
        FinalUrl = finalUrl;
        ContainerNode = containerNode;
        HostApiKey = hostApiKey;
        OfframpWebHookV3Url = offrampWebHookV3Url;
        UseSendCryptoCallbackVersion = useSendCryptoCallbackVersion;
    }
}

}