using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowInterstitial : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // android ID: 5818288
    // interstitial ID: Interstitial_Android

    public string GAME_ID = "5818288";
    public string INTERSTITIAL_ID = "Interstitial_Android";
    public string REWARDED_ID = "Rewarded_Android";

    private void Start()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(GAME_ID, true, this);
        }
    }

    /* UI */

    public void ShowInterstitialAd() { Advertisement.Show(INTERSTITIAL_ID, this); }
    public void ShowRewardedAd() { Advertisement.Show(REWARDED_ID, this); }

    /* IUnityAdsInitializationListener */

    public void OnInitializationComplete()
    {
        Advertisement.Load(INTERSTITIAL_ID, this);
        Advertisement.Load(REWARDED_ID, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }

    /* IUnityAdsLoadListener */

    public void OnUnityAdsAdLoaded(string placementId) { }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    /* IUnityAdsShowListener */

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Advertisement.Load(placementId, this);

        if (placementId == REWARDED_ID)
        {
            GameManager.Instance.UnlockDictionary();
        }
    }
}
