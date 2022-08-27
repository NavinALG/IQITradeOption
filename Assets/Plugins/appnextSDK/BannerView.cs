using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace appnext
{
    public class BannerView : Ad
    {
        // Start is called before the first frame update
        public BannerView(string placementID) : base(placementID)
        {
        }

        public BannerView(string placementID, BannerConfig config) : base(placementID, config)
        {
           // setCreativeType(config.getCreativeType());
            //setAutoPlay(config.isAutoPlay());
           // setSkipText(config.getSkipText());
        }
        protected override void initAd(string placementID)
        {
#if UNITY_ANDROID
            createInstance("getBanners", placementID);
#elif UNITY_IPHONE
			this.adKey = AppnextIOSSDKBridge.createAd(AppnextIOSSDKBridge.AD_TYPE_INTERSTITIAL, placementID);
#endif
        }
        public void setBanneSize(string creativeType)
        {
#if UNITY_ANDROID
            instance.Call("setCreativeType", creativeType);
#elif UNITY_IPHONE
			AppnextIOSSDKBridge.setCreativeType(adKey, creativeType);
#endif
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
