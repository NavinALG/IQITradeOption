using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using SimpleJSON;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class GoogleSignInDemo : MonoBehaviour
{
    //public Text infoText;
    public string webClientId = "<your client id here>";
    public Text url3;
    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    public GameObject circle,errormsg,UserbanPanel;
    public mainui MainUI;
    public Ui ui;
   // public InterstitialAdScene interstaialads;
   // public AdViewScene view;
    public Text info;
    //public GameObject OffScreen;
   // var url = "https://gamelovin.com/Trading/Userprocess/signup";
    private void Awake()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        CheckFirebaseDependencies();
    }
    private void Start()
    {
     //   interstaialads.enabled = true;
       // view.enabled = true;
    }
    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                    auth = FirebaseAuth.DefaultInstance;
                else
                    AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
            else
            {
                AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    public void SignInWithGoogle() { circle.SetActive(true); OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

   
    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn");

      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
       
               
       
    }

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
        
        auth.SignOut();
    }

    public void OnDisconnect()
    {
        AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
       
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                    info.text = "ERROR:"+error.Message;
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                    info.text = "Excdption"+task.Exception.ToString();
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            AddToInformation("Google ID Token = " + task.Result.IdToken);
            AddToInformation("Email = " + task.Result.Email);
           
            SignInWithGoogleOnFirebase(task.Result.IdToken);
            
        }
    }
    private IEnumerator PostRequestCoroutine(string url, string json,string name)
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
            errormsg.transform.GetChild(0).GetComponent<Text>().text = "Network error!";
            errormsg.SetActive(true);
            StartCoroutine("hidenotification");
        }
        else
        {
            circle.SetActive(false);
            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                                    
                PlayerPrefs.SetInt("Referralsvalue", 0);
                PlayerPrefs.SetString("email", jsonNode["email"]);
                PlayerPrefs.SetString("name", name);
               

                var url1 =AppLoad.BaseUrl+"UserDetails";
                string json1 = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
                StartCoroutine(PostRequestuserCoroutine(url1, json1));

               
            }
            else if (jsonNode["status"] == "2")
            {

                UserbanPanel.SetActive(true);

            }
            else if(jsonNode["status"] == "1")
            {
                                  
                PlayerPrefs.SetInt("Referralsvalue", 1);
                PlayerPrefs.SetString("email", jsonNode["email"]);
                PlayerPrefs.SetString("name", name);
              

                var url1 = AppLoad.BaseUrl+"UserDetails";
                string json1 = "{\"email\":\"" + PlayerPrefs.GetString("email") + "\"}";
                StartCoroutine(PostRequestuserCoroutine(url1, json1));

               
            }
            else
            {
              
                errormsg.transform.GetChild(0).GetComponent<Text>().text = "Something going wrong!";
                errormsg.SetActive(true);
                StartCoroutine("hidenotification");
            }
          //  Debug.Log(www.downloadHandler.text);
        }
          
    }

    
    private IEnumerator PostRequestuserCoroutine(string url, string json)
    {
        circle.SetActive(true);
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();
        circle.SetActive(false);
        if (www.isNetworkError)
        {
            PlayerPrefs.SetInt("Login", 0);
            PlayerPrefs.SetString("email", "");
        }
        else
        {

            JSONNode jsonNode = SimpleJSON.JSON.Parse(www.downloadHandler.text.ToString());
            // Debug.Log(jsonNode["status"]+"and"+ jsonNode["message"]);
            if (jsonNode["status"] == "0")
            {
                PlayerPrefs.SetString("name", jsonNode["Name"]);
              //  PlayerPrefs.SetString("email", jsonNode["email"]);
                PlayerPrefs.SetInt("Login", 1);
                PlayerPrefs.SetString("uniqueid", jsonNode["uniqueid"]);
                PlayerPrefs.SetFloat("Total", jsonNode["total"]);
                PlayerPrefs.SetInt("crown",int.Parse(jsonNode["crown"]));
                
                if (jsonNode["is_lock"]=="Y")
                {
                    PlayerPrefs.SetInt("lockvalue", 0);
                }
                else
                {
                    PlayerPrefs.SetInt("lockvalue", 1);
                }
                MainUI.checkcrown();
                ui.CheckCrown();
                StartCoroutine(MainUI.GetTexture());
            }
          
            else
            {
                PlayerPrefs.SetInt("Login", 0);
                PlayerPrefs.SetString("email", "");
                this.gameObject.SetActive(false);
               

            }
            //  Debug.Log(www.downloadHandler.text);
        }

    }
    IEnumerator hidenotification()
    {
        yield return new WaitForSeconds(2);
        errormsg.SetActive(false);
    }
    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
       
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                info.text = ex.ToString();
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                AddToInformation("Sign In Successful.with firebase");
                string IPadress="Unknown";
                var url = AppLoad.BaseUrl + "signup";
                string pic_url=task.Result.PhotoUrl.ToString();
                string pic_url1 = task.Result.PhoneNumber.ToString();
               // string pic_url3 = task.Result..ToString();
               // url3.text = pic_url+ pic_url1;
                PlayerPrefs.SetString("profileicon", pic_url);

                 string json = "{\"firstname\": \"" + task.Result.DisplayName.ToString() +"\",\"lastname\": \"\",\"pic\":\"" + task.Result.PhotoUrl.ToString() + "\",\"email\":\"" + task.Result.Email.ToString() + "\",\"device_id\":\""+ SystemInfo.deviceUniqueIdentifier + "\",\"model\":\""+ SystemInfo.deviceModel+ "\",\"ip\":\""+ IPadress + "\",\"brand\":\""+ SystemInfo.deviceName+ "\",\"os\":\"android\",\"gcm_id\":\""+ idToken + "\",\"mobile\":\"\",\"token\":\""+ idToken+ "\"}";
                //string json = "{\"firstname\": \"" + task.Result.DisplayName.ToString() + "\",\"lastname\": \"\",\"email\":\"" + task.Result.Email.ToString() + "\",\"device_id\":\"\",\"model\":\"\",\"ip\":\"\",\"brand\":\"\",\"os\":\"android\",\"gcm_id\":\"" + idToken + "\",\"mobile\":\"\",\"token\":\"" + idToken + "\"}";
                StartCoroutine(PostRequestCoroutine(url, json, task.Result.DisplayName.ToString()));

            }
        });
    }

    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void AddToInformation(string str)
    { //infoText.text += "\n" + str; 
    }
    public  string GetIP(ADDRESSFAM Addfam)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }

                    //IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }
}
public enum ADDRESSFAM
{
    IPv4, IPv6
}