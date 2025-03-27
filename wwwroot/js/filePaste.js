export function initializeFilePaste(fileDropContainer, inputFile) {

    fileDropContainer.addEventListener('paste', onPaste);



    function onPaste(event) {
        event.preventDefault();
        inputFile.files = event.clipboardData.files;
        const changeEvent = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(changeEvent);
    }
    function preventDefault(event) {
        event.preventDefault();
        event.stopPropagation();
    }

    return {
        dispose: () => {
            fileDropContainer.removeEventListener('paste', onPaste);
 
        }
    }

}