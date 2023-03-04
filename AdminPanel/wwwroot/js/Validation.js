
function validateAuth() {

    var result = true;

    var name = document.getElementById("authNameField");
    var password = document.getElementById("authPasswordField");

    if (name.value == "") {
        name.style.borderColor = "red";
        name.placeholder ="Потрібне ім'я користувача"
        result = false;
    }
    if (password.value == "") {
        password.style.borderColor = "red";
        password.placeholder = "Потрібен пароль"
        result = false;
    }

    return result;
}