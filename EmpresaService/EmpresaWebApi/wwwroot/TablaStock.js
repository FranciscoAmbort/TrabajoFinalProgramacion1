// Función para llenar la tabla de usuarios



function llenarTablaStock() {
    fetch(`https://localhost:7026/Producto`)
        .then(response => response.json())
        .then(data => {
            
            var tablaStock = document.getElementById("tablaStock");
            var productosFiltrados = data.filter(x => x.stock < x.stockMinimo)

            productosFiltrados.forEach(producto => {
                var fila = document.createElement("tr");
                fila.innerHTML = `
                    <td>${producto.nombreProducto}</td>
                    <td>${producto.marcaProducto}</td>
                    <td>${producto.precioProducto}</td>
                    <td>${producto.stock}</td>
                    <td>${producto.stockMinimo}</td>
                  
                `;
                tablaStock.appendChild(fila);
            });
        })
        .catch(error => {
            console.error('Error al obtener datos:', error);
        });
}

// Llama a la función para llenar la tabla cuando el DOM esté listo
document.addEventListener("DOMContentLoaded", llenarTablaStock());