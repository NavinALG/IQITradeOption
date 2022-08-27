using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;
using System;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Store;
#if RECEIPT_VALIDATION
using UnityEngine.Purchasing.Security;
#endif

public class SterterkitManager : MonoBehaviour, IStoreListener
{


    //public GameObject inAppPurchasesObj, carSelectObj, FreeCoinsObj, LevelSelectObj,MenuObj,MessagePopupObj;

    // Use this for initialization
    /*  public Image logoicon, leftmenuprofileicon, profileicon;
      public Sprite[] icon;
      public GameObject NotificationPurchase, NotificationPurchase1, crowntype, premiumContent, subscriberContent;
     
    public GameObject NotificationPurchase, NotificationPurchase1;
    public Text headtxt, error;*/
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string[] Products = new string[] { "starter_kit","starter_kit_4", "starter_kit_8"};
    public static string[] Productssubscribe = new string[] { "beginner_subs_4", "medium_subs_8", "pro_subs_15", "bizz_subs_25" };

   
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    // public Text error;
    int coins = 0;

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.

        foreach (string s in Products)
        {
            builder.AddProduct(s, ProductType.Consumable);
        }
        foreach (string s in Productssubscribe)
        {
            builder.AddProduct(s, ProductType.Subscription);
        }

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null & m_StoreExtensionProvider != null;
    }
    private IEnumerator PostRequeststarterCoroutine(string url, string json)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {



        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {


                PlayerPrefs.SetFloat("Total", PlayerPrefs.GetFloat("Total") + float.Parse(jsonNode["coins"]));
               
              
                Ui.tradeno = 3;
              

            }
            else
            {



            }

        }

    }
  /*  public void ShowPremiumContent()
    {
        //  Debug.Log("Premium");
        headtxt.text = "One Time Purchase";
        premiumContent.SetActive(true);
        subscriberContent.SetActive(false);
    }
    public void ShowSubscriberContent()
    {
        // Debug.Log("Subscription");
        headtxt.text = "Subscription Purchase";
        premiumContent.SetActive(false);
        subscriberContent.SetActive(true);
    }
    public void benifitspremium()
    {
        NotificationPurchase.SetActive(true);
    }
    public void benifitsSubscriber()
    {
        NotificationPurchase1.SetActive(true);
    }*/
    public void BuyStarterkit()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(Products[0]);
    }
    public void BuyStarterkit4()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(Products[1]);
    }
    public void BuyStarterkit8()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(Products[2]);
    }
    

    public void BuyConsumable_subs_4()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(Productssubscribe[0]);
    }
    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null & product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
              
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
              
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
         
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");


        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //
    private IAppleExtensions m_AppleExtensions;
    private IMoolahExtension m_MoolahExtensions;
    private ISamsungAppsExtensions m_SamsungExtensions;
    private IMicrosoftExtensions m_MicrosoftExtensions;
    private IUnityChannelExtensions m_UnityChannelExtensions;
    private ITransactionHistoryExtensions m_TransactionHistoryExtensions;
    private IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");
        //  error.text += "OnInitialized: PASS";
        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;


    }


    public void OnInitializeFailed(InitializationFailureReason error1)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error1);
        // error.text += "OnInitializeFailed InitializationFailureReason:" + error1;
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, Products[0], StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            coins = 4000;
            var url = "https://gamelovin.com/tradeoption/Trading/Userprocess/UpdateInAppStarter";
            string json = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"pack\":\"starter_kit\",\"coins\":\"" + coins + "\",\"usd\":\"4\"}";

            StartCoroutine(PostRequeststarterCoroutine(url, json));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[1], StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            coins =8000;
            var url = "https://gamelovin.com/tradeoption/Trading/Userprocess/UpdateInAppStarter";
            string json = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"pack\":\"starter_kit_4\",\"coins\":\"" + coins + "\",\"usd\":\"8\"}";

            StartCoroutine(PostRequeststarterCoroutine(url, json));

        }
        else if (String.Equals(args.purchasedProduct.definition.id, Products[2], StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            coins = 16000;
            var url = "https://gamelovin.com/tradeoption/Trading/Userprocess/UpdateInAppStarter";
            string json = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"pack\":\"starter_kit_8\",\"coins\":\"" + coins + "\",\"usd\":\"15\"}";

            StartCoroutine(PostRequeststarterCoroutine(url, json));
        }
        else
        {
            //error.text+= string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id);
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }


        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }



    void Start()
    {



        /*   if (PlayerPrefs.GetInt("crown") == 1)
           {
               crowntype.SetActive(true);
               if (PlayerPrefs.GetInt("subscriber") == 0)
               {
                   crowntype.transform.GetChild(0).GetComponent<Text>().text = "Premium Member";
               }
               else
               {
                   crowntype.transform.GetChild(0).GetComponent<Text>().text = "Subscriber Member";
               }

           }
           else
           {
               crowntype.SetActive(false);
           }*/
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }


    }

    // Update is called once per frame
    void Update()
    {


    }





}
