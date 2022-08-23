window.clipboardCopy = {
    copyText: function (text) {
        navigator.clipboard.writeText(text).then(function () {
        })
            .catch(function (error) {
                return false;
            });
        return true;
    }
};

function addTooltips(toolTipId) {
    $("#" + toolTipId).tooltip({
        delay: { show: 0, hide: 1200 },
        title: 'برای کپی کردن کلیک کنید'
    });

    $("#" + toolTipId).on('click', function () {
        var tt = bootstrap.Tooltip.getInstance(this);
        tt.dispose();
        this.setAttribute('title', 'کپی شد')
        tt = bootstrap.Tooltip.getOrCreateInstance(this);
        tt.show();
    });
}