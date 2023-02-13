let localDay;

$(document).ready(function () {
    $('#tab-departements').addClass("active")

    $('#DepartementsTable').DataTable({
        "ajax": {
            "url": urlBackend + "/departement",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data",
            "headers": {
                'Authorization': 'Bearer ' + sessionStorage.getItem("token")
            },
            "error": (e) => {
                document.querySelector("#DepartementsTable > tbody > tr > td").innerHTML = "Data Not Available";
            },
        },
        "columns": [
            {
                "data": null,
                "className": "text-center",
                "render": function (data, type, full, meta) {
                    return meta.row + 1;
                }, "width": "1%"
            },
            { "data": "name" },
            {
                "data": "manager_Id",
            },
            {
                "data": "id",
                "className": "text-center",
                "render": function (data) {
                    return `
                    <button class="btn btn-warning" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="GetById('${data}')">
                        <i class="fa fa-pen"></i>
                    </button > &nbsp;
                    <button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete('${data}')">
                        <i class="fa fa-trash">
                    </i></button >`
                }, "width": "17%"
            }
        ],
        "width": "100%"
    });
});

function allEmployees() {
    $.ajax({
        "url": urlBackend + "/employee",
        "type": "GET",
        "datatype": "json",
        "dataSrc": "data",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var obj = result.data
            $("#InputHOD").html("");
            $("#InputHOD").removeAttr("disabled");
            $("#InputHOD").append(`<option value="" selected disabled>Choose The Manager</option>`)
            for (let i = 0; i < obj.length; i++) {
                console.log(obj[i].firstName + ": " + obj[i].nik)
                
                if (obj[i].manager_Id != obj[i].nik) {
                    $("#InputHOD").append(`<option value="${obj[i].nik}">${obj[i].firstName} ${obj[i].lastName}</option>`)
                }
                $("#InputHOD").append(`<option value="${obj[i].nik}" disabled hidden>${obj[i].firstName} ${obj[i].lastName}</option>`)
            }
        },
        "error": (e) => {
            $("#InputHOD").html("");
            $("#InputHOD").attr("disabled", "disabled");
            $("#InputHOD").append(`<option value="" selected disabled>Data not available</option>`)
        }
    })

}

$("#ModalCreate").click(() => {
    allEmployees()

    $("#buttonSubmit").attr("onclick", "Create()");
    $("#buttonSubmit").attr("class", "btn btn-success");
    $("#buttonSubmit").html("Create");
    $("#CreateModalLabel").html("Create New Departement");

    $("#InputIdDepartement").val("");
    $("#InputDepartementName").val("");

    $("#InputHOD").html("");
    $("#InputHOD").attr("disabled", "disabled");

    $("#BodyModal > form > div:nth-child(3)").hide()
    $("#InputDepartementName").attr("placeholder", "Input Departement Name");
})

function Create() {
    let validateForm = true;

    if (
        $("#InputDepartementName").val() == "" ||
        $("#InputHOD").val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }

    if (validateForm) {
        var Departement = {};
        Departement.Name = $("#InputDepartementName").val();
        Departement.manager_Id = parseInt($("#InputHOD").val());

        console.log(Departement)

        $.ajax({
            "type": "POST",
            "url": urlBackend + "/departement",
            "data": JSON.stringify(Departement),
            "contentType": "application/json;charset=utf-8",
            "success": (result) => {
                if (result.status == 200 || result.status == 201) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Data successfully created',
                    })

                    $('#DepartementsTable').DataTable().ajax.reload();
                    $('#CreateModal').modal("hide");
                } else {
                    alert("Data failed to create")
                }
                $('#DepartementsTable').DataTable().ajax.reload();
                $('#CreateModal').modal("hide");
            },
            "error": (result) => {
                if (result.status == 400 || result.status == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: result.responseJSON.message,
                    })
                }
            },
        })
    }
}

function GetById(id) {
    // debugger;

    $("#BodyModal > form > div:nth-child(3)").show()

    $.ajax({
        "type": "GET",
        "url": urlBackend + "/departement/" + id,
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (result) {
            var obj = result.data;

            console.log(obj)

            allEmployees()

            // debugger;
            $('#InputIdDepartement').val(obj.id);
            $('#InputDepartementName').val(obj.name);
            $('#InputHOD').val(obj.manager_Id);

            $("#buttonSubmit").attr("onclick", "Update()");
            $("#buttonSubmit").attr("class", "btn btn-warning");
            $("#buttonSubmit").html("Update");
            $("#CreateModalLabel").html("Update New Departement");
            $('#CreateModal').modal("show");
        },
        error: function (errormesage) {
            swal("Data failed to input!", "You clicked the button!", "error");
        }
    })
}

function Update() {
    let validateForm = true;

    if (
        $("#InputIdDepartement").val() == "" ||
        $("#InputDepartementName").val() == "" ||
        $("#InputHOD").val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }

    if (validateForm) {
        var Departement = {};
        Departement.Id = $("#InputIdDepartement").val();
        Departement.Name = $("#InputDepartementName").val();
        Departement.manager_Id = parseInt($("#InputHOD").val());
    
        console.log(Departement)
    
        $.ajax({
            "url": urlBackend + "/departement/UpdateSpecificDepartement",
            "type": "PUT",
            "data": JSON.stringify(Departement),
            "contentType": "application/json; charset=utf-8",
            "success": (result) => {
                if (result.status == 200 || result.status == 201) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Data successfully updated',
                    })
                    allEmployees()
                    $('#DepartementsTable').DataTable().ajax.reload();
                    $('#CreateModal').modal("hide");
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: 'Data failed to update',
                    })
                }
            },
            "error": (result) => {
                if (result.status == 400 || result.status == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: result.responseJSON.message,
                    })
                }
            },
        })
    }
}

function ConfirmDelete(id) {
    $.ajax({
        "url": urlBackend + "/departement/" + id,
        "type": "GET",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var obj = result.data;

            Swal.fire({
                title: 'Delete data',
                icon: 'info',
                html: `Are you sure want to delete data with departement <b>${obj.name}</b>?`,
                showCloseButton: true,
                showConfirmButton: false,
                showDenyButton: true,
                showCancelButton: true,
                denyButtonText: `<span onclick="Delete('${obj.id}')">Delete</span>`,
                cancelButtonText: `Close`,
            })
        },
        "error": (e) => {
            alert(e.responseText)
        }
    })
}

function Delete(id) {
    $.ajax({
        "url": urlBackend + "/departement/DeleteSpecificDepartement/" + id,
        "type": "DELETE",
        "dataType": "json",
        "success": (result) => {
            if (result.status == 200 || result.status == 201) {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Data successfully deleted',
                })
                $('#DepartementsTable').DataTable().ajax.reload();
            }
        },
        "error": (result) => {
            if (result.status == 400 || result.status == 500) {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed',
                    text: result.responseJSON.message,
                })
            }
        },
    })
}