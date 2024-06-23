using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    public InitializeAds initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    private int roundsPlayed = 0;
    private bool shouldReset = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        interstitialAds.LoadInterstitialAd();
        bannerAds.LoadBannderAd();
        rewardedAds.LoadRewardedAd();
    }

    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                StartCoroutine(DisplayBannerWithDelay());

                if (shouldReset)
                {
                    if (roundsPlayed % 3 == 0)
                    {
                        interstitialAds.ShowInterstitialAd();
                    }
                    shouldReset = false;
                }
                break;

            case GameState.LevelComplete:
                bannerAds.HideBannerAd();
                roundsPlayed++;
                shouldReset = true;
                break;

            case GameState.Gameover:
                bannerAds.HideBannerAd();
                roundsPlayed++;
                shouldReset = true;
                break;

            case GameState.Menu:
                bannerAds.HideBannerAd();
                break;
        }
    }

    private IEnumerator DisplayBannerWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        bannerAds.ShowBannerAd();
    }

}
