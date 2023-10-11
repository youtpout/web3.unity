namespace ChainSafe.Gaming.Exchangers
{
    public static class RampExchangerFactory
    {
        public static RampExchanger CreateRampExchanger(RampData rampData)
        {
            #if UNITY_IOS
            return new RampExchangeriOS(rampData);
            #elif UNITY_ANDROID
            return new RampExchangerAndroid(rampData);
            #endif
            return null;
        }
    }
}