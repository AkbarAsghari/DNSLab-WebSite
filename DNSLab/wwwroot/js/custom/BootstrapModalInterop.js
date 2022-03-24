window.BootstrapModal = {};
window.BootstrapModal.OpenedModal = null;

window.BootstrapModal.ShowModal = (id) => {
    if (window.BootstrapModal.OpenedModal != null) {
        window.BootstrapModal.HideModal();
    }

    window.BootstrapModal.OpenedModal = new bootstrap.Modal(document.getElementById(id));
    window.BootstrapModal.OpenedModal.show();
}

window.BootstrapModal.HideModal = () => {
    if (window.BootstrapModal.OpenedModal == null) {
        return;
    }

    window.BootstrapModal.OpenedModal.hide();
    window.BootstrapModal.OpenedModal = null;
}