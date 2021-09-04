function save_options() {
    chrome.storage.sync.set({
        serialPort: document.getElementById('serialPort').value,
        baudRate: document.getElementById('baudRate').value
    }, function () {
        // Update status to let user know options were saved.
        var status = document.getElementById('status');
        status.textContent = 'Options saved.';
        setTimeout(function () {
            status.textContent = '';
        }, 750);
    });
}

function restore_options() {
    chrome.storage.sync.get(['serialPort', 'baudRate'], function (items) {
        document.getElementById('serialPort').value = items.serialPort || '';
        document.getElementById('baudRate').value = items.baudRate || '';
    });
}

document.addEventListener('DOMContentLoaded', restore_options);
document.getElementById('save').addEventListener('click', save_options);