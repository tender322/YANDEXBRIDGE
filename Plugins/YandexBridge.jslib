





mergeInto(LibraryManager.library, {
    GetProfile:function(){ 
        getProfile();
    },
    AutoLoginIfPossible:function(){
        autoLoginIfPossible();
    },

    GetGameFlags:function(Flag){
        var _Flag = UTF8ToString(Flag);
        getGameFlag(_Flag);
    },

    showCommonADV:function(){
        showCommonADV();
    },

    showRewardADV:function(){
        showRewardADV();
    },

    GetLeaderBoard:function(leaderBoard,top,around){
        var _leaderboard = UTF8ToString(leaderBoard);
        getLeaderBoard(_leaderboard,top,around);
    },

    SetLeaderBoardPoint:function(leaderBoard,score){
        var _leaderboard = UTF8ToString(leaderBoard);
        setLeaderBoard(_leaderboard,score);
    },

    StartLevel:function(){
        startLevel();
    },

    StopLevel:function(){
        stopLevel();
    },
    SavePlayerData:function(key,data){
        var _key = UTF8ToString(key);
        var _data = UTF8ToString(data);
        Save(_key,_data);
    },
    LoadPlayerData:function(key){
        var _key = UTF8ToString(key);
        Load(_key);
    }

});