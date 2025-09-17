using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdsManager Instance;
    public InterstitialAds interstitialAds;
    public BannerAdsManager bannerAds;
    public RewardedAdsManager rewardedAds;

    [Header("Ad IDs")]
    [SerializeField] string androidGameId = "YOUR_ANDROID_GAME_ID";
    [SerializeField] string iosGameId = "YOUR_IOS_GAME_ID";
    [SerializeField] bool testMode = true;

    private string gameId;

    private void Awake()
    {
        // Singleton pattern: keep only one AdsManager across scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        gameId = iosGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#else
        gameId = null;
#endif

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialized.");
        bannerAds.LoadBanner();
        interstitialAds.LoadAd();
        rewardedAds.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }
}
