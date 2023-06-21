
$(document).ready(function () {

    $('p[name="maincategories-name"]').on('click', function () {
        document.getElementById('categoryImageSelect').value = "";

        // Update image on page
        var categoryId = $(this).attr('value');
        $.ajax({
            type: 'GET',
            url: '/Categories/GetMainCategoryImageUrl',
            data: { 'id': categoryId },
            success: function (result) {
                $('#selectedImage').attr('src', result.imageUrl);
                $('#uploadImageButton').addClass("h-hidden");
                if (result.imageUrl) {
                    $('#selectedImage').removeClass("h-hidden");
                    $('#removeImageButton').removeClass("h-hidden");
                }
                else {
                    $('#selectedImage').addClass("h-hidden");
                    $('#removeImageButton').addClass("h-hidden");
                }
            }
        });


        let name = $(this).text().split("| ")
        $('#categories-name').val(name[1])

        $('#categories-name').attr("value", $(this).attr("value"))
        $('#maincategories-selec option[value="-1"]').prop('selected', true);
        $('#maincategories-selec').attr('disabled', 'disabled')

        $('#change-category-btn').removeClass("h-hidden")
        $('#category-btn').addClass("h-hidden")
     });

    $('p[name="subcategories-name"]').on('click', function () {
        document.getElementById('categoryImageSelect').value = "";

        // Update image on page
        var categoryIds = $(this).attr('value').split(',');
        var subCategoryId = categoryIds[1];
        $.ajax({
            type: 'GET',
            url: '/Categories/GetSubcategoryImageUrl',
            data: { 'id': subCategoryId },
            success: function (result) {
                $('#selectedImage').attr('src', result.imageUrl);
                $('#uploadImageButton').addClass("h-hidden");
                if (result.imageUrl) {
                    $('#selectedImage').removeClass("h-hidden");
                    $('#removeImageButton').removeClass("h-hidden");
                }
                else {
                    $('#selectedImage').addClass("h-hidden");
                    $('#removeImageButton').addClass("h-hidden");
                }
            }
        });


        let name = $(this).text().split("| ")
        let id = $(this).attr("value").split(',')

        $('#categories-name').val(name[1])

        $('#categories-name').attr("value", $(this).attr("value"))
        $('#maincategories-selec option[value="' + id[0] + '"]').prop('selected', true);
        $('#maincategories-selec').attr('disabled', 'disabled');

        $('#change-category-btn').removeClass("h-hidden")
        $('#category-btn').addClass("h-hidden")
    });

    $('#categories-name').on('input', function () {
        if ($(this).val() == '') {
            $('#category-btn').removeClass("h-hidden")
            $('#maincategories-selec').removeAttr('disabled')
            $('#change-category-btn').addClass("h-hidden")
        }
    })

    $('#category-btn-change').on('click', function () {

        let name = $('#categories-name').val().trim()
        let mainCategory = $('#maincategories-selec option:selected').val()
        
        if (name !== '' && mainCategory === '-1') {

            let id = $('#categories-name').attr("value")

            $.ajax({
                type: 'GET',
                url: '/Categories/EditMainCategoryName/',
                data: { "id": id, "name": name },
                success: function (result) {
                    $('p[value="' + $('#categories-name').attr("value") + '"]').text(name)
                }
            })
        }

        if (name !== '' && mainCategory !== '-1') {

            let id = $('#categories-name').attr("value").split(',')

            $.ajax({
                type: 'GET',
                url: '/Categories/EditSubcategoryName/',
                data: { "id": id[1], "name": name },
                success: function (result) {
                    $('p[value="' + $('#categories-name').attr("value") + '"]').text(name)
                }
             })
        }

    })

    $('#category-btn-delete').on('click', function () {

        let name = $('#categories-name').val().trim()
        let mainCategory = $('#maincategories-selec option:selected').val()

        if (name !== '' && mainCategory === '-1') {

            if ($('p[name="maincategories-name"][value="' + $('#categories-name').attr("value") + '"]').closest('div[name="maincategories-list"]').find('div[name="subcategories-list"]').find('p').html() !== undefined) {
                let message = $('#message')
                $('#message-body-text').text("Неможливо видалити категорію, яка має підкатегорії.")
                message.show();
                setTimeout(function () { message.hide('900'); }, 4000);
            }
            else {
                let id = $('#categories-name').attr("value")
                $.ajax({
                    type: 'GET',
                    url: '/Categories/DeleteMainCategory/',
                    data: { "id": id },
                    success: function (result) {

                        $('p[name="maincategories-name"][value="' + $('#categories-name').attr("value") + '"]').remove()

                        $('#categories-name').text('')
                        $('#category-btn').removeClass("h-hidden")
                        $('#maincategories-selec').removeAttr('disabled')
                        $('#change-category-btn').addClass("h-hidden")
                    }
                })
            }
        }
        if (name !== '' && mainCategory !== '-1') {

            let id = $('#categories-name').attr("value").split(',')

            $.ajax({
                type: 'GET',
                url: '/Categories/DeleteSubcategory/',
                data: { "id": id[1]},
                success: function (result) {
                    $('p[name="subcategories-name"][value="' + $('#categories-name').attr("value") + '"]').remove()

                    $('#categories-name').text('')
                    $('#category-btn').removeClass("h-hidden")
                    $('#maincategories-selec').removeAttr('disabled')
                    $('#change-category-btn').addClass("h-hidden")
                }
            })
        }
    })

    $('#category-btn').on('click', function () {

        let name = $('#categories-name').val().trim()
        let mainCategory = $('#maincategories-selec option:selected').val()

        if (name !== '' && mainCategory === '-1') {

            $.ajax({
                type: 'POST',
                url: '/AdminPanel/CreateMainCategory/',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: { "name": name },
                success: function (result) {
                    location.reload();
                }
            })
        }

        if (name !== '' && mainCategory !== '-1') {
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/CreateSubcategory/',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: { "mainCategoryId": mainCategory,"name": name },
                success: function (result) {
                    location.reload();
                }
             })
        } 
    })


    // upload image
    $('#uploadImageButton').on('click', function () {
        var selectedFile = document.getElementById('categoryImageSelect').files[0];

        var categoryId = $('#categories-name').attr('value');
        var isSubcategory = categoryId.includes(',') ? true : false;
        if (isSubcategory) {
            categoryId = categoryId.split(',')[1];
        }

        var formData = new FormData();
        formData.append('image', selectedFile);
        formData.append('categoryId', categoryId);
        formData.append('isSubcategory', isSubcategory);

        $.ajax({
            type: 'POST',
            beforeSend: function (request) {
                request.setRequestHeader("RequestVerificationToken", document.getElementById("RequestVerificationToken").value);
            },
            url: '/Categories/AddImageToCategory',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                console.log('Image uploaded successfully!');
                $('#removeImageButton').removeClass("h-hidden");
                $('#uploadImageButton').addClass("h-hidden");
                $('#selectedImage').removeClass("h-hidden");
            },
            error: function (error) {
                console.error('Image upload failed:', error);
            }
        });
    });

    // remove image
    $('#removeImageButton').on('click', function () {
        var categoryId = $('#categories-name').attr('value');
        var isSubcategory = categoryId.includes(',') ? true : false;
        if (isSubcategory) {
            categoryId = categoryId.split(',')[1];
        }

        var formData = new FormData();
        formData.append('categoryId', categoryId);
        formData.append('isSubcategory', isSubcategory);

        $.ajax({
            type: 'POST',
            beforeSend: function (request) {
                request.setRequestHeader("RequestVerificationToken", document.getElementById("RequestVerificationToken").value);
            },
            url: '/Categories/RemoveImageFromCategory',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                console.log('Image removed successfully!');
                $('#removeImageButton').addClass("h-hidden");
                $('#uploadImageButton').addClass("h-hidden");
                $('#selectedImage').addClass("h-hidden");
                $('#selectedImage').attr('src', "");
            },
            error: function (error) {
                console.error('Image remove failed:', error);
            }
        });
    });

    // input image
    var fileInput = document.getElementById('categoryImageSelect');
    var imageElement = document.getElementById('selectedImage');
    fileInput.addEventListener('change', function (event) {
        var selectedFile = event.target.files[0];
        var reader = new FileReader();

        reader.onload = function () {
            imageElement.src = reader.result;
        };

        reader.readAsDataURL(selectedFile);

        $('#selectedImage').removeClass("h-hidden");
        $('#uploadImageButton').removeClass("h-hidden");
        $('#removeImageButton').addClass("h-hidden");
    });
});