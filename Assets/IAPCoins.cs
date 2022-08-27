using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;

public class IAPCoins : MonoBehaviour, IStoreListener
{
    private IStoreController controller;

    //The following products must be added to the Product Catalog in the Editor:
    private const string coins4 = "beginner_4";
    private const string coins8 = "medium_8";
    private const string coins15 = "pro_15";
    private const string coins25 = "bizz_25";

    public int coin_count = 0;

    void Awake()
    {
        StandardPurchasingModule module = StandardPurchasingModule.Instance();
        ProductCatalog catalog = ProductCatalog.LoadDefaultCatalog();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, catalog);
        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller; Debug.Log("Initialization Successful");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("UnityIAP.OnInitializeFailed (" + error + ")");
    }

    public void OnPurchaseFailed(Product item, PurchaseFailureReason reason)
    {
        Debug.Log("UnityIAP.OnPurchaseFailed (" + item + ", " + reason + ")");
    }
    private IEnumerator PostRequestrewardsCoroutine(string url, string json)
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

            }
            else
            {



            }

        }

    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        string purchasedItem = e.purchasedProduct.definition.id;

        switch (purchasedItem)
        {
            case coins4:
                Debug.Log("Congratulations, you are richer!");
                var url = AppLoad.BaseUrl + "UpdateInApp";
                string json = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"pack\":\"Beginner_4\",\"coins\":\"2000\",\"usd\":\"4\"}";

                StartCoroutine(PostRequestrewardsCoroutine(url, json));
                break;

            case coins8:
                var url1 = AppLoad.BaseUrl + "UpdateInApp";
                string json1 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"pack\":\"Medium_8\",\"coins\":\"4000\",\"usd\":\"8\"}";

                StartCoroutine(PostRequestrewardsCoroutine(url1, json1));
                break;
            case coins15:
              
                var url2 = AppLoad.BaseUrl + "UpdateInApp";
                string json2 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"pack\":\"Pro_15\",\"coins\":\"9000\",\"usd\":\"15\"}";

                StartCoroutine(PostRequestrewardsCoroutine(url2, json2));
                break;
            case coins25:
                var url3 = AppLoad.BaseUrl + "UpdateInApp";
                string json3 = "{\"unique_id\":\"" + PlayerPrefs.GetString("uniqueid") + "\",\"pack\":\"Bizz_25\",\"coins\":\"20000\",\"usd\":\"25\"}";

                StartCoroutine(PostRequestrewardsCoroutine(url3, json3));
                break;
        }
        return PurchaseProcessingResult.Complete;
    }

    public void Buy(string productId)
    {
        Debug.Log("UnityIAP.BuyClicked (" + productId + ")");
        controller.InitiatePurchase(productId);
    }
}
