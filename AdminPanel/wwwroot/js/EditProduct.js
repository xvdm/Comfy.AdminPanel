
document.getElementById("save-btn").onclick = function () {
    document.getElementById("btnMainForm").click()
}

let categoriesSelect = document.getElementById("categories-select")
let divSubcategories = document.getElementById("subcategories-div")

if (categoriesSelect.options[categoriesSelect.selectedIndex].value != -1) {
    divSubcategories.classList.remove("h-hidden")
}

$(document).ready(function () {
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

    // Images

    $('input[name="uploads"]').change(function () {

        var input = $(this);
        var files = input[0].files;
        var formData = new FormData();

        formData.append("productId", id);
        for (var i = 0; i < files.length; i++) {
            formData.append("files", files[i]);
        }

        if (files.length > 0)
        {
            $.ajax({
                url: "/Images/UploadProductImage",
                data: formData,
                processData: false,
                contentType: false,
                type: "POST",
                success: function (result) {
                    alert(result);
                }
            });
        }
    });


    // Categories
    $('#categories-select').on('change', function () {
        let select = $(this).val()
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

    // Characteristics
    // Add

    $('#add-characteristic-btn').on('click', function () {
        let name = $('#addC-input-name');
        let value = $('#addC-input-value');
        
        if (name.val().trim() !== '' && value.val().trim() !== '') {
            $.ajax({
                type: 'POST',
                url: '/Products/AddCharacteristic/',
                data: { "productId": id, "name":name.val(), "value":value.val() },
                success: function (result) {
                    
                }
            });
            }
        else {
            name.css('border', '1px solid red')
            value.css('border', '1px solid red')
        }
    });

});
