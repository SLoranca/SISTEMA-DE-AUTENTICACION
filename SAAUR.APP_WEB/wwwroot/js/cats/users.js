let modalUser = new bootstrap.Modal(document.getElementById('modal-user'), { keyboard: false, backdrop: "static" });
let modalPhoto = new bootstrap.Modal(document.getElementById('modal-photo'), { keyboard: false, backdrop: "static" });
let modalUserEdit = new bootstrap.Modal(document.getElementById('modal-user-edit'), { keyboard: false, backdrop: "static" });

function init() {
    document.getElementById("imgUser").src = _app.srcImg + "blank.png";
    document.getElementById("lblTitleHeader").innerText = _app.select.admin_modules.users.title;
    document.getElementById("lblTitleSubHeader").innerText = _app.select.admin_modules.users.subtitle;
    getList();
    loadRoles("cboRolesEdit");
}

function initForm() {
    loadRoles("cboRolesInsert");
    tools.resetForm("FormUserInsert");
    document.getElementById("lblTitleModal").innerText = "Nuevo Usuario";
    document.getElementById("txtId").value = "0";
    document.getElementById("txtName").value = "";
    document.getElementById("txtPLastName").value = "";
    document.getElementById("txtMLastName").value = "";
    document.getElementById("txtEmail").value = "";
    document.getElementById("txtPassword").value = "";
    modalUser.show();
}

function getList() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/User/GetList", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let data_show = [];
            let actions = "";
            let srcImg = "";
            if (data != null) {
                data.map(function (item, index) {
                    item.img === "blank.png" ? srcImg = _app.srcImg + item.img : srcImg = _app.srcImg + "img" + "/" + item.id + "/" + item.img;
                    actions = ' <button class="btn btn-sm btn-icon btn-active-light-primary btn-white" data-toggle="tooltip" data-placement="bottom" onclick="initFormEdit(\'' + btoa(JSON.stringify(item)) + '\');" title="Editar"><i class="fa fa-edit"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-info btn-white" data-toggle="tooltip" data-placement="bottom" onclick="changePhoto(\'' + btoa(JSON.stringify(item)) + '\');" title="Cambiar Foto"><i class="fa fa-camera"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-danger btn-white" data-toggle="tooltip" data-placement="bottom" onclick="delReg(\'' + btoa(JSON.stringify(item)) + '\');" title="Eliminar"><i class="fa fa-trash"></i></button>';
                    
                    data_show[index] = [];
                    data_show[index][0] = "<div class='symbol symbol-40px symbol-fixed position-relative'><img src='" + srcImg + "'/></div>";
                    data_show[index][1] = item.nombre + " " + item.ape_paterno + " " + item.ape_materno + "<br/>" + "<span class='text-gray-400 fw-bold'>" + item.correo + "</span>";
                    data_show[index][2] = actions;
                });
            }
            dataTable.table = "#tblList";
            dataTable.buscador = "#txtSearch";
            dataTable.targets = [
                { "targets": 0, "class": "text-left", "width": "10%" },
                { "targets": 1, "class": "text-left", "width": "60%" },
                { "targets": 2, "class": "text-center", "width": "30%" },
            ];
            dataTable.load(data_show);
            document.getElementById("txtSearch").value = "";
        }
    });
}

function insert() {
    if (!$("#FormUserInsert").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    if (parseInt(document.getElementById("cboRolesInsert").value) <= 0) {
        tools.mensajeError("Selecccione el rol para este usuario.");
        return false;
    }
    
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        id: 0,
        id_rol: document.getElementById("cboRolesInsert").value,
        name: document.getElementById("txtName").value,
        p_last_name: document.getElementById("txtPLastName").value,
        m_last_name: document.getElementById("txtMLastName").value,
        email: document.getElementById("txtEmail").value,
        password: document.getElementById("txtPassword").value,
        hashPass: "-",
        salt: "-"
    };
    ajax.send("/User/Insert", function (response) {
        if (response.status === "OK") {
            modalUser.hide();
            getList();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function delReg(data) {
    let info = JSON.parse(atob(data));
    swal.fire({
        title: '\xbfSeguro(a) que desea eliminar este registro?. Se eliminara permanentemente.',
        showDenyButton: true,
        confirmButtonText: 'SI',
        denyButtonText: 'NO'
    }).then((result) => {
        if (result.isConfirmed) {
            ajax.async = false;
            ajax.serialize = true;
            ajax.parametros = {
                id: info.id
            };
            ajax.send("/User/Delete", function (response) {
                if (response.status === "OK") {
                    getList();
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

function loadRoles(cbo) {
    let sel = document.getElementById(cbo);
    let opt = document.createElement("option");
    sel.innerHTML = "";
    opt.value = "-1";
    opt.text = "[SELECCIONA]";
    sel.add(opt, null);

    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/Rol/GetList", function (response) {
        let info = JSON.parse(response.data);
        if (response.status === "OK") {
            info?.map(function (elem) {
                let opt = document.createElement("option");
                opt.value = elem.id;
                opt.text = elem.rol;
                sel.add(opt, null);
            });
        }
        else { tools.mensajeError(response.message); }
    });
}

function uploadImg() {
    if (!$("#FormPhoto").valid()) { tools.mensajeError("Debe seleccionar una imagen."); return false; }
    let form_data = new FormData();
    let xhr = new XMLHttpRequest();
    let fileInput = document.getElementById('txtPhoto');
    form_data.append("file", fileInput.files[0]);
    form_data.append("user_id", _app.select.user_id);
    xhr.responseType = 'json';
    xhr.open("POST", "/UploadImg/UploadImg", true);
    xhr.send(form_data);
    xhr.onreadystatechange = function (r) {
        if (xhr.readyState == 4 && xhr.status == 200) {
            if (xhr.response.status === "OK") {
                let info = JSON.parse(xhr.response.data);
                document.getElementById("lblNameUser").innerText = "[USUARIO]";
                document.getElementById("imgUser").src = _app.srcImg + "blank.png";
                getList();
                modalPhoto.hide();
                tools.mensajeOK(info.message);
            }
            else {
                tools.mensajeError(info.message);
            }
        }
    }
}

function changePhoto(data) {
    let info = JSON.parse(atob(data));
    let srcImg = "";
    document.getElementById("lblTitleModalPhoto").innerText = "Cambiar foto de perfil";
    info.img === "blank.png" ? srcImg = _app.srcImg + info.img : srcImg = _app.srcImg + "img" + "/" + info.id + "/" + info.img;
    _app.select.user_id = info.id;
    document.getElementById("lblNameUser").innerText = info.nombre + " " + info.ape_paterno + " " + info.ape_materno;
    document.getElementById("imgUser").src = srcImg;
    document.getElementById("txtPhoto").value = "";
    tools.resetForm("FormPhoto");
    modalPhoto.show();
}

//----------------------EDIT USER--------------------------------

function initFormEdit(data){
    let info = JSON.parse(atob(data));    
    document.getElementById("lblTitleModalEdit").innerText = "Editar Usuario";
    document.getElementById("lblEditUser").innerText = info.nombre +" "+info.ape_paterno+" "+info.ape_materno;
    document.getElementById("lblEditEmail").innerText = info.correo;
    document.getElementById("txtId").value = info.id;
    document.getElementById("cboRolesEdit").value = info.rol_id;
    document.getElementById("txtNameEdit").value = info.nombre;
    document.getElementById("txtPLastNameEdit").value = info.ape_paterno;
    document.getElementById("txtMLastNameEdit").value = info.ape_materno;
    document.getElementById("txtEmailEdit").value = info.correo;
    document.getElementById("txtNewPassword").value="";
    closeFormEmail();
    closeFormPassword();
    closeFormInfoGral();
    modalUserEdit.show();
}

function updInfoGral(){
    if (!$("#FormInfoGralEdit").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    if (parseInt(document.getElementById("cboRolesEdit").value) <= 0) {
        tools.mensajeError("Selecccione el rol para este usuario.");
        return false;
    }

    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        user_id: document.getElementById("txtId").value,
        id_rol: document.getElementById("cboRolesEdit").value,
        name: document.getElementById("txtNameEdit").value,
        p_last_name: document.getElementById("txtPLastNameEdit").value,
        m_last_name: document.getElementById("txtMLastNameEdit").value
    };
    ajax.send("/User/UpdGeneralInfo", function (response) {
        if (response.status === "OK") {
            getList();
            closeFormInfoGral();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function updEmail(){
    if (!$("#FormEmailEdit").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        user_id: document.getElementById("txtId").value,
        email: document.getElementById("txtEmailEdit").value
    };
    ajax.send("/User/UpdEmail", function (response) {
        if (response.status === "OK") {
            getList();
            closeFormEmail();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function updPassword(){
    if (!$("#FrmPassword").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        user_id: document.getElementById("txtId").value,
        password: document.getElementById("txtNewPassword").value,
        hashPass: "-",
        salt: "-"
    };
    ajax.send("/User/UpdPassword", function (response) {
        if (response.status === "OK") {
            getList();
            closeFormPassword();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function openFormInfoGral(){
    closeFormEmail();
    closeFormPassword();
    document.getElementById("content-info-gral-label").classList.add("d-none");
    document.getElementById("content-info-gral-btn").classList.add("d-none");
    document.getElementById("content-info-gral-form").classList.remove("d-none");
}

function openFormEmail(){
    closeFormInfoGral();
    closeFormPassword();
    document.getElementById("content-email-label").classList.add("d-none");
    document.getElementById("content-email-btn").classList.add("d-none");
    document.getElementById("content-email-form").classList.remove("d-none");
}

function openFormPassword(){
    closeFormEmail();
    closeFormInfoGral();
    document.getElementById("content-password-label").classList.add("d-none");
    document.getElementById("content-password-btn").classList.add("d-none");
    document.getElementById("content-password-form").classList.remove("d-none");
}

function closeFormInfoGral(){
    document.getElementById("content-info-gral-form").classList.add("d-none");
    document.getElementById("content-info-gral-label").classList.remove("d-none");
    document.getElementById("content-info-gral-btn").classList.remove("d-none");
}

function closeFormEmail(){
    document.getElementById("content-email-form").classList.add("d-none");
    document.getElementById("content-email-label").classList.remove("d-none");
    document.getElementById("content-email-btn").classList.remove("d-none");
}

function closeFormPassword(){
    document.getElementById("content-password-form").classList.add("d-none");
    document.getElementById("content-password-label").classList.remove("d-none");
    document.getElementById("content-password-btn").classList.remove("d-none");
}

$(function () {
    $("#FormUserInsert").validate({
        rules: {
            name: { required: true },
            p_last_name: { required: true },
            m_last_name: { required: true },
            email: { required: true, email: true },
            password: { required: true }
        },
        messages: {
            name: { required: "*Este campo es obligatorio" },
            p_last_name: { required: "*Este campo es obligatorio" },
            m_last_name: { required: "*Este campo es obligatorio" },
            email: { required: "*Este campo es obligatorio", email: "Debe proporcionar un correo valido" },
            password: { required: "*Este campo es obligatorio" }
        }
    });

    $("#FormInfoGralEdit").validate({
        rules: {
            name_edit: { required: true },
            p_last_name_edit: { required: true },
            m_last_name_edit: { required: true }
        },
        messages: {
            name_edit: { required: "*Este campo es obligatorio" },
            p_last_name_edit: { required: "*Este campo es obligatorio" },
            m_last_name_edit: { required: "*Este campo es obligatorio" }
        }
    });

    $("#FormEmailEdit").validate({
        rules: {
            emailaddress: { required: true }
        },
        messages: {
            emailaddress: { required: "*Este campo es obligatorio" }
        }
    });

    $("#FrmPassword").validate({
        rules: {
            new_password: { required: true }
        },
        messages: {
            new_password: { required: "*Este campo es obligatorio" }
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