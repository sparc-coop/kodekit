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

function move(obj, event) {
    obj.style = "";
    obj.style.top = event.clientY + "px";
    obj.style.left = event.clientX + "px";
}

// cursor/follow changes shape based on element hovered
// follow disappears when hovering over the nav

if (cursor) {
    window.addEventListener("mousemove", function (event) {
        var e = event;
        var t = e.target;
        var c = cursor;
        var f = follow;
        var st = start;
        var cs = shadow;

        if (t == null || t.parentElement == null)
            return;

        if (t.tagName == "BUTTON" || t.tagName == "A") {
            c.style.backgroundColor = "transparent";

            f.style.top = t.offsetTop + "px";
            f.style.left = t.offsetLeft + "px";
            f.style.width = t.clientWidth + "px";
            f.style.height = t.clientHeight + "px";
            f.classList.add("on-focus");

            st.style.top = (t.offsetTop) + "px";
            st.style.left = (t.offsetLeft) + "px";
            st.style.width = t.clientWidth + "px";
            st.style.height = t.clientHeight + "px";
            st.classList.add("on-focus");

            cs.style.top = t.offsetTop + "px";
            cs.style.left = t.offsetLeft + "px";
            cs.style.width = t.clientWidth + "px";
            cs.style.height = t.clientHeight + "px";
            cs.classList.add("on-focus");

        } if (t.tagName == "NAV" ||
            t.parentElement.tagName == "NAV" ||
            (t.parentElement.classList.contains("menu-left") && t.tagName == "H1") ||
            (t.parentElement.tagName == "H1" && t.classList.contains("logo")) ||            
            t.parentElement.classList.contains("navmenu") || t.classList.contains("category") ||
            (t.parentElement.classList.contains("category") && t.classList.contains("heading")) ||
            t.classList.contains("links") ||
            (t.parentElement.classList.contains("links") && t.tagName == "A") ||
            (t.parentElement.classList.contains("menu-right") && t.classList.contains("login-display")) ||
            t.parentElement.classList.contains("login-display") ||
            (t.parentElement.classList.contains("user-menu") && t.tagName == "BUTTON") ||
            t.parentElement.classList.contains("dropdown-menu") ||
            (t.parentElement.classList.contains("logout") && t.tagName == "SPAN") ||
            t.parentElement.classList.contains("overlay"))
        {
            f.style.display = "none";
            st.style.display = "none";
            move(c, e);
        } else {
            move(c, e);
            move(f, e);
            move(st, e);
            move(cs, e);
            f.classList.remove("on-focus");
            st.classList.remove("on-focus");
            cs.classList.remove("on-focus");
        }
    });
//    window.addEventListener("mouseover", function (event) {
//        var e = event;
//        var t = e.target;
//        var f = follow;

//        if (t.tagName == "NAV") {
//            f.style.display = "none";
//        }
//    });
}