mergeInto(LibraryManager.library, {
    Js_GetLang: function () {
        var value = yandexGames.getLang()
        return stringToBuffer(value)
    },
  
    Js_SetLeaderboardScore: function (score) {
        yandexGames.setLeaderboardScore(score)
    },
    
    Js_OnGameReady: function () {
        yandexGames.onGameReady()
    },
    
    Js_TryShowAd: function () {
        yandexGames.tryShowAd()
    },
    
    Js_TryShowRewardedAd: function () {
        yandexGames.tryShowRewardedAd()
    },

    Js_TryRequestFullscreen: function () {
        yandexGames.tryRequestFullscreen()
    },

    Js_ShouldRequestFullscreen: function () {
        return yandexGames.shouldRequestFullscreen() ? 1 : 0
    },
    
    Js_GetTime: function () {
        return yandexGames.getTime()
    },
    
    Js_SavePlayerData: function (data) {
        var value = UTF8ToString(data)
        yandexGames.savePlayerData(value)
    },

    Js_GetPlayerData: function () {
        var value = yandexGames.getPlayerData()
        return stringToBuffer(value)
    },

    Js_GetPlayerId: function () {
        var value = yandexGames.getPlayerId()
        return stringToBuffer(value)
    },
});