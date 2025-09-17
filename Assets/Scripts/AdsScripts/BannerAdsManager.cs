using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdsManager : MonoBehaviour
{
    [SerializeField] string androidAdUnitId = "Banner_Android";
    [SerializeField] string iosAdUnitId = "Banner_iOS";
    private string adUnitId;
    [SerializeField] BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] bool testMode = true;

    void Start()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif

      //  Advertisement.Initialize("YOUR_GAME_ID_HERE", testMode); // replace with your actual game ID
        Advertisement.Banner.SetPosition(bannerPosition);

      //  LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(adUnitId, options);
    }

    public void ShowBanner()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(adUnitId, options);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded successfully");
        ShowBanner();
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner failed to load: {message}");
    }

    void OnBannerClicked()
    {
        Debug.Log("Banner clicked");
    }

    void OnBannerShown()
    {
        Debug.Log("Banner shown");
    }

    void OnBannerHidden()
    {
        Debug.Log("Banner hidden");
    }
}
