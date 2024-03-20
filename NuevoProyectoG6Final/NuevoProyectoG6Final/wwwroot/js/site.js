// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#BorrarMascota').click(function (event) {
    event.preventDefault();
    var url = $(this).attr('href');
    if (confirm('¿Estás seguro de que deseas eliminar esta mascota?')) {

        window.location.href = url;
        /*alert('Eliminado Correctamente');*/
  }
});









