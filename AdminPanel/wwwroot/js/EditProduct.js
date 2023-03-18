﻿
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
        var input = $(this)[0];

        var files = input.files;
        var divsImg = $('div[name="load-img"]')
        var divsPreImg = $('div[name="preload-img-div"]')
        var imgs = $('img[name="preload-img"]')
        var reader = [];

        $(input.files).each(function (i, el) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(imgs[i]).attr('src', e.target.result);
            };
            reader.readAsDataURL(input.files[i]);
            $(divsPreImg[i]).removeClass("h-hidden")
            $(divsImg[i]).addClass("h-hidden")
        });

        //for (var i = 0; i < files.length; i++) {

        //    reader[i] = new FileReader()
        //    reader[i].readAsDataURL(files[i]);

        //    reader[i].onload = function (e) {
        //        $(imgs[i]).attr('src', e.target.result);
        //    }

        
        //}



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
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
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
        $('#edit-name').val(value[0]);
        $('#edit-value').val(value[1]);
    });

    //Save
    $('#save-changes-modal').click(function () {
        const newName = $('#edit-name').val();
        const newValue = $('#edit-value').val();
        if ((newName.length > 1 && newValue.length > 1) && (newName.length < 50 && newValue.length < 50)) {

            alert("Збережено!");
        } else {
            alert("Помилка!");
        }

    });
    
    // Delete
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
               location.reload();
           }
        });
    });


});
