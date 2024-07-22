function isFaceDetectionModelLoaded() {
    return !!faceapi.nets.ssdMobilenetv1.params
}

function getFaceDetectorOptions() {
    // ssd_mobilenetv1 options
    let minConfidence = 0.5
    return new faceapi.SsdMobilenetv1Options({ minConfidence });
}

function getFaceImageUri(className, idx) {
    return `face_datasource/${className}/${className}${idx}.png`
}

async function loadImageFromUrl(url) {
    $('#inputImg').get(0).src = url
}

async function initSelectAsync() {
    const selectFaceType = $('#selectFaceType');

    const optgroupData = [
        { label: '宅男行不行', prefix: 'bbt', count: 5 },
        //{ label: 'Amber', prefix: 'a', count: 10 },
        //{ label: 'Joyce Amber', prefix: 'ja', count: 6 },
        //{ label: 'Lawrence', prefix: 'l', count: 5 },
        //{ label: 'Lawrence Amber', prefix: 'la', count: 6 },
        //{ label: 'Lawrence Joyce', prefix: 'lj', count: 4 },
        //{ label: 'Lawrence Joyce Amber', prefix: 'lja', count: 10 }
    ];

    optgroupData.forEach(group => {
        let optgroup = document.createElement('optgroup');
        optgroup.label = group.label;
        for (let i = 1; i <= group.count; i++) {
            $(optgroup).append(`<option value="${group.prefix}${i}">${group.prefix}${i}</option>`);
        }
        selectFaceType.append(optgroup);
    });

    await loadImageFromUrl(`images/${selectFaceType.val()}.jpg`)
}