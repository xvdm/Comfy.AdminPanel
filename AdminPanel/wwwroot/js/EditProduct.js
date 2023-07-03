document.getElementById("save-btn").onclick = function () {
    document.getElementById("btnMainForm").click()
}

let categoriesSelect = document.getElementById("categories-select")
let divSubcategories = document.getElementById("subcategories-div")

if (categoriesSelect.options[categoriesSelect.selectedIndex].value != -1) {
    divSubcategories.classList.remove("h-hidden")
}


$(document).ready(function editProductJS() {
    let id = $('#product-id').val();
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
        let select = $(this).val()
        if (select != -1) {
            $.ajax({
                type: 'GET',
                url: '/Categories/GetSubcategoriesForMainCategory/',
                data: { "mainCategoryId": select },
                success: function (result) {
                    if (result.length == 0) {
                        $('#subcategories-div').hide();
                        $('#subcategories-div').addAttr('hidden');
                    }
                    else {
                        $('#subcategories-div').removeAttr('hidden');
                        $('#subcategories-select').html(result);
                        $('#subcategories-div').show();
                    }
                }
            });
        }
    });

    // Characteristics
    // Edit
    $('button[name="edit-characteristic"]').on('click', function () {
        const value = $(this).val().split(";");
        $('#edit-name').val(value[0]);
        $('#edit-value').val(value[1]);
        $('#edit-value-id').val(value[2]);

    });

    //Save modal
    $('#save-changes-modal').click(function () {
        const newName = $('#edit-name').val();
        const newValue = $('#edit-value').val();
        const characteristicId = $('#edit-value-id').val();

            $.ajax({
                type: 'POST',
                url: '/Products/EditCharacteristic/',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: { "productId": id, "id": characteristicId, "name": newName, "value": newValue },
                success: function (result) {
                    location.reload();
                }
            });
    });

    
    //// Delete
    $('button[name="delete-characteristic"]').on('click', function () {
        const characteristicsId = $(this).val();
        $.ajax({
            type: 'POST',
            url: '/Products/DeleteCharacteristic/',
            headers: {
                RequestVerificationToken: $('#RequestVerificationToken').val()
            },
            data: { "productId": id, "id": characteristicsId },
            success: function (result) {
                $('tr[name="' + characteristicsId + '"]').remove()
            }
        });
    });
});