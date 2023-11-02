using UnityEngine;

namespace ChainSafe.Gaming.Exchangers
{
    public class RampExchangerAndroid : RampExchanger
    {
        private readonly AndroidJavaObject _unityActivity;
        private readonly AndroidJavaClass _unityClass;
        private readonly AndroidJavaObject _rampSDK;
        private readonly RampCallback _rampCallback;
        
        public RampExchangerAndroid(RampData rampData) : base(rampData)
        {
            _unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _rampSDK = new AndroidJavaObject("network.ramp.sdk.facade.RampSDK");
            _unityActivity = _unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            _rampCallback = new RampCallback();
        }

        public override void OpenRamp()
        {
            AndroidJavaClass flowClass = new AndroidJavaClass("network.ramp.sdk.facade.Flow");
            
            AndroidJavaObject onrampFlow = flowClass.GetStatic<AndroidJavaObject>("ONRAMP");
            AndroidJavaObject offrampFlow = flowClass.GetStatic<AndroidJavaObject>("OFFRAMP");
            
            AndroidJavaObject set = new AndroidJavaObject("java.util.HashSet");
            set.Call<bool>("add", onrampFlow);
            set.Call<bool>("add", offrampFlow);
            
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
                set , // Empty set for 'enabledFlows' for simplicity; adjust if needed
                _rampData.OfframpWebHookV3Url,
                null, // Assuming `UseSendCryptoCallbackVersion` is nullable bool
                null);
            Debug.Log("Starting transactions");
            
            _unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                _rampSDK.Call("startTransaction", _unityActivity, configObject,
                    _rampCallback, string.Empty);
            }));
        }
    }

  
    public class RampCallback : AndroidJavaProxy
    {
        public RampCallback() : base("network.ramp.sdk.facade.RampCallback") // Replace with the actual interface path
        { }

        public void onPurchaseFailed()
        {
            Debug.Log("MainActivity: onPurchaseFailed");
        }

        public void onPurchaseCreated(AndroidJavaObject purchase, string purchaseViewToken, string apiUrl)
        {
            Debug.Log("MainActivity: onPurchaseCreated");
        }

        public void onWidgetClose()
        {
            Debug.Log("MainActivity: onWidgetClose");
        }

        public void offrampSendCrypto(AndroidJavaObject assetInfo, string amount, string address)
        {
            // Note: You might need to retrieve fields from `assetInfo` using assetInfo.Get<type>("fieldName") as necessary
            Debug.Log($"MainActivity: offrampSendCrypto  assetInfo: {assetInfo} amount: {amount} address: {address}");
        }

        public void onOfframpSaleCreated(AndroidJavaObject sale, string saleViewToken, string apiUrl)
        {
            // Example fields extracted from the sale object
            string saleId = sale.Get<string>("id");
            string saleCreatedAt = sale.Get<string>("createdAt");
        
            // Assuming `crypto` is an inner object in `sale` which contains `amount` and `assetInfo` fields
            AndroidJavaObject crypto = sale.Get<AndroidJavaObject>("crypto");
            string cryptoAmount = crypto.Get<string>("amount");
            AndroidJavaObject cryptoAssetInfo = crypto.Get<AndroidJavaObject>("assetInfo");

            Debug.Log($"MainActivity: onOfframpSaleCreated {saleId} {saleCreatedAt} crypto: {cryptoAmount} {cryptoAssetInfo}");
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