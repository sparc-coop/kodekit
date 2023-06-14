// create cursor elements

document.body.style.cursor = "none";

var cursor = document.createElement("div");
cursor.classList.add("cursor");
document.body.appendChild(cursor);

var follow = document.createElement("div");
follow.classList.add("follow");
document.body.appendChild(follow);

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
        var f = follow;
        var c = cursor;

        if (t.tagName == "BUTTON" || t.tagName == "A") {
            c.style.backgroundColor = "transparent";

            f.style.top = t.offsetTop + "px";
            f.style.left = t.offsetLeft + "px";
            f.style.width = t.clientWidth + "px";
            f.style.height = t.clientHeight + "px";

            f.classList.add("on-focus");
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
            move(c, e);
        } else {
            move(c, e);
            move(f, e);
            f.classList.remove("on-focus");
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