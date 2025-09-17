using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string androidAdUnitId = "Rewarded_Android";
    [SerializeField] string iosAdUnitId = "Rewarded_iOS";
    private string adUnitId;
    [SerializeField] bool testMode = true;
    bool isloaded = false;

    void Start()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif

        //Advertisement.Initialize("YOUR_GAME_ID_HERE", testMode); // replace with your actual game ID
      //  LoadAd();
    }

    // Load the ad
    public void LoadAd()
    {
        Advertisement.Load(adUnitId, this);
    }

    // Show the ad
    public void ShowAd()
    {
        if (Advertisement.isInitialized && isloaded)
        {
            PauseGame();
            Advertisement.Show(adUnitId, this);
        }
        else
        {
            ResumeGame();
            Debug.Log("Ad not ready yet.");
        }
    }

    // Called when ad is successfully loaded
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"Ad Loaded: {adUnitId}");
        isloaded = true;
      //  ShowAd();
    }

    // Called when ad fails to load
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading ad {adUnitId}: {error.ToString()} - {message}");
        isloaded = false;
    }

    // Called when ad fails to show
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing ad {adUnitId}: {error.ToString()} - {message}");
        isloaded = false;
        ResumeGame();
    }

    // Called when ad starts showing
    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"Ad Started: {adUnitId}");
    }

    // Called when ad is clicked
    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log($"Ad Clicked: {adUnitId}");
    }

    // Called when ad finishes showing
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(this.adUnitId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Reward the player!");
            isloaded = false;
            ResumeGame();
            RewardPlayer();
        }
    }

    private void RewardPlayer()
    {
        // Example: Increase score, give life, etc.
        ResumeGame();
        Debug.Log("Player rewarded!");
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
