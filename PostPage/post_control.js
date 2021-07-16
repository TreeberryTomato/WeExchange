let slidePage = document.querySelector(".slidepage");
let NextBtn = document.querySelector(".nextBtn");
let NextBtnSec = document.querySelector(".nextSec");
let prevBtn = document.querySelector(".prev-1");
let prevBtnSec = document.querySelector(".prev-2");
let submitBtn = document.querySelector(".submit");
let progressText = document.querySelectorAll(".step p");
let progressCheck = document.querySelectorAll(".step .check");
let bullet = document.querySelectorAll(".step .bullet");

let max = 3;
let current = 1;
let temp = new String("");
let temp2 = new String("");
let temp3 = new String("");
let temp4 = new String("");

function change() {
    let obj = document.getElementById("itemimage");
    temp = obj.files[0].name;
    console.log(temp);
}

function change2() {
    let obj = document.getElementById("itemimage2");
    temp2 = obj.files[0].name;
    console.log(temp2);
}

function change3() {
    let obj = document.getElementById("itemimage3");
    temp3 = obj.files[0].name;
    console.log(temp3);
}

function change4() {
    let obj = document.getElementById("itemimage4");
    temp4 = obj.files[0].name;
    console.log(temp4);
}

NextBtn.addEventListener("click", function () {
    let iname = document.getElementById("itemname").value;
    let icategory = document.getElementById("itemcategory").value;
    let itag = document.getElementById("itemtag").value;
    let idescription = document.getElementById("itemdescription").value;
    let iprice = document.getElementById("itemprice").value;
    console.log(iname + " " + icategory + " " + itag + " " + idescription + " " + iprice);
    //let iimage = document.getElementById("itemimage").value;

    let iimage = "";

    if (!iname || !icategory || !itag || !idescription || !iprice) return;

    slidePage.style.marginLeft = "-25%";
    bullet[current - 1].classList.add("active");
    progressText[current - 1].classList.add("active");
    progressCheck[current - 1].classList.add("active");
    current += 1;

    document.getElementById("iName").innerHTML = iname;
    document.getElementById("iCategory").innerHTML = icategory;
    document.getElementById("iTag").innerHTML = itag;
    document.getElementById("iDescription").innerHTML = idescription;
    iimage = iimage + " " + temp + " " + temp2 + " " + temp3 + " " + temp4;
    document.getElementById("iImage").innerHTML = iimage;
    document.getElementById("iPrice").innerHTML = iprice + " RMB";

    event.preventDefault();
});

NextBtnSec.addEventListener("click", function () {
    let uemail = document.getElementById("email").value;
    let uphoneno = document.getElementById("phoneNo").value;
    if (!uemail || !uphoneno) return;

    slidePage.style.marginLeft = "-50%";
    bullet[current - 1].classList.add("active");
    progressText[current - 1].classList.add("active");
    progressCheck[current - 1].classList.add("active");
    current += 1;

    document.getElementById("uEmail").innerHTML = uemail;
    document.getElementById("uPhoneNo").innerHTML = uphoneno;

    event.preventDefault();
});


submitBtn.addEventListener("click", function () {
    bullet[current - 1].classList.add("active");
    progressText[current - 1].classList.add("active");
    progressCheck[current - 1].classList.add("active");
    current += 1;
    event.preventDefault();
    /*
    setTimeout(function(){
        alert("You're successfully posting your item!");
        location.reload();
    }, 800);
     */
});


prevBtn.addEventListener("click", function () {
    slidePage.style.marginLeft = "0%";
    bullet[current - 2].classList.remove("active");
    progressText[current - 2].classList.remove("active");
    progressCheck[current - 2].classList.remove("active");
    current -= 1;
    event.preventDefault();
});

prevBtnSec.addEventListener("click", function () {
    slidePage.style.marginLeft = "-25%";
    bullet[current - 2].classList.remove("active");
    progressText[current - 2].classList.remove("active");
    progressCheck[current - 2].classList.remove("active");
    current -= 1;
    event.preventDefault();
});