// create cursor elements

document.body.style.cursor = "none";

var cursor = document.createElement("div");
cursor.classList.add("cursor");
document.body.appendChild(cursor);

var follow = document.createElement("div");
follow.classList.add("follow");
document.body.appendChild(follow);

var start = document.createElement("div");
start.classList.add("cursor-start");
var startImg = document.createElement("img");
startImg.setAttribute("src", "blobs/cursor-text.svg");
startImg.classList.add("cursor-text");
start.appendChild(startImg);
document.body.appendChild(start);

var shadow = document.createElement("img");
shadow.classList.add("cursor-shadow");
shadow.setAttribute("src", "blobs/blob-black.svg");
document.body.appendChild(shadow);

const cursors = [cursor, follow, start, shadow];

// cursor/follow changes shape based on element hovered
// follow disappears when hovering over the nav

//var isMouseMoving = false;

function move(event) {
    var e = event;
    var t = e.target;
    var mouseX = e.clientX;
    var mouseY = e.clientY;
    var c = cursor;
    var f = follow;
    var st = start;
    var cs = shadow;

    //e.preventDefault();
    //isMouseMoving = true;

    if (t == null || t.parentElement == null) {
        return;
    }

    for (var i = 0; i < cursors.length; i++) {
        //var position = cursors[i].offset();
        cursors[i].style = "";
        //cursors[i].style.top = mouseY + "px";
        //cursors[i].style.left = mouseX + "px";
        //cursors[i].style.transform = "translate(" + mouseX + "px, " + mouseY + "px" + ")";
        //cursors[i].css({
        //    transform: 'translateX(' + mouseX + 'px) translateY(' + mouseY + 'px)'
        //});
        //cursors[i].css({
            //transform: 'translateX(' + (mouseX - cursors[i].offsetLeft) + 'px) translateY(' + (mouseY - cursors[i].offsetTop) + 'px)'
        //});

        //cursors[i].style.transform = 'translateX(' + mouseX + 'px) translateY(' + mouseY + 'px)'
        cursors[i].style.transform = 'translateX(' + (mouseX - cursors[i].offsetLeft) + 'px) translateY(' + (mouseY - cursors[i].offsetTop) + 'px)';

        //if (cursors[i].classList.contains("cursor")) {
        //    cursors[i].style.transform = 'translateX(' + (mouseX - (cursors[i].offsetLeft)) + 'px) translateY(' + (mouseY - (cursors[i].offsetTop)) + 'px)';
        //} else {
        //    cursors[i].style.transform = 'translateX(' + (mouseX - (cursors[i].offsetLeft - (cursors[i].width / 2))) + 'px) translateY(' + (mouseY - (cursors[i].offsetTop - (cursors[i].height / 2))) + 'px)';
        //}
    }

    requestAnimationFrame(move);

    //e.preventDefault();
    //isMouseMoving = true;

    //var e = event;
    //var t = e.target;
    //var c = cursor;
    //var f = follow;
    //var st = start;
    //var cs = shadow;

    //move(c, e);
    //move(f, e);
    //move(st, e);
    //move(cs, e);
    //f.classList.remove("on-focus");
    //st.classList.remove("on-focus");
    //cs.classList.remove("on-focus");

    //if (isMouseMoving) {
        //requestAnimationFrame(move(cursors, e));
        //move(cursors, e);
    //}

    //if (t == null || t.parentElement == null)
    //    return;

    //if (t.tagName == "BUTTON" || t.tagName == "A") {
    //    c.style.backgroundColor = "transparent";

    //    f.style.top = t.offsetTop + "px";
    //    f.style.left = t.offsetLeft + "px";
    //    f.style.width = t.clientWidth + "px";
    //    f.style.height = t.clientHeight + "px";
    //    f.style.transform = "translate(" + t.offsetTop + "px" + t.offsetLeft + "px" + ")";
    //    f.classList.add("on-focus");

    //    st.style.top = (t.offsetTop) + "px";
    //    st.style.left = (t.offsetLeft) + "px";
    //    st.style.width = t.clientWidth + "px";
    //    st.style.height = t.clientHeight + "px";
    //    st.style.transform = "translate(" + t.offsetTop + "px" + t.offsetLeft + "px" + ")";
    //    st.classList.add("on-focus");

    //    cs.style.top = t.offsetTop + "px";
    //    cs.style.left = t.offsetLeft + "px";
    //    cs.style.width = t.clientWidth + "px";
    //    cs.style.height = t.clientHeight + "px";
    //    cs.style.transform = "translate(" + t.offsetTop + "px" + t.offsetLeft + "px" + ")";
    //    cs.classList.add("on-focus");

    //} if (t.tagName == "NAV" ||
    //    t.parentElement.tagName == "NAV" ||
    //    (t.parentElement.classList.contains("menu-left") && t.tagName == "H1") ||
    //    (t.parentElement.tagName == "H1" && t.classList.contains("logo")) ||
    //    t.parentElement.classList.contains("navmenu") || t.classList.contains("category") ||
    //    (t.parentElement.classList.contains("category") && t.classList.contains("heading")) ||
    //    t.classList.contains("links") ||
    //    (t.parentElement.classList.contains("links") && t.tagName == "A") ||
    //    (t.parentElement.classList.contains("menu-right") && t.classList.contains("login-display")) ||
    //    t.parentElement.classList.contains("login-display") ||
    //    (t.parentElement.classList.contains("user-menu") && t.tagName == "BUTTON") ||
    //    t.parentElement.classList.contains("dropdown-menu") ||
    //    (t.parentElement.classList.contains("logout") && t.tagName == "SPAN") ||
    //    t.parentElement.classList.contains("overlay")) {
    //    f.style.display = "none";
    //    st.style.display = "none";
    //    move(c, e);
    //    //    skipFrames(move(c,e));
    //} else {
    //    move(c, e);
    //    move(f, e);
    //    move(st, e);
    //    move(cs, e);
    //    //skipFrames(move(c, e));
    //    //skipFrames(move(f, e));
    //    //skipFrames(move(st, e));
    //    //skipFrames(move(cs, e));
    //    f.classList.remove("on-focus");
    //    st.classList.remove("on-focus");
    //    cs.classList.remove("on-focus");
    //}
}

if (cursor) {
    window.addEventListener("mousemove", move)
}


// ATTEMPTS TO THROTTLE MOUSEMOVE EVENT

var timesPerSecond = 10;
function throttle(func) {
    setTimeout(func, 1000 / timesPerSecond);
}

var globalSkipCounter = 0;
var globalSkipRate = 5;

function skipFrames(func) {
    if (globalSkipCounter >= globalSkipRate) {
        func();
        globalSkipCounter = 0;
    } else {
        glbalSkipCounter++;
    }
}