
function ClickElement(xpath) {
    var elem = document.evaluate(xpath, document, null, XPathResult.ANY_TYPE, null);
    if (!elem) {
        lastElementClick = false;
        return;
    } else {
        lastElementClick = true;
        elem.click();
    }
}

function InjectValue(xpath, val) {
    var elem = document.evaluate(xpath, document, null, XPathResult.ANY_TYPE, null);
    if (!elem) {
        return false;
    } else {
        elem.value = val;
        return true;
    }
}