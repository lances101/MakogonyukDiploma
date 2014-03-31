var LastXPath = '';
var LastClickedElement;

document.oncontextmenu = function () {
    LastClickedElement = window.event.srcElement;
    var value = getXPath(LastClickedElement);
    LastXPath = value;
    jsobject.callNETReturnXpath(LastXPath);
};

function getXPathParent() {
    LastClickedElement = LastClickedElement.parentElement;
    var value = getXPath(LastClickedElement);
    LastXPath = value;
    jsobject.callNETReturnXpath(LastXPath);
}
function getXPath(element) {
    
    var currentNode = element;
    var path = [];
    while (currentNode) {
        var pe = getNode(currentNode);
        if (pe) {
            path.push(pe);
            if (pe.indexOf('@id') != -1 || pe.indexOf('@class') != -1) break;
        }
        currentNode = currentNode.parentElement;
    }
    var xpath = "/" + path.reverse().join('/');
    return xpath;
}
function getNode(node) {
    var nodeTag = node.tagName;
    if (!nodeTag) return null;
    if (node.id != '') {
        nodeTag += "[@id='" + node.id + "']";
        return "/" + nodeTag;
    }
    if (node.className != '') {
        nodeTag += "[@class='" + node.className + "']";
        return "/" + nodeTag;
    }
    var rank = 0;
    var ps = node.previousElementSibling;
    while (ps) {
        if (ps.tagName == node.tagName)
            rank++;
        ps = ps.previousElementSibling;
    }
    if (rank > 1) {
        nodeTag += '[' + rank + ']';
    }
    else {
        var count = 0;
        var ns = node.nextElementSibling;
        while (ns) {

            count++;
            if (ns.tagName == node.tagName) {
                nodeTag += "[" + getChildIndex(ns) + "]";
                break;
            }
            ns = ns.nextElementSibling;

        }
    }
    return nodeTag;
}
function getChildIndex(node) {
    var count = -1;
    var parent = node.parentElement;
    var child = parent.childNodes[0];
    if (!child) alert("HOLY FUCKING COWS, HOW IS THIS POSSIBLE");
    while (child) {
        if (child.tagName != node.tagName) {
            child = child.nextElementSibling;
            continue;
        }
        count++;
        if (node == child)
            return count;
        child = child.nextElementSibling;
    }
    return count;

}