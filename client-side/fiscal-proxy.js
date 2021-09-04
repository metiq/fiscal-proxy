var fiscalProxy = (function () {
    var chromeExtensionId = null;

    function init(extensionId) {
        chromeExtensionId = extensionId;
    }

    function print(data, callback) {
        var port = chrome.runtime.connect(chromeExtensionId);

        port.onMessage.addListener(function (response) {
            port.disconnect();

            callback(response);
        });

        port.postMessage(data);
    }

    return {
        init: init,
        print: print
    };
})();