// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//Toastr y Confirmacion para Borrado Logico de Mascotas
$('.BorrarMascota').click(function (event) {
    event.preventDefault();
    var url = $(this).attr('href');
    var estadoMascota = $(this).data('estado');

    var estadoBool = (estadoMascota.toLowerCase() === 'true');

    if (!estadoBool) {
        toastr.options.positionClass = 'toast-top-center';
        toastr.info('La mascota ya se encuentra inactiva.');
        return; 
    }

    if (confirm('¿Estás seguro de que deseas eliminar esta mascota?')) {
        toastr.options.positionClass = 'toast-top-center';
        toastr.success('La Mascota fue eliminada correctamente.');
        setTimeout(function () {
            window.location.href = url;
        }, 3000);
    }
});















