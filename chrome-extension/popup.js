var serialPort, baudRate;

function load_options() {
    chrome.storage.sync.get(['serialPort', 'baudRate'], function (items) {

        serialPort = items.serialPort;
        baudRate = items.baudRate;

        document.getElementById('serialPort').innerText = serialPort || '-- not set ---';
        document.getElementById('baudRate').innerText = baudRate || '-- not set ---';
    });
}

document.addEventListener('DOMContentLoaded', load_options);
document.getElementById('btnOpenOptions').addEventListener('click', function () {
    chrome.runtime.openOptionsPage();
});