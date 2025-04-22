using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowInterstitial : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
{
    // android ID: 5818288
    // interstitial ID: Interstitial_Android

    public string GAME_ID = "5818288";
    public string INTERSTITIAL_ID = "Interstitial_Android";

    private void Start()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(GAME_ID, Debug.isDebugBuild, this);
        }
    }


    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }



    public void OnInitializationComplete()
    {
        // assim que completa o start dos ads, vamos carregar um
        Debug.Log("OnInitializationComplete");

        Advertisement.Load(INTERSTITIAL_ID, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(INTERSTITIAL_ID, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Advertisement.Load(placementId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
