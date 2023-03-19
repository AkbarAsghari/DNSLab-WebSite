window.CKEditorInterop = (() => {
    const editors = {};

    return {
        init(id, dotNetReference) {
            DecoupledEditor
                .create(document.querySelector('#ckeditor'), {
                    language: 'fa',
                    ckfinder: { uploadUrl: 'https://api.dnslab.link/Files/Upload' }
                })
                .then(editor => {
                    const toolbarContainer = document.querySelector('#toolbar-container');

                    toolbarContainer.appendChild(editor.ui.view.toolbar.element);
                    editors[id] = editor;
                    editor.model.document.on('change:data', () => {
                        let data = editor.getData();

                        const el = document.createElement('div');
                        el.innerHTML = data;
                        if (el.innerText.trim() == '')
                            data = null;

                        dotNetReference.invokeMethodAsync('EditorDataChanged', data);
                    });
                })
                .catch(error => console.error(error));
        },
        destroy(id) {
            editors[id].destroy()
                .then(() => delete editors[id])
                .catch(error => console.log(error));
        }
    };
})();

