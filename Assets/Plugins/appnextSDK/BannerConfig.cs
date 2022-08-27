using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace appnext
{
    public class BannerConfig : AdConfig
    {
        public BannerConfig() : base()
        {
#if UNITY_ANDROID
            AndroidJavaObject instance = new AndroidJavaObject("com.appnext.banners.BannerView");
            initParamsFromAndroidJavaObject(instance);
#elif UNITY_IPHONE
#endif
        }
    }
}
