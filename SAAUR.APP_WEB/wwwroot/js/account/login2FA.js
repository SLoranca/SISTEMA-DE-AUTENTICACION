function validCode() {
    if (!$("#frmLogin").valid()) { tools.mensajeError("Ingrese el codigo enviado a su correo."); return false; }
    ajax.async = true;
    ajax.serialize = true;
    ajax.parametros = {
        email: localStorage.getItem("data-email"),
        code: document.getElementById("txtCode").value
    };
    ajax.send("/Account/Authentication2FAValid", function (response) {
        if (response.status === "OK") {
            localStorage.removeItem("data-email");
            localStorage.setItem("data-apps", btoa(response.json_apps));
            localStorage.setItem("data-modules", btoa(response.json_modules));
            localStorage.setItem("data-photo", "");
            localStorage.setItem("data-tf", btoa(response.twofactor));
            localStorage.setItem("data-token", response.token);
            window.location = response.url;
        }
        else {
            tools.mensajeError(response.message);
        }
    });
}

function sendCode() {
    let email = localStorage.getItem("data-email");

    if(email===null){
        tools.mensajeError("No se encontro email para poder enviar el codigo."); 
        return false;
    }

    ajax.async = true;
    ajax.serialize = true;
    ajax.parametros = {
        email: email
    };
    ajax.send("/Account/GenerateCode", function (response) {
        if (response.status === "OK") {
            tools.mensajeOK(response.message);
        }
        else {
            tools.mensajeError(response.message);
        }
    });
}


$(function () {
    $("#frmLogin").validate({
        rules: {
            code: { required: true }
        },
        messages: {
            code: { required: "*Este campo es obligatorio" }
        }
    });
});