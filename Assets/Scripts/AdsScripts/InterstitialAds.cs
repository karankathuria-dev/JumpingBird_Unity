using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string androidAdUnitId = "Interstitial_Android";
    [SerializeField] string iosAdUnitId = "Interstitial_iOS";
    private string adUnitId;
    private bool isloaded = false;

    void Awake()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
       
        if (Advertisement.isInitialized && isloaded)
        {
           // PauseGame();
            Advertisement.Show(adUnitId, this);
        }
        else
        {
           // ResumeGame();
            Debug.Log("Ad not ready yet.");
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        isloaded = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading ad {adUnitId}: {error} - {message}");
        ResumeGame();
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad Show Complete: {adUnitId} - {showCompletionState}");
        isloaded = false;
        ResumeGame();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ad Show Failed: {adUnitId} - {error} - {message}");
        ResumeGame();
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log("Ad Started: " + adUnitId);
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("Ad Clicked: " + adUnitId);
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
