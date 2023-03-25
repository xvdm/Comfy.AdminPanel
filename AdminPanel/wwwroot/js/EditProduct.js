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
    // Add
    $('#add-characteristic-btn').on('click', function () {
        const name = $('#addC-input-name').val();
        const value = $('#addC-input-value').val();
        if (name.trim() !== '' && value.trim() !== '') {
            $.ajax({
                type: 'POST',
                url: '/Products/AddCharacteristic/',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: { "productId": id, "name": name, "value": value },
                success: function (result) {
                    $('#addC-input-name').val('');
                    $('#addC-input-value').val('');
                    $("#list-characteristics").append($('<tr name=' + result['id'] + '></tr>'));
                    $('tr[name=' + result['id'] + ']').append($('<td><p>' + result['id'] + '</p></td>'
                        + '<td><p>' + result['characteristicsName']['name'] + '</p></td>'
                        + '<td><p>' + result['characteristicsValue']['value'] + '</p></td>'
                        + '<td><button value="' + result['characteristicsName']['name'] + ',' + result['characteristicsValue']['value'] + ',' + result['id'] + '" data-bs-toggle="modal" data-bs-target="#staticBackdrop" type="button" name="edit-characteristic">Редагувати</button></td>'
                        + '<td><button value="' + result['id'] + '" type="button" name="delete-characteristic">Видалити</button></td>'));
                    location.reload();
                    /*editProductJS()*/
                }
            });

        }
    });

    // Characteristics
    // Edit
    $('button[name="edit-characteristic"]').on('click', function () {
        const value = $(this).val().split(",");
        $('#edit-name').val(value[0]);
        $('#edit-value').val(value[1]);
        $('#edit-value-id').val(value[2]);
    });

    //Save modal
    $('#save-changes-modal').click(function () {
        const newName = $('#edit-name').val();
        const newValue = $('#edit-value').val();
        const characteristicId = $('#edit-value-id').val();

        if ((newName.length > 1 && newValue.length > 1) && (newName.length < 50 && newValue.length < 50)) {
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
        }
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