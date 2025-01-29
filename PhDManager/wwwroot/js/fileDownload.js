function saveAsFile(fileName, fileBytes, contentType) {
    const file = new Blob([fileBytes], { type: contentType });
    if (window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveOrOpenBlob(file, filename);
    } else {
        const link = document.createElement('a');
        const url = URL.createObjectURL(file);
        link.href = url;
        link.download = fileName;
        document.body.appendChild(link);
        link.click();
        setTimeout(function () {
            document.body.removeChild(link);
            window.URL.revokeObjectURL(url);
        }, 0);
    }
}