let modalPerformance = new bootstrap.Modal(document.getElementById('modal-performance'), { keyboard: false, backdrop: "static" });

cboStatus.onchange = function () {
    get_info_graph_by_status();
}

function initGraph(){
    document.getElementById("titleModalPerformance").innerText = "RENDIMIENTO DE PERSONAL";
    get_info_graph_by_status();
    modalPerformance.show();
}

function get_info_graph_by_status() {
    ajax.async = false;
    ajax.serialize = true;
    ajax.parametros = {
        status: document.getElementById("cboStatus").value
    };
    ajax.send("/Profile/GetGraphByStatus", function (response) {
        let data = JSON.parse(response.data);
        let htmlTable = "";

        if (data.length > 0) {
            data.map(function (item, index) {
                htmlTable += "<tr>";
                htmlTable += "   <td style='width:80%'>" + item.usuario + "</td>";
                htmlTable += "   <td style='width:20%' class='text-center'>" + item.total_act + "</td>";
                htmlTable += "</tr>";
            });

            dgtData(response.data);
        }
        else {
            htmlTable += "<tr>";
            htmlTable += "   <td style='width:100%' class='text-center'>No se encontro informacion</td>";
            htmlTable += "</tr>";
        }

        document.getElementById("tblLista").innerHTML = htmlTable;
    });
}

function dgtData(data) {
    let dataInfo = JSON.parse(data);
    let dgtDataEarring = [];
    let dgtDataComplete = [];

    let doughnutOptions = {
        responsive: false,
        legend: {
            display: false
        }
    };

    if (dataInfo.length > 0) {
        dgtDataEarring = {
            labels: ["Total"],
            datasets: [{
                data: [dataInfo[0].total_act_pend],
                backgroundColor: ["#E66B40"]
            }]
        };

        dgtDataComplete = {
            labels: ["Total"],
            datasets: [{
                data: [dataInfo[0].total_act_ok],
                backgroundColor: ["#28CD73"]
            }]
        };
    }
    else {
        dgtDataEarring = {
            labels: ["Total"],
            datasets: [{
                data: [0],
                backgroundColor: ["#E66B40"]
            }]
        };

        dgtDataComplete = {
            labels: ["Total"],
            datasets: [{
                data: [0],
                backgroundColor: ["#28CD73"]
            }]
        };
    }

    let ctxRev = document.getElementById("dgtDataEarring").getContext("2d");
    new Chart(ctxRev, { type: 'doughnut', data: dgtDataEarring, options: doughnutOptions });

    let ctxOK = document.getElementById("dgtDataComplete").getContext("2d");
    new Chart(ctxOK, { type: 'doughnut', data: dgtDataComplete, options: doughnutOptions });
}