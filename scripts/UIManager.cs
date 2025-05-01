using System;
using TMPro;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
public class UIManager : MonoBehaviour
{

    private List<TextMeshProUGUI> gui = new List<TextMeshProUGUI>();
    public List<Translator> _language = new List<Translator>();

    void Start()
    {
        YandexBridge.Instance.autoLoginIfPossible(setProfile);
        setTextMeshProGUI(PointTextMeshPro,pointsUser.ToString());
        gui = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>()
            .Where(t =>
                t.hideFlags == HideFlags.None
                && t.gameObject.scene.IsValid()
                && !t.gameObject.name.StartsWith("!")
            )
            .ToList();
        
    }
    
    [Header("Данные пользователя")]
    public TextMeshProUGUI userName;
    private Profile Profile;
    public TextMeshProUGUI PointTextMeshPro;
    public int pointsUser;
    public string KeySAVE;
    public TextMeshProUGUI LangTextMeshPro;
    
    
    public void getProfile() => YandexBridge.Instance.getProfile(setProfile);

    private void setProfile(Profile Profile)
    {
        this.Profile = Profile;
        setTextMeshProGUI(userName,this.Profile.name);
        setTextMeshProGUI(LangTextMeshPro,this.Profile.lang);
        reloadText();
    }
    private void reloadText()
    {
        if(Profile.lang == "ru")
        {
            foreach(TextMeshProUGUI textMeshPro in gui)
            {
                textMeshPro.text = _language.Find(x=>x.ru.ToLower() == textMeshPro.text.ToLower()).ru;
            }
        }else
        {
            foreach(TextMeshProUGUI textMeshPro in gui)
            {
                textMeshPro.text = _language.Find(x=>x.ru.ToLower() == textMeshPro.text.ToLower()).en;
            }
        }
    }
    public void saveData()
    {
        YandexBridge.Instance.savePlayerData(KeySAVE,pointsUser.ToString());
    }
    public void loadData()
    {
        YandexBridge.Instance.loadPlayerData(KeySAVE,dataLoaded);
    }
    private void dataLoaded(String text)
    {
        JSONNode node = JSON.Parse(text);
        string _points = node["_data"];
        if (int.TryParse(_points, out int number))
        {
            pointsUser = number;
            setTextMeshProGUI(PointTextMeshPro,number.ToString());
        }
    }
    
    
    [Header("Данные LeaderBoard")]
    public string leaderBoardName;
    public int top;
    public int around;
    
    
    public void getLeaderBoard() => YandexBridge.Instance.getLeaderBoard(leaderBoardName,top,around,setLeaderBoard);
    private void setLeaderBoard(LeaderboardData data)
    {
        foreach(LeaderboardEntry leader in data.entries)
        {
            Debug.Log(leader.playerPublicName + " | " + leader.score + " | " + leader.playerAvatarUrl);
        }
    }
    public void setPlayerLeaderBoardPoint()
    {
        YandexBridge.Instance.setPlayerScoreIntoLeaderBoard(leaderBoardName,pointsUser);
    }
    
    

    
    [Header("Флаги")]
    public string nameFlag;
    public string flag;
    public void getFlags()
    {
        YandexBridge.Instance.getFlags(nameFlag,setFlags);
    }
    private void setFlags(string data)
    {
        flag = data;
        Debug.Log(flag);
    }
    
    // РЕКЛАМА
    public void showADV()
    {
        YandexBridge.Instance.showADV();
    }
    public void showRewardADV()
    {
        YandexBridge.Instance.showRewardedADV(setReward);
    }
    private void setReward()
    {
        pointsUser++ ;
        setTextMeshProGUI(PointTextMeshPro,pointsUser.ToString());
    }
    
    private void setTextMeshProGUI(TextMeshProUGUI textMeshPro, string text)
    {
        textMeshPro.text = text;
    }
    
}

[Serializable]
public class Translator
{
    public string ru;
    public string en;
}