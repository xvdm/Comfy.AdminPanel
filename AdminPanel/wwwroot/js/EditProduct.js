
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
     
        for (var i = 0; i < files.length; i++) {
            
        }

    })


    //#region AjaxImg
    //$('input[name="uploads"]').change(function () {
    //    var input = $(this);
    //    var files = input[0].files;

    //    var formData = new FormData();
    //    for (var i = 0; i < files.length; i++) {
    //        formData.append("files", files[i]);
    //    }


    //    if (files.length > 0)
    //    {
    //        console.log(id);
    //        $.ajax({
    //            url: "/Images/UploadProductImage/",
    //            type: "POST",
    //            contentType: false,
    //            processData: false,
    //            //data: {
    //            //    //"productId": id,
    //            //    "file": files[0]
    //            //},
    //            data: { formData },
    //            dataType: 'json',
    //            cache: false,
    //            success: function (result) {
    //                console.log(result);
    //            },
    //            error: function (xhr, status, error) {
    //                console.log("Error");
    //            }
    //        });
    //    }
    //});
    //#endregion


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

    // Characteristics
    // Edit
    $('button[name="edit-characteristic"]').on('click', function () {
        const value = $(this).val().split(",");
        $('input[name="edit-name"]').val(value[0]);
        $('input[name="edit-value"]').val(value[1]);
    });

    $('button[name="delete-characteristic"]').on('click', function () {
        const value = $(this);
        result = confirm("Підтвердіть видалення  (ID = " + value.val() + ")");
        if (result) {
            alert("Видалено")
        }
        else {

        }

    });

});
