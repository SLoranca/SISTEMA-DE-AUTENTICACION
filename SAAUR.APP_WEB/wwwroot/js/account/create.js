function create_account() {
    if (!$("#Form").valid()) { tools.mensajeError("Todos los campos son obligatirios"); return false; }
    ajax.async = true;
    ajax.serialize = true;
    ajax.parametros = {
        name: document.getElementById("txtName").value,
        p_last_name: document.getElementById("txtPLastName").value,
        m_last_name: document.getElementById("txtMLastName").value,
        email: document.getElementById("txtEmail").value,
        password: document.getElementById("txtPassword").value,
        hashPass:"-",
        salt:"-"
    };
    ajax.send("/Account/Create", function (response) {
        if (response.status === "OK") {
            window.location = response.data;
        }
        else {
            tools.mensajeError(response.message);
        }
    });
}

$(function () {
    $("#Form").validate({
        rules: {
            name: { required: true },
            p_last_name: { required: true },
            m_last__name: { required: true},
            email: { required: true, email: true },
            password: { required: true }
        },
        messages: {
            name: { required: "*Este campo es obligatorio" },
            p_last_name: { required: "*Este campo es obligatorio" },
            m_last__name: { required: "*Este campo es obligatorio" },
            email: { required: "*Este campo es obligatorio", email: "Debe proporcionar un correo valido" },
            password: { required: "*Este campo es obligatorio" }
        }
    });
});