
document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Complete el Formulario")
});

document.getElementById('FormularioAsignarViajes').addEventListener('submit', function (event) {
    event.preventDefault(); // Prevenir el envío del formulario normal



    // Crear un objeto con los datos
    const datos = {
        FechaDeEntregaDesde: document.getElementById('FechaDeEntregaDesde').value,
        FechaEntregaHasta: document.getElementById('FechaEntregaHasta').value,
    };

    // Enviar los datos a una API REST (sustituye la URL por la de tu API)
    fetch(`https://localhost:7026/api/Viajes`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(datos)
    })
        .then(response => response.text())
        .then(data => {
            // Manejar la respuesta de la API aquí
            console.log('Respuesta de la API:', data);
            alert('Registro exitoso');
        })
        .catch(error => {
            // Manejar errores aquí
            console.error('Error al enviar los datos:', error);
            alert('Hubo un error al procesar el registro');
        });
});