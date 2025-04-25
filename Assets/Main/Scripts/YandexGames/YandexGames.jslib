mergeInto(LibraryManager.library, {
    YandexGames_GetLang: function () {
        var value = yandexGames.getLang()
        var bufferSize = lengthBytesUTF8(value) + 1
        var buffer = _malloc(bufferSize)
        stringToUTF8(value, buffer, bufferSize)
        return buffer
    },
  
    YandexGames_SetLeaderboardScore: function (score) {
        yandexGames.setLeaderboardScore(score)
    },
});