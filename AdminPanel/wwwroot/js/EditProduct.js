
document.getElementById("btn-header").onclick = function () {
    document.getElementById("btnMainForm").click()
}

let categoriesSelect = document.getElementById("categories-select")
let divSubcategories = document.getElementById("subcategories-div")

if (categoriesSelect.options[categoriesSelect.selectedIndex].value != -1) {
    divSubcategories.classList.remove("h-hidden")
}

let visibility = document.getElementById("visibility-status").value
let switcherON = document.getElementById("visibility-on")
let switcherOFF = document.getElementById("visibility-off")

if (visibility) {
    switcherON.setAttribute('checked', 'true');
} else {
    switcherOFF.setAttribute('checked', 'true');
}

$(document).ready(function () {
    $('#categories-select').on('change', function () {
        var select = $(this).val()
            if(select != -1) {
            $.ajax({
                type: 'GET',
                url: '/Categories/GetSubcategoriesForMainCategory',
                data: select,
                success: function (data) {
                    if (data.length == 0) {
                        $('#subcategories-div').hide();
                    }
                    else {
                        $('#subcategories-select').html(data);
                        $('#subcategories-select').show();
                    }
                }
            });
        }
    });
});

