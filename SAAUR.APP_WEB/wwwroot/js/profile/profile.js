let modalPhoto = new bootstrap.Modal(document.getElementById('modal-photo'), { keyboard: false, backdrop: "static" });
let _twoFactor = false;

chkTwoFactor.onchange =function(){
    chkTwoFactor.checked ? _twoFactor = true : _twoFactor = false;
    twoFactor();
}

function init() {
    document.getElementById("lblTitleHeader").innerText = _app.select.admin_modules.cuenta.title;
    document.getElementById("lblTitleSubHeader").innerText = _app.select.admin_modules.cuenta.subtitle;
    parseInt(atob(localStorage.getItem("data-tf"))) == 1 ? document.getElementById("chkTwoFactor").checked = true : document.getElementById("chkTwoFactor").checked = false;
}

function resetEmail(){
    closePassword();
    tools.resetForm("FormEmail");
    document.getElementById("kt_signin_email").classList.add("d-none");
    document.getElementById("kt_signin_email_button").classList.add("d-none");
    document.getElementById("kt_signin_email_edit").classList.remove("d-none");
}

function resetPassword(){
    closeEmail();
    tools.resetForm("FormPassword");
    document.getElementById("kt_signin_password").classList.add("d-none");
    document.getElementById("kt_signin_password_button").classList.add("d-none");
    document.getElementById("kt_signin_password_edit").classList.remove("d-none");
}

function closeEmail() {
    document.getElementById("txtNewEmail").value = "";
    document.getElementById("txtConfirmPassword2").value = "";
    document.getElementById("kt_signin_email_edit").classList.add("d-none");
    document.getElementById("kt_signin_email").classList.remove("d-none");
    document.getElementById("kt_signin_email_button").classList.remove("d-none");
}

function closePassword() {
    document.getElementById("txtPassword").value = "";
    document.getElementById("txtNewPassword").value = "";
    document.getElementById("txtConfirmPassword").value = "";
    document.getElementById("kt_signin_password_edit").classList.add("d-none");
    document.getElementById("kt_signin_password").classList.remove("d-none");
    document.getElementById("kt_signin_password_button").classList.remove("d-none");
} 

function update_password() {
    if (!$("#FormPassword").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    swal.fire({
        title: '\xbfSeguro(a) que desea actualizar la contraseña?',
        showDenyButton: true,
        confirmButtonText: 'SI',
        denyButtonText: 'NO'
    }).then((result) => {
        if (result.isConfirmed) {
            ajax.async = false;
            ajax.serialize = true;
            ajax.parametros = {
                user_id: 0,
                password: document.getElementById("txtPassword").value,
                new_password: document.getElementById("txtNewPassword").value,
                confirm_password: document.getElementById("txtConfirmPassword").value,
                hashPass: "-",
                salt: "-"
            };
            ajax.send("/Profile/UpdPassword", function (response) {
                if (response.status === "OK") {
                    document.getElementById("txtPassword").value = "";
                    document.getElementById("txtNewPassword").value = "";
                    document.getElementById("txtConfirmPassword").value = "";
                    closePassword();
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

function update_email() {
    if (!$("#FormEmail").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    swal.fire({
        title: '\xbfEl correo electronico esta correcto?',
        showDenyButton: true,
        confirmButtonText: 'SI',
        denyButtonText: 'NO'
    }).then((result) => {
        if (result.isConfirmed) {
            ajax.async = false;
            ajax.serialize = true;
            ajax.parametros = {
                user_id: 0,
                new_email: document.getElementById("txtNewEmail").value,
                confirm_password: document.getElementById("txtConfirmPassword2").value,
            };
            ajax.send("/Profile/UpdEmail", function (response) {
                if (response.status === "OK") {
                    document.getElementById("txtNewEmail").value = "";
                    document.getElementById("txtConfirmPassword2").value = "";
                    closeEmail();
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

function uploadImg() {
    if (!$("#FormPhoto").valid()) { tools.mensajeError("Debe seleccionar una imagen."); return false; }
    let formdata = new FormData();
    let xhr = new XMLHttpRequest();
    let fileInput = document.getElementById('txtPhoto');
    formdata.append("file", fileInput.files[0]);
    formdata.append("user_id", _app.select.user_id);
    xhr.responseType = 'json';
    xhr.open("POST", "/UploadImg/UploadImg", true);
    xhr.send(formdata);
    xhr.onreadystatechange = function (r) {
        if (xhr.readyState == 4 && xhr.status == 200) {
            if (xhr.response.status === "OK") {
                let info = JSON.parse(xhr.response.data);
                document.getElementById("imgProfile").src = "";
                document.getElementById("imgProfile").src = _app.srcImg + "img" + "/" + _app.select.user_id + "/" + info.data;
                localStorage.setItem("data-photo", info.data);
                modalPhoto.hide();
                tools.mensajeOK(info.message);
            }
            else {
                tools.mensajeError(info.message);
            }
        }
    }
}

function modalChangePhoto() {
    document.getElementById("lblTitleModalPhoto").innerText = "Cambiar foto de perfil";
    document.getElementById("txtPhoto").value = "";
    tools.resetForm("FormPhoto");
    modalPhoto.show();
}

function twoFactor(){
    let url = "";
    let title = "";
    _twoFactor ? url="/Profile/EnableTwoFactor" :  url="/Profile/DIsableTwoFactor";
    _twoFactor ? title="\xbf Desea hablitar la autenticación en 2 pasos?" :  title="\xbf Desea deshablitar la autenticación en 2 pasos?";
    swal.fire({
        title: title,
        showDenyButton: true,
        confirmButtonText: 'SI',
        denyButtonText: 'NO'
    }).then((result) => {
        if (result.isConfirmed) {
            ajax.async = false;
            ajax.serialize = true;
            ajax.send(url, function (response) {
                let info = JSON.parse(response.data);
                if (response.status === "OK") {
                    localStorage.setItem("data-tf", btoa(info.data));
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
        else{
            parseInt(atob(localStorage.getItem("data-tf"))) == 1 ? document.getElementById("chkTwoFactor").checked = true : document.getElementById("chkTwoFactor").checked = false;
        }
    });
}

$(function () {
    $("#FormPassword").validate({
        rules: {
            password: { required: true },
            newPassword: { required: true },
            confirmPassword: { required: true }
        },
        messages: {
            password: { required: "*Este campo es obligatorio" },
            newPassword: { required: "*Este campo es obligatorio" },
            confirmPassword: { required: "*Este campo es obligatorio" }
        }
    });

    $("#FormEmail").validate({
        rules: {
            emailAddress: { required: true },
            confirmPassword2: { required: true }
        },
        messages: {
            emailAddress: { required: "*Este campo es obligatorio" },
            confirmPassword2: { required: "*Este campo es obligatorio" }
        }
    });

    $("#FormPhoto").validate({
        rules: {
            photo: { required: true }
        },
        messages: {
            photo: { required: "*Este campo es obligatorio" }
        }
    });
});