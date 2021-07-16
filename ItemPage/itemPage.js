let uid = Number(document.getElementById("openbtn").value);
if (uid != Number("0")) {
    document.getElementById("register&login").style.display = 'none';
    document.getElementById("online&logout").style.display = '';
}
else {
    document.getElementById("register&login").style.display = '';
    document.getElementById("online&logout").style.display = 'none';
    document.getElementById("send").style.display = 'none';
}

function changeArea() {
    setInterval(function () {
        uid = Number(document.getElementById("openbtn").value);
        if (uid != Number("0")) {
            document.getElementById("register&login").style.display = 'none';
            document.getElementById("online&logout").style.display = '';
        }
        else {
            document.getElementById("register&login").style.display = '';
            document.getElementById("online&logout").style.display = 'none';
        }
    }, 5000);
}

let item_id = document.getElementById("item_id").value;
let isAdded = document.getElementById("isAdded").value == "true";

//if the user is not owner and is login.
if (document.getElementById("Added_img")) {
    if (isAdded) {
        document.getElementById("NotAdded_img").style.display = "none";

        document.getElementById("Added_img").onmouseover = function () {
            document.getElementById("Added_img").style.display = "none";
            document.getElementById("NotAdded_img").style.display = "";

            document.getElementById("NotAdded_img").onmouseout = function () {
                document.getElementById("Added_img").style.display = "";
                document.getElementById("NotAdded_img").style.display = "none";
            }
        }
    }
    else {
        document.getElementById("Added_img").style.display = "none";

        document.getElementById("NotAdded_img").onmouseover = function () {
            document.getElementById("NotAdded_img").style.display = "none";
            document.getElementById("Added_img").style.display = "";

            document.getElementById("Added_img").onmouseout = function () {
                document.getElementById("NotAdded_img").style.display = "";
                document.getElementById("Added_img").style.display = "none";
            }
        }
    }
}

let wrap = document.querySelector(".wrap");
let next = document.querySelector(".arrow_right");
let prev = document.querySelector(".arrow_left");
let nums = document.querySelectorAll(".slideshow .buttons span").length
let index = 0;

next.onclick = function () {
    next_pic();
}

prev.onclick = function () {
    prev_pic();
}

//手动播放: 左右按钮
function next_pic() {

    index++;
    if (index > nums - 1) {
        index = 0;
    }
    showCurrentDot();
    let newLeft;
    if (wrap.style.left === -550 * (nums - 1) + "px") {
        newLeft = 0;
    } else {
        newLeft = parseInt(wrap.style.left) - 550;
    }
    wrap.style.left = newLeft + "px";
}

function prev_pic() {
    index--;
    if (index < 0) {
        index = nums - 1;
    }
    showCurrentDot();
    let newLeft;
    if (wrap.style.left === "0px") {
        newLeft = -550 * (nums - 1);
    } else {
        newLeft = parseInt(wrap.style.left) + 550;
    }
    wrap.style.left = newLeft + "px";
}

//自动播放
let timer = null;
function autoPlay() {
    timer = setInterval(function () {
        next_pic();
    }, 5000);
}
autoPlay();

//停止播放
let slideshow = document.querySelector(".slideshow");
slideshow.onmouseenter = function () {
    clearInterval(timer);
}
slideshow.onmouseleave = function () {
    autoPlay();
}

//图片下方小圆点的滚动
let dots = document.querySelectorAll(".slideshow .buttons span")
function showCurrentDot() {
    for (let i = 0, len = dots.length; i < len; i++) {
        dots[i].className = "";
    }
    dots[index].className = "on";
}

//点击小圆点可以跳转到对应的图片
for (let i = 0, len = dots.length; i < len; i++) {
    (function (i) {
        dots[i].onclick = function () {
            let dis = index - i;

            wrap.style.left = (parseInt(wrap.style.left) + dis * 550) + "px";
            index = i;
            showCurrentDot();
        }
    })(i);
}

// search button submit
function submitSearch() {
    document.getElementById("searchForm").submit();
}