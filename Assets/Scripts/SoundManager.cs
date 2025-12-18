using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip background;
    public AudioClip effectMerge;
    public bool isPlayBackgoundSound = true;
    public bool isPlayMergeSound = true;
    void Awake()
    {
        if (instance == null)
        {
            // Nếu chưa có quản lý âm thanh nào, thì đây là cái chính chủ
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Nếu đã có một cái SoundManager đang sống từ Scene trước rồi
            // Thì tiêu diệt ngay cái mới này đi để tránh trùng lặp
            Destroy(gameObject); 
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBackgroundSound();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBackgroundSound()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.playOnAwake = true;
            musicSource.spatialBlend = 0;

            musicSource.Play();
        }
        
        isPlayBackgoundSound = true;

    }
    public void PlayMergeSound()
    {
        if(isPlayMergeSound) sfxSource.PlayOneShot(effectMerge);
    }
    public void StopBackGroudSound()
    {
        musicSource.Stop();
        isPlayBackgoundSound = false;
    }
    
}
