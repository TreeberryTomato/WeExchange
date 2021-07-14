let wrap = document.querySelector(".wrap");
let next = document.querySelector(".arrow_right");
let prev = document.querySelector(".arrow_left");

next.onclick = function () {
	next_pic();
}

prev.onclick = function () {
	prev_pic();
}

//手动播放: 左右按钮
function next_pic () {
	index++;
	if(index > 4){
		index = 0;
	}
	showCurrentDot();
	let newLeft;
	if(wrap.style.left === "-6000px"){
		newLeft = -2000;
	}else{
		newLeft = parseInt(wrap.style.left)-1000;
	}
	wrap.style.left = newLeft + "px";
}

function prev_pic () {
	index--;
	if(index < 0){
		index = 4;
	}
	showCurrentDot();
	let newLeft;
	if(wrap.style.left === "0px"){
		newLeft = -4000;
	}else{
		newLeft = parseInt(wrap.style.left)+1000;
	}
	wrap.style.left = newLeft + "px";
}

//自动播放
let timer = null;
function autoPlay () {
	timer = setInterval(function () {
		next_pic();
	},4000);
}
autoPlay();

//停止播放
let container = document.querySelector(".container");
container.onmouseenter = function () {
	clearInterval(timer);
}
container.onmouseleave = function () {
	autoPlay();
}

//图片下方小圆点的滚动
let index = 0;
let dots = document.getElementsByTagName("span");
function showCurrentDot () {
	for(let i = 0, len = dots.length; i < len; i++){
		dots[i].className = "";
	}
	dots[index].className = "on";
}

//点击小圆点可以跳转到对应的图片
for (let i = 0, len = dots.length; i < len; i++){
	(function(i){
		dots[i].onclick = function () {
			let dis = index - i;
			if(index == 4 && parseInt(wrap.style.left)!==-5000){
				dis = dis - 5;
			}
			//和使用prev和next相同，在最开始的照片5和最终的照片1在使用时会出现问题，导致符号和位数的出错，做相应地处理即可
			if(index == 0 && parseInt(wrap.style.left)!== -1000){
				dis = 5 + dis;
			}
			wrap.style.left = (parseInt(wrap.style.left) +  dis * 1000)+"px";
			index = i;
			showCurrentDot();
		}
	})(i);
}

