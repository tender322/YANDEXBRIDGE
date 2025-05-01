using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexBridge : MonoBehaviour
{
    public static YandexBridge Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    


    // Профиль игрока 
    public event Action<Profile> _ProfileCallback;
    
    public event Action<string> _ProfileSaveLoadedCallback;
    [DllImport("__Internal")]
    private static extern void GetProfile();
    [DllImport("__Internal")]
    private static extern void SavePlayerData(string key, string data);
    [DllImport("__Internal")]
    private static extern void LoadPlayerData(string key);
    [DllImport("__Internal")]
    private static extern void AutoLoginIfPossible();
    
    
    public void getProfile(Action<Profile> callback)
    {
        _ProfileCallback = callback;
        GetProfile();
    }
    
    public void autoLoginIfPossible(Action<Profile> callback)
    {
        _ProfileCallback = callback;
        AutoLoginIfPossible();
    }
    
    public void setProfileFromYandex(string json)
    {
        var profile = JsonUtility.FromJson<Profile>(json);
        _ProfileCallback?.Invoke(profile);
    }
    
    public void savePlayerData(string key, string data)
    {
        SavePlayerData(key,data);
    }
    public void loadPlayerData(string key,Action<string> callback)
    {
        _ProfileSaveLoadedCallback = callback;
        LoadPlayerData(key);
    }
    public void loadPlayerDataFromYandex(string json){
        _ProfileSaveLoadedCallback?.Invoke(json);
    }
    
    
    // Лидер борды
    public event Action<LeaderboardData> _LeaderBoardCallback;
    [DllImport("__Internal")]
    private static extern void GetLeaderBoard(string leaderBoard,int top, int around);
    
    [DllImport("__Internal")]
    private static extern void SetLeaderBoardPoint(string leaderBoard,int score);
    
    public void getLeaderBoard(string leaderBoard,int top, int around,Action<LeaderboardData> callback)
    {
        _LeaderBoardCallback = callback;
        GetLeaderBoard(leaderBoard,top,around);
    }
    
    
    public void setPlayerScoreIntoLeaderBoard(string leaderBoard, int score)
    {
        SetLeaderBoardPoint(leaderBoard,score);
    }
    
    
    
    public void setLeaderBoardFromYandex(string json)
    {
        LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
        foreach (var entry in data.entries)
        {
            Debug.Log($"Имя: {entry.playerPublicName}, Очки: {entry.score}, Аватар: {entry.playerAvatarUrl}");
        }
        _LeaderBoardCallback?.Invoke(data);
    }
   
    //Реклама
    public event Action _RewardedCallback;
    [DllImport("__Internal")]
    private static extern void showCommonADV();
    [DllImport("__Internal")]
    private static extern void showRewardADV();

    public void showADV()
    {
        showCommonADV();
    }
    
    public void showRewardedADV(Action Callback)
    {
        _RewardedCallback = Callback;
        showRewardADV();
    }
    public void onRewarded()
    {
        _RewardedCallback?.Invoke();
    }
    
    
    // Флаги
    public event Action<string> _FlagCallback;
    [DllImport("__Internal")]
    private static extern void  GetGameFlags(string flag);
    public void getFlags(string flagName,Action<string> Callback)
    {
        _FlagCallback = Callback;
        GetGameFlags(flagName);
    }
    public void setFlags(string flags)
    {
        _FlagCallback?.Invoke(flags);
    }
   
    public void resumeGame()
    {
        Time.timeScale = 1;
    }
    public void stopGame()
    {
        Time.timeScale = 0;
    }
    
    
}
[Serializable]
public class Profile
{
    public string name;
    public string id;
    public string lang;
}
[Serializable]
public class LeaderboardEntry
{
    public string playerPublicName;
    public int score;
    public string playerAvatarUrl;
}

[Serializable]
public class LeaderboardData
{
    public LeaderboardEntry[] entries;
}