using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public InterstitialAds interstitialAds;
    public RewardedAdsManager rewardedAdsManager;
    public void GotoGame()
    {
        interstitialAds.ShowAd();
        //rewardedAdsManager.ShowAd();
        SceneManager.LoadScene(SceneData._GameView);
    }
}
