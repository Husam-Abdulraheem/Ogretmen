let profileImg = document.getElementsByClassName("profileImage")[0];
let profileList = document.getElementsByClassName("profileList")[0];

profileImg.addEventListener("click", function showList() {
    if (profileList.style.display == "flex") {
        profileList.style.display = "none";
    } else {
        profileList.style.display = "flex";
    }
})

