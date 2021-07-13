const sleep = ms => new Promise(resolve => setTimeout(resolve, ms))

// Modified from https://github.com/wmcmurray/just-detect-adblock
// Detects: uBlock, Adblock, Adblock Plus, AdBlocker Ultimate, Ghostery,
export async function isAdblockDetected() {
    let detected = false

    // create the bait
    const bait = document.createElement('div')
    bait.setAttribute('class', `pub_300x250 pub_300x250m pub_728x90 text-ad
        textAd text_ad text_ads text-ads text-ad-links`)
    bait.setAttribute('style', `width: 1px ! important; height: 1px !important;
        position: absolute !important; left: -10000px !important;
        top: -1000px !important;`)
    document.body.appendChild(bait)

    // Give time for extensions to block.
    await sleep(100)

    // Check if the bait has been affected by an adblocker.
    if (window.document.body.getAttribute('abp') !== null ||
        bait.offsetParent === null ||
        bait.offsetHeight === 0 ||
        bait.offsetLeft === 0 ||
        bait.offsetTop === 0 ||
        bait.offsetWidth === 0 ||
        bait.clientHeight === 0 ||
        bait.clientWidth === 0) {
        detected = true
    } else if (window.getComputedStyle !== undefined) {
        const baitTemp = window.getComputedStyle(bait, null)
        if (baitTemp && (baitTemp.getPropertyValue('display') === 'none' ||
            baitTemp.getPropertyValue('visibility') === 'hidden')) {
            detected = true
        }
    }

    // Destroy the bait.
    document.body.removeChild(bait)

    return detected
}