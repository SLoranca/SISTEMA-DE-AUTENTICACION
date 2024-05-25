btnLogin.onclick = function (event) { authentication(); }

document.querySelector("#frmLogin").addEventListener('keypress', function (e) {
    if (e.keyCode === 13) {
        authentication();
    }
});

function init(){
    localStorage.removeItem("data-email");
    localStorage.removeItem("data-modules");
    localStorage.removeItem("data-photo");
    localStorage.removeItem("data-token");
    localStorage.removeItem("data-tf");
    localStorage.removeItem("data-apps");
}

function authentication() {
    if (!$("#frmLogin").valid()) { tools.mensajeError("Ingrese sus credenciales"); return false; }
    ajax.async = true;
    ajax.serialize = true;
    ajax.parametros = $("#frmLogin").serialize();
    ajax.send("/Account/Authentication", function (response) {
        if (response.status === "OK") {
            if(response.twofactor != 1){
                localStorage.setItem("data-apps", btoa(response.json_apps));
                localStorage.setItem("data-modules", btoa(response.json_modules));
                localStorage.setItem("data-photo", "");
                localStorage.setItem("data-tf", btoa(response.twofactor));
                localStorage.setItem("data-token", response.token);
            }
            else{
                localStorage.setItem("data-email", response.email);
            }
            
            window.location = response.url;
        }
        else {
            tools.mensajeError(response.message);
        }
    });
}

$(function () {
    $("#frmLogin").validate({
        rules: {
            email: { required: true, email: true },
            pwd: { required: true }
        },
        messages: {
            email: { required: "*Este campo es obligatorio", email: "Debe proporcionar un correo valido" },
            pwd: { required: "*Este campo es obligatorio" }
        }
    });
});