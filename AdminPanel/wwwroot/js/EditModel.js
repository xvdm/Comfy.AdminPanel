﻿$(document).ready(function editModelJS() {
    // Model 
    // Add 
    $('#add-model-btn').on('click', function () {
        const name = $('#add-model-name').val();
        if (name.trim() !== '') {
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/CreateModel/',
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

    // Model 
    // Edit 
    $('button[name="edit-model"]').on('click', function () {
        const value = $(this).val().split(",");
        $('#edit-value-id').val(value[0]);
        $('#edit-model').val(value[1]);
    });

    //Save modal 
    $('#save-changes-modal').click(function () {
        const newName = $('#edit-model').val();
        const Id = $('#edit-value-id').val();
        if (newName.length > 1  && newName.length < 50) {
            $.ajax({
                type: 'POST',
                url: '/AdminPanel/UpdateModel/',
                headers: {
                    RequestVerificationToken: $('#RequestVerificationToken').val()
                },
                data: { "modelId": Id, "newName": newName},
                success: function () {
                    location.reload();
                }
            });
        }
    });

    // Models
    // Delete 
    $('button[name="delete-model"]').on('click', function () {
        const Id = $(this).val();
        $.ajax({
            type: 'POST',
            url: '/AdminPanel/DeleteModel/',
            headers: {
                RequestVerificationToken: $('#RequestVerificationToken').val()
            },
            data: { "modelId": Id },
            success: function (result) {
                $('tr[name="' + Id + '"]').remove()
            },
            error: function () {
                alert("Модель має наявні товари");
            }
        });
    });
});
