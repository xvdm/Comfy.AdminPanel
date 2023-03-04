
var authConainer = document.getElementById("body-container-authorization")
var container = document.getElementById("body-container")

if (authConainer != null) {
    container.classList.remove("body-container")
    container.classList.add("body-container-auth")
}
else {
    container.classList.remove("body-container-auth")
    container.classList.add("body-container")
}

