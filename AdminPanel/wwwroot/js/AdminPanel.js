
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

switch (userMainPosition[1]) {

    case "Orders":
        let divMain = document.getElementById("Main");
        blockMainLink(divMain)

        links = ["Усі замовлення"];
        href = ["/Orders/Orders"]
        createLI(links, href)
        div.appendChild(ul)
        divMain.appendChild(div)
        break;

    case "Products":
        let divGoods = document.getElementById("Goods");
        blockMainLink(divGoods)
     
        links = ["Товари", "Бренди","Моделі","Вітрина товарів","Категорії", "Фільтри підкатегорій"];
        href = ["/Products/Products", "/AdminPanel/Brands", "/AdminPanel/Models", "/Showcase/Index", "/Categories/Index","/Categories/SubcategoryFilters"]
        createLI(links, href)
        div.appendChild(ul)
        divGoods.appendChild(div)
        break; 

    case "Comments":
        let divComments = document.getElementById("Comments");
        blockMainLink(divComments)
     
        links = ["Активні питання", "Активні відгуки", "Активні відповіді на питання", "Активні відповіді на відгуки", "Неактивні питання",
            "Неактивні відгуки", "Неактивні відповіді на питання", "Неактивні відповіді на відгуки"];
        href = ["/Comments/ActiveQuestions", "/Comments/ActiveReviews", "/Comments/ActiveQuestionAnswers", "/Comments/ActiveReviewAnswers",
            "/Comments/InactiveQuestions", "/Comments/InactiveReviews", "/Comments/InactiveQuestionAnswers", "/Comments/InactiveReviewAnswers"]
        createLI(links, href)
        div.appendChild(ul)
        divComments.appendChild(div)
        break;

    case "AdminPanel":
        let divApanel = document.getElementById("Goods");
        blockMainLink(divApanel)
     
        links = ["Товари",  "Бренди", "Моделі", "Вітрина товарів", "Категорії", "Фільтри підкатегорій"];
        href = ["/Products/Products", "/AdminPanel/Brands", "/AdminPanel/Models", "/Showcase/Index", "/Categories/Index", "/Categories/SubcategoryFilters"]
        createLI(links, href)
        div.appendChild(ul)
        divApanel.appendChild(div)
        break;

    case "Categories":
        let divCategories = document.getElementById("Goods");
        blockMainLink(divCategories)
  

        links = ["Товари", "Бренди", "Моделі", "Вітрина товарів", "Категорії", "Фільтри підкатегорій"];
        href = ["/Products/Products", "/AdminPanel/Brands", "/AdminPanel/Models", "/Showcase/Index", "/Categories/Index", "/Categories/SubcategoryFilters"]

        createLI(links, href)

        div.appendChild(ul)
        divCategories.appendChild(div)
        
        break;

    case "Showcase":
        let divShowcase = document.getElementById("Goods");
        blockMainLink(divShowcase)
  

        links = ["Товари",  "Бренди", "Моделі", "Вітрина товарів","Категорії", "Фільтри підкатегорій"];
        href = ["/Products/Products", "/AdminPanel/Brands", "/AdminPanel/Models", "/Showcase/Index", "/Categories/Index", "/Categories/SubcategoryFilters"]

        createLI(links, href)

        div.appendChild(ul)
        divShowcase.appendChild(div)
        
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

    case "Accounts":
        let divAccounts = document.getElementById("Accounts");
        blockMainLink(divAccounts)

        links = ["Активні користувачі","Заблоковані користувачі"];
        href = ["/Accounts/ActiveUsers","/Accounts/LockoutUsers"]

        createLI(links, href)

        div.appendChild(ul)
        divAccounts.appendChild(div)
        break;

    case "Characteristics":
        let divCharacteristics = document.getElementById("Characteristics");
        blockMainLink(divCharacteristics)

        links = ["Імена характеристик", "Значення характеристик"];
        href = ["/Characteristics/CharacteristicNames", "/Characteristics/CharacteristicValues"]

        createLI(links, href)

        div.appendChild(ul)
        divCharacteristics.appendChild(div)
        break;
}