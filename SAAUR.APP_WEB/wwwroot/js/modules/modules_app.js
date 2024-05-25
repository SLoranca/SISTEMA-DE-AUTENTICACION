let modalModule = new bootstrap.Modal(document.getElementById('modal-modules'), { keyboard: false, backdrop: "static" });

function validId() { parseInt(document.getElementById("txtIdModule").value) === 0 ? insertModule() : updateModule(); }

function initModule() {
    document.getElementById("txtIdModule").value = "0";
    document.getElementById("txtModule").value = "";
    document.getElementById("txtAction").value = "";
    document.getElementById("txtController").value = "";
}

function addModules(data) {
    let info = JSON.parse(atob(data));
    tools.resetForm("FormModule");
    document.getElementById("lblTitleModalModules").innerText = "CREACION DE MODULOS";
    document.getElementById("lblTitleForm").innerText = "NUEVO MODULO";
    document.getElementById("lblNameApp").innerText = info.nombre;
    document.getElementById("txtIdApp").value = info.id;
    initModule();
    getListModules(info.id);
    modalModule.show();
}

function getListModules(app_id) {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {
        app_id: app_id
    };
    ajax.send("/Module/GetList", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let data_show = [];
            let actions = "";
            let status = "";

            if (data != null) {
                data.map(function (item, index) {
                    actions = ' <button class="btn btn-sm btn-icon btn-active-light-primary btn-white" data-toggle="tooltip" data-placement="bottom" onclick="editModule(\'' + btoa(JSON.stringify(item)) + '\');" title="Editar"><i class="fa fa-edit"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-success btn-white" data-toggle="tooltip" data-placement="bottom" onclick="enableModule(\'' + btoa(JSON.stringify(item)) + '\');" title="Habilitar"><i class="fa fa-check"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-danger btn-white" data-toggle="tooltip" data-placement="bottom" onclick="disablelModule(\'' + btoa(JSON.stringify(item)) + '\');" title="Deshabilitar"><i class="fa fa-times"></i></button>';

                    if (item.activo) {
                        status = "<label class='badge badge-success'>Habilitado</label>";
                    }
                    else {
                        status = "<label class='badge badge-danger'>Deshabilitado</label>";
                    }

                    data_show[index] = [];
                    data_show[index][0] = status;
                    data_show[index][1] = item.titulo;
                    data_show[index][2] = item.accion;
                    data_show[index][3] = item.controlador;
                    data_show[index][4] = actions;
                });
            }
            dataTable.table = "#tblListModule";
            dataTable.targets = [
                { "targets": 0, "class": "text-left", "width": "10%" },
                { "targets": 1, "class": "text-left", "width": "30%" },
                { "targets": 2, "class": "text-left", "width": "20%" },
                { "targets": 3, "class": "text-left", "width": "20%" },
                { "targets": 4, "class": "text-center", "width": "20%" },
            ];
            dataTable.load(data_show);
        }
    });
}

function insertModule() {
    if (!$("#FormModule").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }

    let app_id = document.getElementById("txtIdApp").value;

    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        id: 0,
        app_id: app_id,
        title: document.getElementById("txtModule").value,
        action: document.getElementById("txtAction").value,
        controller: document.getElementById("txtController").value
    };
    ajax.send("/Module/Insert", function (response) {
        if (response.status === "OK") {
            getListModules(app_id);
            initModule();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function editModule(data) {
    let info = JSON.parse(atob(data));
    document.getElementById("lblTitleForm").innerText = "EDITAR MODULO";
    document.getElementById("txtIdApp").value = info.app_id;
    document.getElementById("txtIdModule").value = info.id;
    document.getElementById("txtModule").value = info.titulo;
    document.getElementById("txtAction").value = info.accion;
    document.getElementById("txtController").value = info.controlador;
    modalModule.show();
}

function updateModule() {
    if (!$("#FormModule").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }

    let app_id = document.getElementById("txtIdApp").value;

    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        id: document.getElementById("txtIdModule").value,
        app_id: app_id,
        title: document.getElementById("txtModule").value,
        action: document.getElementById("txtAction").value,
        controller: document.getElementById("txtController").value
    };
    ajax.send("/Module/Update", function (response) {
        if (response.status === "OK") {
            getListModules(app_id);
            initModule();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function enableModule(data) {
    let info = JSON.parse(atob(data));
    swal.fire({
        title: '\xbfSeguro(a) que desea habilitar este modulo?',
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
            ajax.send("/Module/Enable", function (response) {
                if (response.status === "OK") {
                    getListModules(info.app_id);
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

function disablelModule(data) {
    let info = JSON.parse(atob(data));
    swal.fire({
        title: '\xbfSeguro(a) que desea deshabilitar este modulo?',
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
            ajax.send("/Module/Disable", function (response) {
                if (response.status === "OK") {
                    getListModules(info.app_id);
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

$(function () {
    $("#FormModule").validate({
        rules: {
            module: { required: true },
            action: { required: true },
            controller: { required: true }
        },
        messages: {
            module: { required: "*Este campo es obligatorio" },
            action: { required: "*Este campo es obligatorio" },
            controller: { required: "*Este campo es obligatorio" }
        }
    });
});