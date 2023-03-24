$(document).ready(function editBrandJS() {
    // Brand 
    // Add 
    $('#add-brand-btn').on('click', function () {
        const name = $('#add-brand-name').val();
        if (name.trim() !== '') {
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/CreateBrand/',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: { "name": name },
                success: function (result) {
                    location.reload();
                }
            });
        }
    });

    // Characteristics 
    // Edit 
    $('button[name="edit-brand"]').on('click', function () {
        const value = $(this).val().split(",");
        $('#edit-value-id').val(value[0]);
        $('#edit-brand').val(value[1]);
    });

    //Save modal 
    $('#save-changes-modal').click(function () {
        const newName = $('#edit-brand').val();
        const Id = $('#edit-value-id').val();
        if (newName.length > 1  && newName.length < 50) {
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/UpdateBrand/',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: { "brandId": Id, "newName": newName},
                success: function () {
                    location.reload();
                }
            });
        }
    });

    // Brand
    // Delete 
    $('button[name="delete-brand"]').on('click', function () {
        const Id = $(this).val();
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/DeleteBrand/',
            headers: {
                RequestVerificationToken: $('#RequestVerificationToken').val()
            },
            data: { "brandId": Id },
            success: function (result) {
                $('tr[name="' + Id + '"]').remove()
            },
            error: function () {
                alert("Бренд має наявні товари");
            }
        });
    });


});
