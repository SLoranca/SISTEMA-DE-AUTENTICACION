let array_modules = [];
let modalRol = new bootstrap.Modal(document.getElementById('modal-rol'), { keyboard: false, backdrop: "static" });
let modalPermissions = new bootstrap.Modal(document.getElementById('modal-permissions'), { keyboard: false, backdrop: "static" });

btnSave.onclick = function () {
    parseInt(document.getElementById("txtId").value) === 0 ? insert() : update();
}

function init() {
    document.getElementById("lblTitleHeader").innerText = _app.select.admin_modules.roles.title;
    document.getElementById("lblTitleSubHeader").innerText = _app.select.admin_modules.roles.subtitle;
    getList();
    getApps();
}

function initForm() {
    tools.resetForm("Form");
    document.getElementById("lblTitleModal").innerText = "Nuevo Rol";
    document.getElementById("txtId").value = "0";
    document.getElementById("txtRol").value = "";
    modalRol.show();
}

function getList() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/Rol/GetList", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let data_show = [];
            let actions = "";

            if (data != null) {
                data.map(function (item, index) {
                    actions = ' <button class="btn btn-sm btn-icon btn-active-light-info btn-white" data-toggle="tooltip" data-placement="bottom" onclick="addPermissions(\'' + btoa(JSON.stringify(item)) + '\');" title="Asignar permisos"><i class="fa fa-key"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-primary btn-white" data-toggle="tooltip" data-placement="bottom" onclick="edit(\'' + btoa(JSON.stringify(item)) + '\');" title="Editar"><i class="fa fa-edit"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-danger btn-white" data-toggle="tooltip" data-placement="bottom" onclick="delReg(\'' + btoa(JSON.stringify(item)) + '\');" title="Eliminar"><i class="fa fa-trash"></i></button>';

                    data_show[index] = [];
                    data_show[index][0] = item.rol;
                    data_show[index][1] = actions;
                });
            }
            dataTable.table = "#tblList";
            dataTable.buscador = "#txtSearch";
            dataTable.targets = [
                { "targets": 0, "class": "text-left", "width": "80%" },
                { "targets": 1, "class": "text-center", "width": "20%" },
            ];
            dataTable.load(data_show);
            document.getElementById("txtSearch").value = "";
        }
    });
}

function insert() {
    if (!$("#Form").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        id: 0,
        rol: document.getElementById("txtRol").value,
    };
    ajax.send("/Rol/Insert", function (response) {
        if (response.status === "OK") {
            modalRol.hide();
            getList();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function edit(data) {
    let info = JSON.parse(atob(data));
    tools.resetForm("Form");
    document.getElementById("lblTitleModal").innerText = "Actualizar Rol";
    document.getElementById("txtId").value = info.id;
    document.getElementById("txtRol").value = info.rol;
    modalRol.show();
}

function update() {
    if (!$("#Form").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        id: document.getElementById("txtId").value,
        rol: document.getElementById("txtRol").value,
    };
    ajax.send("/Rol/Update", function (response) {
        if (response.status === "OK") {
            modalRol.hide();
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
            ajax.send("/Rol/Delete", function (response) {
                if (response.status === "OK") {
                    getList();
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

//-------------CONFIGURACIÓN DE PERMISOS PARA LOS ROLES----------//
function addPermissions(data) {
    let info = JSON.parse(atob(data));
    _app.select.rol_id = info.id;
    array_modules = [];
    document.getElementById("titleModalPermission").innerText = "ASIGNAR PERMISOS AL ROL: " + info.rol;
    document.getElementById("content-module").innerHTML = _app.select.admin_modules.roles.text_modal_permissions;
    uncheckApp();
    modalPermissions.show();
}

function getApps() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/Rol/ListRolApp", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let html = "";
            
            if (data != null) {
                data.map(function (item) {
                    html += '<tr>\
                                <td style="width:80%">'+ item.nombre + '</td>\
                                <td style="width:20%" class="text-end">\
                                    <div class="form-check">\
                                        <input class="form-check-input" type="checkbox" id="check_'+ item.id + '"  onclick="checkApp(\'' + btoa(JSON.stringify(item)) + '\');"/>\
                                        <label class="form-check-label" for="check_'+ item.id + '"></label>\
                                    </div>\
                                </td>\
                            </tr>';
                });

                document.getElementById("tblAplications").innerHTML = html;
            }
        }
    });
}

function checkApp(data) {
    let info = JSON.parse(atob(data));
    uncheckApp();
    var checkbox = document.querySelector("#check_" + info.id + "");
    checkbox.checked = true;
    _app.select.app_id = info.id;
    getModules(info.id);
}

function checkModule(data) {
    let info = JSON.parse(atob(data));

    if (array_modules.findIndex(e => e.module_id === info.id) > -1) {
        array_modules.splice(array_modules.findIndex(e => e.module_id === info.id), 1);
    }
    else {
        array_modules.push({
            module_id: info.id
        });
    }
}

function getModules(app_id) {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {
        app_id: app_id
    };
    ajax.send("/Rol/ListRolModules", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let html = "";

            if (data != null) {
                data.map(function (item) {
                    html += '<div class="col-lg-4">\
                                    <div class="d-flex flex-stack bg-light p-5">\
                                        <div class="text-gray-700 fw-semibold fs-6 me-2">'+ item.titulo + '</div>\
                                        <div class="d-flex align-items-senter">\
                                            <div class="form-check">\
                                                <input class="form-check-input" type="checkbox" id="Modulecheck_'+ item.id + '" onclick="checkModule(\'' + btoa(JSON.stringify(item)) + '\');" />\
                                            </div>\
                                        </div>\
                                    </div>\
                                </div>';
                });
            }

            document.getElementById("content-module").innerHTML = html;
        }
    });
}

function insertAppRol() {
    if (array_modules.length <= 0) {
        tools.mensajeError("Debe seleccionar al menos 1 modulo para poder continuar");
        return false;
    }

    if (_app.select.rol_id === "0" || _app.select.app_id === "0") {
        tools.mensajeError("Los valores (rol_id) y/o (app_id) deben sen mayores a cero");
        return false;
    }

    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        rol_id: _app.select.rol_id,
        app_id: _app.select.app_id,
        modules: array_modules
    };
    ajax.send("/Rol/InsertRolAppModules", function (response) {
        if (response.status === "OK") {
            _app.select.app_id = 0;
            array_modules = [];
            uncheckApp();
            document.getElementById("content-module").innerHTML = _app.select.admin_modules.roles.text_modal_permissions;
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function uncheckApp() {
    let inputs = document.querySelectorAll('.form-check-input');
    for (let i = 0; i < inputs.length; i++) { inputs[i].checked = false; }
}

$(function () {
    $("#Form").validate({
        rules: {
            rol: { required: true },
        },
        messages: {
            rol: { required: "*Este campo es obligatorio" }
        }
    });
});