window.downloadFileFromStream = async (fileName, byteArray) => {
    const blob = new Blob([byteArray]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

window.clipboardCopy = { copyText: function (text) { navigator.clipboard.writeText(text).then(function () { }).catch(function (error) { return false; }); return true; } };