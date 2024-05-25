let modalApp = new bootstrap.Modal(document.getElementById('modal-app'), { keyboard: false, backdrop: "static" });

btnSaveReg.onclick = function () { parseInt(document.getElementById("txtId").value) === 0 ? insert() : update(); }

function init() {
    document.getElementById("lblTitleHeader").innerText = _app.select.admin_modules.aplications.title;
    document.getElementById("lblTitleSubHeader").innerText = _app.select.admin_modules.aplications.subtitle;
    getList();
}

function initForm() {
    tools.resetForm("Form");
    document.getElementById("lblTitleModal").innerText = "Nueva Aplicacion";
    document.getElementById("txtId").value = "0";
    document.getElementById("txtName").value = "";
    document.getElementById("txtLink").value = "";
    document.getElementById("txtDescription").value = "";
    modalApp.show();
}

function getList() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/App/GetList", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let data_show = [];
            let actions = "";

            if (data != null) {
                data.map(function (item, index) {
                    actions = ' <button class="btn btn-sm btn-icon btn-active-light-info btn-white" data-toggle="tooltip" data-placement="bottom" onclick="addModules(\'' + btoa(JSON.stringify(item)) + '\');" title="Asignar modulo"><i class="fa fa-network-wired"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-primary btn-white" data-toggle="tooltip" data-placement="bottom" onclick="edit(\'' + btoa(JSON.stringify(item)) + '\');" title="Editar"><i class="fa fa-edit"></i></button>';
                    actions += ' <button class="btn btn-sm btn-icon btn-active-light-danger btn-white" data-toggle="tooltip" data-placement="bottom" onclick="delReg(\'' + btoa(JSON.stringify(item)) + '\');" title="Eliminar"><i class="fa fa-trash"></i></button>';

                    data_show[index] = [];
                    data_show[index][0] = "<strong>Nombre de la aplicacion: </strong>" + item.nombre + "<br/>" + item.descripcion + "<br/>" + "<strong>Link: </strong>" + item.vinculo;
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
        name: document.getElementById("txtName").value,
        description: document.getElementById("txtDescription").value,
        link: document.getElementById("txtLink").value
    };
    ajax.send("/App/Insert", function (response) {
        if (response.status === "OK") {
            modalApp.hide();
            getList();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function edit(data) {
    let info = JSON.parse(atob(data));
    tools.resetForm("Form");
    document.getElementById("lblTitleModal").innerText = "Editar Aplicacion";
    document.getElementById("txtId").value = info.id;
    document.getElementById("txtName").value = info.nombre;
    document.getElementById("txtDescription").value = info.descripcion;
    document.getElementById("txtLink").value = info.vinculo;
    modalApp.show();
}

function update() {
    if (!$("#Form").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        id: document.getElementById("txtId").value,
        name: document.getElementById("txtName").value,
        description: document.getElementById("txtDescription").value,
        link: document.getElementById("txtLink").value
    };
    ajax.send("/App/Update", function (response) {
        if (response.status === "OK") {
            modalApp.hide();
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
            ajax.send("/App/Delete", function (response) {
                if (response.status === "OK") {
                    getList();
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

$(function () {
    $("#Form").validate({
        rules: {
            name: { required: true },
            link: { required: true },
            desc: { required: true }
        },
        messages: {
            name: { required: "*Este campo es obligatorio" },
            link: { required: "*Este campo es obligatorio" },
            desc: { required: "*Este campo es obligatorio" }
        }
    });
});