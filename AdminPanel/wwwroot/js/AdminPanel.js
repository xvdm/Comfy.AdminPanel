var URL = document.location.pathname; 
var userMainPosition = URL.split("/")

var div = document.createElement("div")
div.classList.add("subLink")
var ul = document.createElement("ul")
var li, a, span
let links

function createLI(links) {
    links.forEach(function (link) {
        li = document.createElement("li")
        a = document.createElement("a")
        a.textContent = link
        li.appendChild(a)
        ul.appendChild(li)
    })
}

switch (userMainPosition[1]) {
    case "Products":
        let divGoods = document.getElementById("Goods");
        links = ["Товари", "Категорії"];
        createLI(links)
        div.appendChild(ul)
        divGoods.appendChild(div)
        break;
    case "Accounts":
        let divAccounts = document.getElementById("Accounts");
        links = ["Активні", "Заблоковані"];
        createLI(links)
        div.appendChild(ul)
        divAccounts.appendChild(div)
        break;
    case "Logging":
        let divLogging = document.getElementById("Logging");
        links = ["Журнал користувачів", "Журнал товаров" ];
        createLI(links)
        div.appendChild(ul)
        divLogging.appendChild(div)
        break;
}

