async function ConsultarNombrePersona() {

    const identificacion = document.getElementById("Identificacion").value;
    document.getElementById("NombreCompleto").value = "";

    if (identificacion.length >= 9) {
        const resp = await fetch(`https://apis.gometa.org/cedulas/${identificacion}`);

        if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
        const datos = await resp.json();

        if (datos.resultcount >= 1) {
            document.getElementById("NombreCompleto").value = datos.nombre;
        }
    }

}