async function ConsultarNombrePersona(idIdentificacion = "Identificacion", idNombre = "NombreCompleto") {

    const identificacion = document.getElementById(idIdentificacion).value;
    document.getElementById(idNombre).value = "";

    if (identificacion.length >= 9) {
        const resp = await fetch(`https://apis.gometa.org/cedulas/${identificacion}`);

        if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
        const datos = await resp.json();

        if (datos.resultcount >= 1) {
            document.getElementById(idNombre).value = datos.nombre;
        }
    }

}