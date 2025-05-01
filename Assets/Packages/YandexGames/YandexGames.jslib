mergeInto(LibraryManager.library, {
    YandexGames_GetLang: function () {
        var value = yandexGames.getLang()
        return stringToBuffer(value)
    },
  
    YandexGames_SetLeaderboardScore: function (score) {
        yandexGames.setLeaderboardScore(score)
    },
    
    YandexGames_OnGameReady: function () {
        yandexGames.onGameReady()
    },
    
    YandexGames_TryShowAd: function () {
        yandexGames.tryShowAd()
    },
    
    YandexGames_TryShowRewardedAd: function () {
        yandexGames.tryShowRewardedAd()
    },

    YandexGames_TryRequestFullscreen: function () {
        yandexGames.tryRequestFullscreen()
    },

    YandexGames_ShouldRequestFullscreen: function () {
        return yandexGames.shouldRequestFullscreen() ? 1 : 0
    },
    
    YandexGames_GetTime: function () {
        return yandexGames.getTime()
    },
    
    YandexGames_SavePlayerData: function (data) {
        var value = UTF8ToString(data)
        yandexGames.savePlayerData(value)
    },

    YandexGames_GetPlayerData: function () {
        var value = yandexGames.getPlayerData()
        return stringToBuffer(value)
    },

    YandexGames_GetPlayerId: function () {
        var value = yandexGames.getPlayerId()
        return stringToBuffer(value)
    },
});