$(document).ready(function editProductJS() {
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
                    $("#list-brands").append($('<tr name=' + result['id'] + '></tr>'));
                    $('tr[name=' + result['id'] + ']').append($('<td><p>' + result['name'] + '</p></td>'
                        + '<td><button value="' + result['id'] + ',' + result['name'] + '" data-bs-toggle="modal" data-bs-target="#staticBackdrop" type="button" name="edit-brand">Редагувати</button></td>'
                        + '<td><button value="' + result['id'] + '" type="button" name="delete-brand">Видалити</button></td>'));
                    editProductJS()
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

                }
            });
        }
    });

    // Brand
    // Delete 
    $('button[name="delete-brand"]').on('click', function () {
        const Id = $(this).val();
        alert(Id);
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/DeleteBrand/',
            headers: {
                RequestVerificationToken: $('#RequestVerificationToken').val()
            },
            data: { "brandId": Id },
            success: function (result) {
                $('tr[name="' + Id + '"]').remove()
            }
        });
    });


});
