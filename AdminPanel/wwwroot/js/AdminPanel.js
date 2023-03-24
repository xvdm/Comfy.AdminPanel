

var URL = document.location.pathname; 
var userMainPosition = URL.split("/")

var div = document.createElement("div")
div.classList.add("subLink")
var ul = document.createElement("ul")
var li, a, span
var links, href

function blockMainLink(elem) {
    elem.querySelector(".menu-link").removeAttribute("href")
}

function createLI(links, href) {
    for (var i = 0; i < links.length ; i++) {
      
        li = document.createElement("li")
        a = document.createElement("a")

        a.setAttribute("href", href[i])
        a.textContent = links[i]

        li.appendChild(a)
        ul.appendChild(li)
    }
}
/*<a class="navbar-brand " asp-area="" asp-controller="Products" asp-action="Products" target="_self">*/

switch (userMainPosition[1]) {

    case "Orders":
        let divMain = document.getElementById("Main");
        blockMainLink(divMain)

        links = ["Активні", "Прийняті", "Виконані", "Скасовані"];
        href = ["/Orders/ActiveOrders", "/Orders/AcceptedOrders", "/Orders/CompletedOrders", "/Orders/CancelledOrders"]
        createLI(links, href)
        div.appendChild(ul)
        divMain.appendChild(div)
        break;

    case "Products":
        let divGoods = document.getElementById("Goods");
        blockMainLink(divGoods)
     
        links = ["Товари", "Категорії","Бренди","Моделі"];
        href = ["/Products/Products", "/Categories/Index","/AdminPanel/CreateBrand","/AdminPanel/CreateModel"]
        createLI(links, href)
        div.appendChild(ul)
        divGoods.appendChild(div)
        break;

    case "AdminPanel":
        let divApanel = document.getElementById("Goods");
        blockMainLink(divApanel)
     
        links = ["Товари", "Категорії","Бренди","Моделі"];
        href = ["/Products/Products", "/Categories/Index","/AdminPanel/CreateBrand","/AdminPanel/CreateModel"]
        createLI(links, href)
        div.appendChild(ul)
        divApanel.appendChild(div)
        break;

    case "Categories":
        let divCategories = document.getElementById("Goods");
        blockMainLink(divCategories)
  

        links = ["Товари", "Категорії", "Бренди", "Моделі"];
        href = ["/Products/Products", "/Categories/Index", "/AdminPanel/CreateBrand", "/AdminPanel/CreateModel"]

        createLI(links, href)

        div.appendChild(ul)
        divCategories.appendChild(div)
        
        break;

    case "Logging":
        let divLogging = document.getElementById("Logging");
        blockMainLink(divLogging)

        links = ["Журнал користувачів", "Журнал товаров"];
        href = ["/Logging/UserLogs", ""]

        createLI(links, href)

        div.appendChild(ul)
        divLogging.appendChild(div)
        break;
}