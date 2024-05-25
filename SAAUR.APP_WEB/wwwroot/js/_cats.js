function loadModulos() {
	if (localStorage.getItem("data-modules") != null) {
		let cats = JSON.parse(atob(localStorage.getItem("data-modules")));
		let htmlMenu = ""; htmlSubMenu = "";
		if (cats != null) {
			cats.map(function (item) {
				if (item.etiqueta === "Menu") {
					htmlMenu += '<div class="menu-item mb-1">\
								<a class="menu-link" href = "/' + item.controlador + "/" + item.accion + '">\
									<span class="menu-icon">'+ item.icono + '</span>\
									<span class="menu-title">'+ item.titulo + '</span>\
								</a>\
							</div>';
				}

				if (item.etiqueta === "SubMenu") {
					htmlSubMenu += '<div class="menu-item">\
									<a class="menu-link" href = "/' + item.controlador + "/" + item.accion + '">\
										<span class="menu-bullet">\
											<span class="bullet bullet-dot"></span>\
										</span>\
										<span class="menu-title">' + item.titulo + '</span>\
									</a >\
								</div>';

					document.getElementById("content-submenu").classList.remove("d-none");
				}
			});

			document.getElementById("menu-modulos").innerHTML = htmlMenu;
			document.getElementById("sub-menu-catalogos").innerHTML = htmlSubMenu;
		}
		else {
			htmlMenu = '<div class="d-flex align-items-center bg-light-danger rounded p-5 mb-7">\
							<span class="svg-icon text-danger me-5">\
								<i class="ki-duotone ki-message-text-2 fs-1 text-danger"><span class="path1"></span><span class="path2"></span><span class="path3"></span></i>\
							</span>\
							<div class="flex-grow-1 me-2">\
								<span class="fw-bold text-gray-800 text-primary fs-6">No se encontro informacion</span>\
							</div>\
						</div>';

			document.getElementById("content-menu-modulos").innerHTML = htmlMenu;
		}
	}
}

function loadApps() {
	if (localStorage.getItem("data-apps") != null) {
		let cats = JSON.parse(atob(localStorage.getItem("data-apps")));
		let html = "";
		if (cats != null) {
			cats.map(function (item) {
				html += '<div class="col-lg-4 mb-3">\
							<div class="bg-app bg-light-info h-100 p-5" id = "kt_docs_swapper_parent_2" >\
								<div class="fs-3 fw-bolder">'+ item.nombre + '</div>\
								<span>'+ item.descripcion +'</span>\
								<br/>\
								<a href='+item.vinculo+' target="_blank">Abrir aplicacion</a>\
							</div>\
						</div > ';
			});
		}
		else {
			html = '<div class="notice d-flex bg-light-danger rounded border-danger border border-dashed mb-9 p-6">\
						<div class="d-flex flex-stack flex-grow-1 ">\
							<div class=" fw-semibold">\
								<div class="fs-6 text-gray-700 "><i class="fas fa-times text-danger fs-4"></i> NO SE ENCONTRARON APLICACIONES ASIGNADAS A ESTE PERFIL.</div>\
							</div>\
						</div>\
					</div>';
		}

		document.getElementById("content-apps").innerHTML = html;
	}
}