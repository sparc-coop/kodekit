function copyToClipboard(id) {
    var copyText = document.getElementById(id).value;
    navigator.clipboard.writeText(copyText);
}

function showHighlight() {
    hljs.highlightAll();
}

function populatePreviewCode(previewBlock, codeBlock) {
    var htmlWithBetterLineBreaks = previewBlock.innerHTML.replace(/>([^\r\n])/g, function (match, $1) { return '>\r\n' + $1 })
        .replace(/([^\s])</g, function (match, $1) { return $1 + '\r\n<' })
        .replace(/\r\n\s*\r\n/g, '\r\n');
    var encodedHtml = html_beautify(htmlWithBetterLineBreaks, { indent_size: 2 })
        .replace(/<!--!-->/g, '') // get rid of blazor debug comments
        .replace(/[\u00A0-\u9999<>\&]/g, function (i) { // switch to html entities
            return '&#' + i.charCodeAt(0) + ';';
        });
    codeBlock.innerHTML = encodedHtml;
    hljs.highlightElement(codeBlock);
}

function initHyperScript() {
    _hyperscript.processNode(document.body);
}

// Cursor
function initCursor() {
    document.body.style.cursor = "none";

    var cursor = document.getElementById('cursor');
    var cursorFollow = document.getElementById('cursor-follow');
    var cursorText = document.getElementById('cursor-text');
    var attachToCursor = document.getElementsByClassName('attach-to-cursor');

    // cursor/follow changes shape based on element hovered
    // follow disappears when hovering over the nav
    function move(event) {
        var e = event;
        var t = e.target;
        var mouseX = e.clientX;
        var mouseY = e.clientY;

        cursor.style.transform = `translate3d(${mouseX}px, ${mouseY}px, 0)`;
        for (var i = 0; i < attachToCursor.length; i++) {
                attachToCursor[i].style.transform = `translate3d(${mouseX}px, ${mouseY}px, 0)`;
        }

        if (t == null)
            return;

        var shouldHide = t.closest('hide-cursor-follow') || t.closest('button, a, input');
        if (cursorFollow) {
            cursorFollow.style.opacity = shouldHide ? "0" : "1";
        }

        if (cursorText) {
            cursorText.style.opacity = shouldHide ? "0" : "1";
        }
    }

    if (cursor) {
        window.addEventListener("mousemove", ev => requestAnimationFrame(() => move(ev)));
    }
}