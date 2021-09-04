var hostName = 'io.metiq.fiscalproxy';

chrome.runtime.onConnectExternal.addListener(function (port) {
    port.onMessage.addListener(function (message) {

        chrome.storage.sync.get(['serialPort', 'baudRate'], function (items) {

            var serialPort = items.serialPort,
                baudRate = items.baudRate;

            if (!serialPort || !baudRate) {
                port.postMessage({ error: 'Printer port and rate are not set, please set them in the extension\'s options.' });
                return;
            }

            var data = {
                serialPort: serialPort,
                baudRate: baudRate,
                message: message
            };

            chrome.runtime.sendNativeMessage(hostName, data, function (response) {
                if (chrome.runtime.lastError) {
                    port.postMessage({ error: chrome.runtime.lastError.message });
                    return;
                }

                port.postMessage(response);
            });

        });

    });
});