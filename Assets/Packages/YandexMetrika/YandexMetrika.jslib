mergeInto(LibraryManager.library, {
    YandexMetrika_Send: function (goal) {
        var value = UTF8ToString(goal)
        yandexMetrika.send(value)
    },
});