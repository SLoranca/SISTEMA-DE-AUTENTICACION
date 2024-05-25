function recovery_account() {
    if (!$("#Form").valid()) { tools.mensajeError("Ingrese el correo electronico a recuperar"); return false; }
    ajax.async = true;
    ajax.serialize = true;
    ajax.parametros = $("#Form").serialize();
    ajax.send("/Account/Recovery", function (response) {
        if (response.status === "OK") {
            document.getElementById("txtEmail").value = "";
            tools.mensajeOK(response.message);
        }
        else {
            tools.mensajeError(response.message);
        }
    });
}

$(function () {
    $("#Form").validate({
        rules: {
            destination_email: { required: true, email: true }
        },
        messages: {
            destination_email: { required: "*Este campo es obligatorio", email: "Debe proporcionar un correo valido" }
        }
    });
});