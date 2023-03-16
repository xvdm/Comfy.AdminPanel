
document.getElementById("btn-header").onclick = function () {
    document.getElementById("btnMainForm").click()
}

let categoriesSelect = document.getElementById("categories-select")
let divSubcategories = document.getElementById("subcategories-div")

if (categoriesSelect.options[categoriesSelect.selectedIndex].value != -1) {
    divSubcategories.classList.remove("h-hidden")
}

$(document).ready(function () {

    // Visibility

    let visibilityStatus = $('#visibility-status').val()
    let switcherON = $('#visibility-on')
    let switcherOFF = $('#visibility-off')

    if (visibilityStatus == "True") {
        switcherON.attr('checked', true)
    } else {
        switcherOFF.attr('checked', true)
    }

    switcherON.on('click', function () {
        if ($(this).attr('checked') === undefined) {
            changeVisibility("True")

            $(this).attr('checked', true)
            switcherOFF.attr('checked', false)
        }
    });
    switcherOFF.on('click', function () {
        if ($(this).attr('checked') === undefined) {
            changeVisibility("False")

            $(this).attr('checked', true)
            switcherON.attr('checked', false)
        }
    });

    function changeVisibility(param) {
        let id = $('#product-id').val();

        $.ajax({
            type: 'GET',
            url: '/Products/ChangeProductActivityStatus/',
            data: { "productId": id, "isActive": param },
            success: function (result) {
                $('#visibility-status').val(param)
            }
        })
    }

    // Categories
    $('#categories-select').on('change', function () {
        var select = $(this).val()
            if(select != -1) {
                $.ajax({
                    type: 'GET',
                    url: '/Categories/GetSubcategoriesForMainCategory/',
                    data: { "mainCategoryId" : select },
                success: function (result) {
                    if (result.length == 0) {
                        $('#subcategories-div').hide();
                    }
                    else {
                        $('#subcategories-select').html(result);
                        $('#subcategories-div').show();
                    }
                }
            });
        }
    });
});
