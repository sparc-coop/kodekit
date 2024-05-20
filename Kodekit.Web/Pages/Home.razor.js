
let canvas = document.getElementById('canvas');
let ctx = canvas.getContext('2d');
var window_width = window.innerWidth;
var window_height = window.innerHeight;
console.log(window_width);

canvas.width = window_width;
canvas.height = window_height;
canvas.style.position = "absolute";
canvas.style.top = "0";
canvas.style.left = "0";
canvas.style.background = "#000";

// ** EXAMPLE CONSTRUCTOR FOR ANIMATED CIRCLE THAT BOUNCES OFF THE WALLS OF THE CONTAINER ** //

// class Blob {
    //     constructor(xpos, ypos, radius, color, speed) {
    //         this.xpos = xpos;
    //         this.ypos = ypos;
    //         this.radius = radius;
    //         this.color = color;
    //         this.speed = speed;

    //         this.dx = 1 * this.speed;
    //         this.dy = 1 * this.speed;
    //     }

    //     draw(ctx) {
    //         ctx.beginPath();
    //         ctx.strokeStyle = this.color;
    //         ctx.lineWidth = 5;
    //         ctx.arc(this.xpos, this.ypos, this.radius, 0, Math.PI * 2, false);
    //         ctx.stroke();
    //         ctx.closePath();
    //     }

    //     update() {
    //         ctx.clearRect(0, 0, window_width, window_height);
    //         this.draw(ctx);

    //         if ((this.xpos + this.radius) > window_width) {
    //             this.dx = -this.dx;
    //         }

    //         if ((this.xpos - this.radius) < 0) {
    //             this.dx = -this.dx;
    //         }

    //         if ((this.ypos + this.radius) > window_height) {
    //             this.dy = -this.dy;
    //         }

    //         if ((this.ypos - this.radius) < 0) {
    //             this.dy = -this.dy;
    //         }


    //         this.xpos += this.dx;
    //         this.ypos += this.dy;
    //     }
    // }

    // let random_x = Math.random() * window_width;
    // let random_y = Math.random() * window_height;

    // let my_blob = new Blob(100, 100, 50, "blue", 5);
    // my_blob.draw(ctx);
    // let updateBlob = function() {
    //     requestAnimationFrame(updateBlob);
    //     my_blob.update();
    // }

    // updateBlob();


    // ** TESTING MOUSE INTERSECTION WITH POSITION OF CIRCLE ** //

    // function getMousePos(canvas, evt) {
    //     var rect = canvas.getBoundingClientRect();
    //     return {
    //         x: (evt.clientX - rect.left) / (rect.right - rect.left) * canvas.width,
    //         y: (evt.clientY - rect.top) / (rect.bottom - rect.top) * canvas.height
    //     };
    // }

    // let mouse_over = function (event) {
    //     event.preventDefault();
    //     let mousePos = getMousePos(canvas, event);
    //     var mouseX = mousePos.x;
    //     var mouseY = mousePos.y;
    //     console.log(mouseX, mouseY);
    //     console.log(my_blob.xpos, my_blob.ypos);

    //     // if mouse overlaps with blob, blob will change direction in X and Y axis
    //     if (mouseX == (my_blob.xpos - my_blob.radius) && mouseY == (my_blob.ypos - my_blob.radius)) {
    //         console.log("hit");
    //         my_blob.dx = -my_blob.dx;
    //         my_blob.dx += my_blob.dx

    //         my_blob.dy = -my_blob.dy;
    //         my_block.dy += my_blob.dy;
    //     }
    // }

    // my_blob.AddEventListener("onmouseover", mouse_over);

    // canvas.onmousedown = (e) => { console.log('mousedown', e.layerX, e.layerY) };

    // ** CONSTRUCTOR FOR PNG BOBS ** //

    // class Blob {
    //     constructor(src, xpos, ypos, width, height, speed) {
    //         this.src = src;
    //         this.xpos = xpos;
    //         this.ypos = ypos;
    //         this.width = width;
    //         this.height = height;
    //         this.speed = speed;

    //         this.dx = 1 * this.speed;
    //         this.dy = 1 * this.speed;
    //     }

    //     draw(ctx) {
    //         var big_blob = new Image();
    //         big_blob.src = this.src;
    //         big_blob.onload = () => ctx.drawImage(big_blob, this.xpos, this.ypos, this.width, this.height);
    //         big_blob.filter = "blur(200px)";
    //     }

    //     update() {
    //         ctx.clearRect(0, 0, window_width, window_height);
    //         this.draw(ctx);

    //         if ((this.xpos + (this.width/2)) > window_width) {
    //             this.dx = -this.dx;
    //         }

    //         if ((this.xpos - (this.width/2)) < 0) {
    //             this.dx = -this.dx;
    //         }

    //         if ((this.ypos + (this.height/2)) > window_height) {
    //             this.dy = -this.dy;
    //         }

    //         if ((this.ypos - (this.height/2)) < 0) {
    //             this.dy = -this.dy;
    //         }

    //         this.xpos += this.dx;
    //         this.ypos += this.dy;
    //     }
    // }

    // ** BLOBS DRAWN USING CONSTRUCTOR HOWEVER LOADING IMAGES IS SLOW IN BROWSER ** //
    // ANIMATION ALSO NOT WORKING

    // let blob_gradient = new Blob("blobs/blob-gradient.png", window_width / 2, window_height / -4, window_width / 2, window_height * 1.2);
    // blob_gradient.draw(ctx);
    // let updateBlob = function() {
    //     requestAnimationFrame(updateBlob);
    //     blob_gradient.update();
    // }

    // let blob_blue = new Blob("blobs/blob-blue.png", window_width / 3, window_height / 3, window_width * .6, window_height * 1.2);
    // blob_blue.draw(ctx);
    // let updateBlob = function () {
    //     requestAnimationFrame(updateBlob);
    //     blob_blue.update();
    // }

    // let blob_purple = new Blob("blobs/blob-purple.png", 0, window_height / -4, window_width * .75, window_height);
    // blob_purple.draw(ctx);
    // let updateBlob = function () {
    //     requestAnimationFrame(updateBlob);
    //     blob_purple.update();
    // }

    // ** ADDING BLOBS WITHOUT CONSTRUCTOR ** //

    // const blobGradient = new Image();
    // blobGradient.src = "blobs/blob-gradient.png";
    // blobGradient.onload = () => ctx.drawImage(blobGradient, window_width/2, window_height/-4, window_width/2, window_height*1.2);

    // const blobBlue = new Image();
    // blobBlue.src = "blobs/blob-blue.png";
    // blobBlue.onload = () => ctx.drawImage(blobBlue, window_width/3, window_height/3, window_width*.6, window_height*1.2);

    // const blobPurple = new Image();
    // blobPurple.src = "blobs/blob-purple.png";
    // blobPurple.onload = () => ctx.drawImage(blobPurple, 0, window_height/-4, window_width*.75, window_height);

    // ctx.filter = "blur(200px)";

    // ** TRIED TO LAYER A SECON CANVAS TO SEPARATE THE NOISE OVERLAY FROM THE BLUR FILTER **//
    // BUT NOT WORKING, BLUR STILL AFFECTING NOISE OVERLAY TO DISPLAY PROPERLY

    // let canvas_overlay = document.getElementById('canvas-overlay');
    // let ctx_overlay = canvas.getContext('2d');

    // canvas_overlay.width = window_width;
    // canvas_overlay.height = window_height;
    // canvas_overlay.style.position = "absolute";
    // canvas_overlay.style.top = "0";
    // canvas_overlay.style.left = "0";

    // const overlay = new Image();
    // overlay.src = "blobs/noise-3.png";
    // overlay.onload = () => ctx_overlay.drawImage(overlay, 0, 0, window_width, window_height);

    // let mouse_over = function (event) {
    //     event.preventDefault();
    //     console.log("hit");
    //     my_blob.dx = -my_blob.dx;
    //     my_blob.dx += my_blob.dx

    //     my_blob.dy = -my_blob.dy;
    //     my_block.dy += my_blob.dy;
    // }

    // ** DRAW SVG IN CANVAS **//
    // Blob constructor for svg
    // NOT WORKING
    class BigBlob {
        constructor(svg, img, xpos, ypos, width, height, speed, direction) {
            this.svg = svg;
            this.img = img;
            this.xpos = xpos;
            this.ypos = ypos;
            this.width = width;
            this.height = height;
            this.speed = speed;
            this.direction = direction;

            this.dx = 1 * this.speed;
            this.dy = 1 * this.speed;
        }

        draw(ctx) {
            //console.log("drawing blob");
            var blobSvg = document.getElementById(this.svg);
            let xml = new XMLSerializer().serializeToString(blobSvg);     // get svg data
            let svg64 = btoa(xml);                                          // make it base64
            let b64Start = 'data:image/svg+xml;base64,';
            let image64 = b64Start + svg64;                                 // prepend a "header"

            var blobImg = document.getElementById(this.img);
            blobImg.src = image64;                                          // image source
            blobImg.onload = () => {
                ctx.beginPath();
                ctx.drawImage(blobImg, this.xpos, this.ypos); //draw
                ctx.closePath();
                ctx.fill();
            }
        }

        animate(ctx) {
            setInterval(this.update(ctx), 2000);
        }

        update(ctx) {
            setTimeout(ctx.clearRect(0, 0, window_width, window_height), 2000);
            console.log("clearing canvas");

            setTimeout(this.draw(ctx), 2000);
            console.log("re-drawing blobs");

            if (this.direction == "right") {
                this.xpos += this.dx;
            }

            if (this.direction == "left") {
                this.xpos -= this.dx;
            }

            if (this.direction == "up_right") {
                this.xpos += this.dx;
                this.ypos -= this.dy;
            }

            requestAnimationFrame(this.update(ctx));

            // if ((this.xpos + (this.width / 2)) > window_width) {
            //     this.dx = -this.dx;
            // }

            // if ((this.xpos - (this.width / 2)) < 0) {
            //     this.dx = -this.dx;
            // }

            // if ((this.ypos + (this.height / 2)) > window_height) {
            //     this.dy = -this.dy;
            // }

            // if ((this.ypos - (this.height / 2)) < 0) {
            //     this.dy = -this.dy;
            // }
        }
    }

let blob_gradient = new BigBlob("blob-gradient", "blob-gradient__img", window_width / 2, window_height / -4, window_width / 2, window_height * 1.2, 0.5, "left");
//blob_gradient.draw(ctx);
blob_gradient.animate(ctx);

// let updateGradientBlob = function () {
    //     requestAnimationFrame(updateGradientBlob);
    //     console.log("requesting animation frame");
    //     blob_gradient.update();
    //     console.log("updating blob");
    // }
    // updateGradientBlob();

    // let blob_blue = new BigBlob("blob-blue", "blob-blue__img", window_width / 3, window_height / 3, window_width * .6, window_height * 1.2, 1, "up_right");
    // blob_blue.draw(ctx);
    // let updateBlueBlob = function () {
    //     requestAnimationFrame(updateBlueBlob);
    //     // blob_blue.update();
    // }

    // updateBlueBlob();

    // let blob_purple = new BigBlob("blob-purple", "blob-purple__img", 0, window_height / -4, window_width * .75, window_height, 1, "right");
    // blob_purple.draw(ctx);
    // let updatePurpleBlob = function () {
    //     requestAnimationFrame(updatePurpleBlob);
    //     blob_purple.update();
    // }

    // updatePurpleBlob();

    function resizeCanvas() {
        window_width = window.innerWidth;
        window_height = window.innerHeight;
        canvas.width = window_width;
        canvas.height = window_height;
        blob_gradient = new BigBlob("blob-gradient", "blob-gradient__img", window_width / 2, window_height / -4, window_width / 2, window_height * 1.2, 1, "left");
        blob_gradient.draw(ctx);
        // blob_blue = new BigBlob("blob-blue", "blob-blue__img", window_width / 3, window_height / 3, window_width * .6, window_height * 1.2, 1, "up_right");
        // blob_blue.draw(ctx);
        // blob_purple = new BigBlob("blob-purple", "blob-purple__img", 0, window_height / -4, window_width * .75, window_height, 1, "right");
        // blob_purple.draw(ctx);
    }

window.addEventListener('resize', resizeCanvas, false);

// ctx.filter = "blur(200px)"

// var blob_gradient = document.getElementById("blob-gradient");
// let xml = new XMLSerializer().serializeToString(blob_gradient);     // get svg data
// let svg64 = btoa(xml);                                          // make it base64
// let b64Start = 'data:image/svg+xml;base64,';
// let image64 = b64Start + svg64;                                 // prepend a "header"

// var blob_gradient__img = document.getElementById("blob-gradient__img");
// blob_gradient__img.src = image64;                                          // image source
// blob_gradient__img.onload = x => {
//     ctx.drawImage(blob_gradient__img, window_width / 2, window_height / -4, window_width / 2, window_height * 1.2); // draw
// }

// var blob_blue = document.getElementById("blob-blue");
// xml = new XMLSerializer().serializeToString(blob_blue);     // get svg data
// svg64 = btoa(xml);                                          // make it base64
// b64Start = 'data:image/svg+xml;base64,';
// image64 = b64Start + svg64;                                 // prepend a "header"

// var blob_blue__img = document.getElementById("blob-blue__img");
// blob_blue__img.src = image64;                                          // image source
// blob_blue__img.onload = x => {
//     ctx.drawImage(blob_blue__img, window_width / 3, window_height / 3, window_width * .6, window_height * 1.2); // draw
// }

// var blob_purple = document.getElementById("blob-purple");
// console.log(blob_purple);
// xml = new XMLSerializer().serializeToString(blob_purple);     // get svg data
// console.log(xml);
// svg64 = btoa(xml);                                          // make it base64
// b64Start = 'data:image/svg+xml;base64,';
// image64 = b64Start + svg64;                                 // prepend a "header"

// var blob_purple__img = document.getElementById("blob-purple__img");
// blob_purple__img.src = image64;                                          // image source
// blob_purple__img.onload = x => {
//     ctx.drawImage(blob_purple__img, 0, 0, window_width * .75, window_height); // draw
// }
