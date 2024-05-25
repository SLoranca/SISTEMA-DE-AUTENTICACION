let items = [];
let first_item = [];
let modalItem = new bootstrap.Modal(document.getElementById('modal-item'), { keyboard: false, backdrop: "static" });
let modalActivity = new bootstrap.Modal(document.getElementById('modal-activity'), { keyboard: false, backdrop: "static" });

function initFormItem() {
    document.getElementById("lblTitleModal").innerText = "Nuevo Item";
    document.getElementById("txtItem").value = "";
    tools.resetForm("FormItem");
    modalItem.show();
}

function initFormActivity() {
    if (parseInt(_app.select.item_id) <= 0) {
        tools.mensajeError("Debe Seleccionar un item, para poder agregar una actividad.");
        return false;
    }
    document.getElementById("lblTitleModal2").innerText = "Nueva actividad";
    tools.resetForm("FormActivity");
    modalActivity.show();
}

function initActivity(rol) {
    if (rol === "SUPER ADMIN") {
        get_items();
        loadUsers();
        _app.select.item_id = 0;
        document.getElementById("card-toolbar").classList.remove("d-none");
    }
    else {
        get_activities_by_users();
    }
}

function get_activities_by_users() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/Activity/GetbyIdUser", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            document.getElementById("content-actividades").innerHTML = paintActivities(data, 'complete');
        }
    });
}

function get_activities(item_id) {
    _app.select.item_id = item_id;
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {
        item_id: _app.select.item_id
    };
    ajax.send("/Activity/GetbyId", function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            document.getElementById("kt_timeline_widget_3_tab_content_" + item_id + "").innerHTML = paintActivities(data, 'delete');
            addClassActiveByAct(_app.select.item_id);
        }
    });
}

function paintActivities(data, button) {
    let htmlAct = "";
    let htmlButton = "";
    let htmlBg = "";
    if (data.length > 0) {
        data.map(function (item, index) {
            item.status.trim() === "PEN" ? htmlBg = 'bg-warning' : htmlBg = 'bg-success';
            htmlAct += '<div class="d-flex align-items-center mb-6">\
                                <span data-kt-element="bullet" class="bullet bullet-vertical d-flex align-items-center min-h-70px mh-100 me-4 '+ htmlBg + '" ></span>\
                                <div class="flex-grow-1 me-5">\
                                    <div class="text-gray-800 fw-semibold fs-2">\
                                        '+ moment(item.fecha_asignada).format("DD-MM-YYYY") + '\
                                    </div>\
                                    <div class="text-gray-700 fw-semibold fs-6">\
                                        '+ item.titulo_act + '\
                                    </div>\
                                    <div class="text-gray-400 fw-semibold fs-7">\
                                        '+ item.descripcion_act + '\
                                    </div>\
                                      <div class="fw-semibold text-end">';
            if (button === "complete") {
                if (item.status.trim() === "PEN") {
                    htmlButton = '<button type="button" class="btn btn-sm btn-white btn-active-light-success" onclick="complete_act(\'' + btoa(JSON.stringify(item.id)) + '\');">\
                                                            <i class="fa fa-check" ></i > Actividad Completada\
                                                      </button>';
                }
                else {
                    htmlButton = '';
                }
            }
            else {
                if (item.status.trim() === "PEN") {
                    htmlButton = '<button type="button" class="btn btn-sm btn-white btn-active-light-danger" onclick="delete_act(\'' + btoa(JSON.stringify(item.id)) + '\');">\
                                                            <i class="fa fa-trash" ></i> Eliminar Actividad\
                                                      </button>';
                }
                else {
                    htmlButton = '';
                }
            }
            htmlAct += htmlButton;

            htmlAct += '              </div >\
                                </div>\
                            </div>';
        });
    }
    else {
        htmlAct = '<div class="notice d-flex bg-light-danger rounded border-danger border border-dashed mb-9 p-6">\
						<div class="d-flex flex-stack flex-grow-1 ">\
							<div class=" fw-semibold">\
								<div class="text-gray-700 "><i class="fas fa-times text-danger fs-4"></i> SIN ACTIVIDADES ASIGNADAS</div>\
							</div>\
						</div>\
					</div>';
    }
    return htmlAct;
}

function get_items() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/Item/GetList", function (response) {
        if (response.status === "OK") {
            let htmlItems = "";
            let htmlAct = "";
            let data = JSON.parse(response.data);
            first_item = [...data].shift();
            if (data.length > 0) {
                data.map(function (item, index) {
                    items.push({ item });
                    htmlItems += '<li class="nav-item p-0 ms-0 min-w-100px" role="presentation">\
                                        <a id="item-'+ item.id + '" onclick="get_activities(\'' + item.id + '\');" class="nav-item-activities nav-link btn d-flex flex-column flex-centeR min-w-45px py-4 px-3 btn-active-danger" data-bs-toggle="tab" href="#kt_timeline_widget_3_tab_content_' + item.id + '" role = "tab">\
                                            <span class="fs-9 fw-semibold">'+ item.titulo + '</span>\
                                            <span class="fs-9 fw-bold">'+ moment(item.fecha_creacion).format("DD-MM-YYYY") +'</span>\
                                        </a>\
                                </li>';
                    htmlAct += '<div class="tab-pane-activities tab-pane fade" id="kt_timeline_widget_3_tab_content_' + item.id + '" role="tabpanel"></div>';
                    get_activities(first_item.id);
                });

                document.getElementById("content-items").innerHTML = htmlItems;
                document.getElementById("content-actividades").innerHTML = htmlAct;
                addClassActiveByItem(first_item.id);
            }
        }
    });
}

function insert_item() {
    if (!$("#FormItem").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        user_id: 0,
        title: document.getElementById("txtItem").value,
    };
    ajax.send("/Item/Insert", function (response) {
        if (response.status === "OK") {
            modalItem.hide();
            get_items();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function insert_act() {
    if (!$("#FormActivity").valid()) { tools.mensajeError("Todos los campos son obligatorios."); return false; }
    if (parseInt(document.getElementById("cboUser").value) <= 0) {
        tools.mensajeError("Seleccione el usuario que realizara esta actividad");
        return false;
    }

    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        item_id: _app.select.item_id,
        user_id: document.getElementById("cboUser").value,
        title_act: document.getElementById("txtTitle").value,
        desc_act: document.getElementById("txtDescription").value,
        date_assigned: document.getElementById("txtDateAssig").value,
    };
    ajax.send("/Activity/Insert", function (response) {
        if (response.status === "OK") {
            _app.select.item_id = 0;
            modalActivity.hide();
            get_items();
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

function loadUsers() {
    let sel = document.getElementById("cboUser");
    let opt = document.createElement("option");
    sel.innerHTML = "";
    opt.value = "-1";
    opt.text = "[SELECCIONA]";
    sel.add(opt, null);

    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {};
    ajax.send("/User/GetList", function (response) {
        let info = JSON.parse(response.data);
        if (response.status === "OK") {
            info?.map(function (elem) {
                let opt = document.createElement("option");
                opt.value = elem.id;
                opt.text = elem.ape_paterno + " " + elem.ape_materno + " " + elem.nombre;
                sel.add(opt, null);
            });
        }
        else { tools.mensajeError(response.message); }
    });
}

function complete_act(id) {
    let _id = JSON.parse(atob(id));
    swal.fire({
        title: '\xbfEsta actividad se ha completado correctamente?',
        showDenyButton: true,
        confirmButtonText: 'SI',
        denyButtonText: 'NO'
    }).then((result) => {
        if (result.isConfirmed) {
            ajax.async = false;
            ajax.serialize = true;
            ajax.parametros = {
                act_id: _id
            };
            ajax.send("/Activity/Complete", function (response) {
                if (response.status === "OK") {
                    get_activities_by_users();
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

function delete_act(id) {
    let _id = JSON.parse(atob(id));
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
                act_id: _id
            };
            ajax.send("/Activity/Delete", function (response) {
                if (response.status === "OK") {
                    get_items();
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

function addClassActiveByAct(item_id) {
    var element = document.querySelectorAll(".tab-pane-activities");
    for (var i = 0; i < element.length; i++) {
        element[i].classList.remove("active");
        element[i].classList.remove("show");
    }
    document.getElementById("kt_timeline_widget_3_tab_content_" + item_id + "").classList.add("active");
    document.getElementById("kt_timeline_widget_3_tab_content_" + item_id + "").classList.add("show");
}

function addClassActiveByItem(first_item) {
    var element = document.querySelectorAll(".nav-item-activities");
    for (var i = 0; i < element.length; i++) {
        element[i].classList.remove("active");
    }
    document.getElementById("item-" + first_item + "").classList.add("active");
}

$(function () {
    $("#FormActivity").validate({
        rules: {
            title_act: { required: true },
            date_assigned: { required: true },
            desc_activity: { required: true }
        },
        messages: {
            title_act: { required: "*Este campo es obligatorio" },
            date_assigned: { required: "*Este campo es obligatorio" },
            desc_activity: { required: "*Este campo es obligatorio" }
        }
    });

    $("#FormItem").validate({
        rules: {
            item: { required: true }
        },
        messages: {
            item: { required: "*Este campo es obligatorio" }
        }
    });
});
