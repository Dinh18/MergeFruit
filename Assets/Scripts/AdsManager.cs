using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager instance;

    [Header("Config")]
    [SerializeField] string _androidGameId = "6007469";     [SerializeField] string _iosGameId = "6007468";
    [SerializeField] bool _testMode = true;

    string _interstitialId = "Interstitial_Android"; // Quảng cáo bung toàn màn hình
    string _rewardedId = "Rewarded_Android";         // Quảng cáo xem để nhận thưởng

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeAds()
    {
        string gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosGameId : _androidGameId;
        Advertisement.Initialize(gameId, _testMode, this);
    }


    // 1. Gọi quảng cáo chuyển cảnh (Interstitial) - Dùng khi GameOver hoặc qua màn
    public void ShowInterstitial()
    {
        Advertisement.Load(_interstitialId, this);
    }

    // 2. Gọi quảng cáo thưởng (Rewarded) - Dùng khi hồi sinh hoặc x2 vàng
    public void ShowRewarded()
    {
        Advertisement.Load(_rewardedId, this);
    }

    public void OnInitializationComplete() 
    {
        Debug.Log("Unity Ads setup thành công!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) 
    {
        Debug.Log($"Lỗi Setup: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Lỗi Load Ad: {message}");
    }


    public void OnUnityAdsShowStart(string adUnitId) 
    {
        if(SoundManager.instance != null) 
            SoundManager.instance.StopBackGroudSound();
        
        // Pause game lại
        Time.timeScale = 0f;
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Time.timeScale = 1f;
        if(SoundManager.instance != null) 
            SoundManager.instance.PlayBackgroundSound();

        if (adUnitId.Equals(_rewardedId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Người chơi đã xem hết, trao thưởng thôi!");
        }
    }

    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }
}