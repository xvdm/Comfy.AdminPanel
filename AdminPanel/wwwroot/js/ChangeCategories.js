
$(document).ready(function () {

    $('p[name="maincategories-name"]').on('click',function () {
        let name = $(this).text()


        $('#categories-name').val(name)

        $('#categories-name').attr("value", $(this).attr("value"))
        $('#maincategories-selec option[value="-1"]').prop('selected', true);
        $('#maincategories-selec').attr('disabled', 'disabled')

        $('#change-category-btn').removeClass("h-hidden")
        $('#category-btn').addClass("h-hidden")
     });

    $('p[name="subcategories-name"]').on('click', function () {
        let name = $(this).text()
        let id = $(this).attr("value").split(',')

        $('#categories-name').val(name)

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
                    $('p[value="'+$('#categories-name').attr("value")+'"]').text(name)
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
                setTimeout(function () { message.hide('slow'); }, 4000);
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




});