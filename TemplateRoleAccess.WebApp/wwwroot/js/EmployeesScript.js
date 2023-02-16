let localDay;

$(document).ready(function () {
    $('#tab-employees').addClass("active")

    $('#EmployeesTable').DataTable({
        "ajax": {
            "url": urlBackend + "/employee/SpecificDataEmployees",
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data",
            "error": (e) => {
                document.querySelector("#EmployeesTable > tbody > tr > td").innerHTML = "Data Not Available";
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
            {
                "data": "nik"
            },
            {
                "data": "firstName",
                "render": function (data, type, full, meta) {
                    return `${full.firstName} ${full.lastName}`;
                }
            },
            { "data": "role_Name" },
            { "data": "departement_Name" },
            { "data": "phone" },
            { "data": "email" },
            {
                "data": "nik",
                "className": "text-center",
                "render": function (data) {
                    return `
                    <button class="btn btn-warning" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="GetById('${data}')">
                        <i class="fa fa-pen"></i>
                    </button > &nbsp;
                    <button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete('${data}')">
                        <i class="fa fa-trash">
                    </i></button >`
                }, "width": "12%"
            }
        ],
        "width": "100%"
    });

    $.ajax({
        "url": urlBackend + "/departement",
        "type": "GET",
        "datatype": "json",
        "dataSrc": "data",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var obj = result.data
            $("#InputDepartement").append(`<option value="" selected disabled>Choose The Departement</option>`)
            for (let i = 0; i < obj.length; i++) {
                $("#InputDepartement").append(`<option value="${obj[i].id}">${obj[i].name}</option>`)
            }
        },
        "error": (e) => {
            $("#InputDepartement").append(`<option value="" disabled>Data Not Available</option>`)
        }
    })
});

$("#ModalCreate").click(() => {
    $("#buttonSubmit").attr("onclick", "Create()");
    $("#buttonSubmit").attr("class", "btn btn-success");
    $("#buttonSubmit").html("Create");
    $("#CreateModalLabel").html("Create New Employee");

    // Password Group
    $("#BodyModal > form > div:nth-child(5)").show()

    $("#InputNIK").val("");
    $("#InputFirstName").val("");
    $("#InputLastName").val("");
    $("#InputDOB").val("");
    $("#InputGender").val("");
    $("#InputSalary").val("");
    $("#InputPhone").val("");
    $("#InputEmail").val("");
    $("#InputPassword").val("");
    $("#InputDepartement").val("");

    $("#InputManagerId").html("");
    $("#InputManagerId").attr("disabled", "disabled");

    $("#InputRole").html("");
    $("#InputRole").attr("disabled", "disabled");


    $("#InputFirstName").attr("placeholder", "Input First Name");
    $("#InputLastName").attr("placeholder", "Input Last Name");
    $("#InputSalary").attr("placeholder", "Input Salary");
    $("#InputPhone").attr("placeholder", "Input Phone Number");
    $("#InputEmail").attr("placeholder", "Input Email");
    $("#InputPassword").attr("placeholder", "Input Password");
})

$("#InputDepartement").on('propertychange input', () => {
    $.ajax({
        "url": urlBackend + "/departement/GetDataManager/" + $("#InputDepartement").val(),
        "type": "GET",
        "datatype": "json",
        "dataSrc": "data",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var obj = result.data
            console.log(obj)
            $("#InputManagerId").html("");
            $("#InputManagerId").removeAttr("disabled");
            $("#InputManagerId").append(`<option value="${obj.manager_NIK}" selected>${obj.manager_Name}</option>`)
        },
        "error": (e) => {
            $("#InputManagerId").html("");
            $("#InputManagerId").attr("disabled", "disabled");
            $("#InputManagerId").append(`<option value="" selected disabled>Data Not Available</option>`)
        }
    })

    $.ajax({
        "url": urlBackend + "/role",
        "type": "GET",
        "datatype": "json",
        "dataSrc": "data",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var obj = result.data
            $("#InputRole").html("");
            $("#InputRole").removeAttr("disabled");
            $("#InputRole").append(`<option value="" selected disabled>Choose The Role</option>`)
            for (let i = 0; i < obj.length; i++) {
                $("#InputRole").append(`<option value="${obj[i].id}">${obj[i].name}</option>`)
            }
            $("#InputRole > option:nth-child(3)").hide()
        },
        "error": (e) => {
            $("#InputRole").html("");
            $("#InputRole").attr("disabled", "disabled");
            $("#InputRole").append(`<option value="" selected disabled>Data Not Available</option>`)
        }
    })
})

function Create() {
    let validateForm = true;

    if (
        $("#InputFirstName").val() == "" ||
        $("#InputLastName").val() == "" ||
        $("#InputDOB").val() == "" ||
        $("#InputGender").val() == "" ||
        $("#InputEmail").val() == "" ||
        $("#InputPhone").val() == "" ||
        $("#InputPassword").val() == "" ||
        $("#InputSalary").val() == "" ||
        $("#InputRole").val() == "" ||
        $("#InputManagerId").val() == "" ||
        $("#InputDepartement").val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    } else {
        if (!$("#InputEmail").val().match(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)) {
            Swal.fire({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your email is not valid",
            })
            validateForm = false
        }
        if (!$("#InputPhone").val().match(/^\d*\d$/)) {
            Swal.fire({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your phone number is invalid",
            })
            validateForm = false
        }
    }

    if (validateForm) {
        var Employee = {};
        Employee.FirstName = $("#InputFirstName").val();
        Employee.LastName = $("#InputLastName").val();
        Employee.BirthDate = $("#InputDOB").val();
        Employee.Gender = parseInt($("#InputGender").val());
        Employee.Email = $("#InputEmail").val();
        Employee.Phone = $("#InputPhone").val();
        Employee.Password = $("#InputPassword").val();
        Employee.Salary = $("#InputSalary").val();
        Employee.Departement_Id = parseInt($("#InputDepartement").val());
        Employee.Manager_Id = $("#InputManagerId").val();
        Employee.Role_Id = parseInt($("#InputRole").val());

        console.log(Employee)

        $.ajax({
            "type": "POST",
            "url": urlBackend + "/employee/register",
            "data": JSON.stringify(Employee),
            "contentType": "application/json;charset=utf-8",
            "headers": {
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
            "success": (result) => {
                if (result.status == 200 || result.status == 201) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Data successfully created',
                    })
                    $('#EmployeesTable').DataTable().ajax.reload();
                    $('#CreateModal').modal("hide");
                } else {
                    alert("Data failed to create")
                }
                $('#EmployeesTable').DataTable().ajax.reload();
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
    //$("#InputRole").removeAttr("disabled");

    $.ajax({
        "url": urlBackend + "/role",
        "type": "GET",
        "datatype": "json",
        "dataSrc": "data",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var obj = result.data
            $("#InputRole").html("");
            $("#InputRole").removeAttr("disabled");
            $("#InputRole").append(`<option value="" selected disabled>Choose The Role</option>`)
            for (let i = 0; i < obj.length; i++) {
                $("#InputRole").append(`<option value="${obj[i].id}">${obj[i].name}</option>`)
            }
            $("#InputRole > option:nth-child(3)").hide()
        },
        "error": (e) => {
            $("#InputRole").html("");
            $("#InputRole").attr("disabled", "disabled");
            $("#InputRole").append(`<option value="" selected disabled>Data Not Available</option>`)
        }
    })

    // Password Group
    $("#BodyModal > form > div:nth-child(5)").hide()
    $("#InputRole").removeAttr("disabled");

    // debugger;
    $.ajax({
        "type": "GET",
        "url": urlBackend + "/employee/SpecificDataEmployees/" + id,
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "headers": {
            'Authorization': 'Bearer ' + localStorage.getItem("token")
        },
        "success": function (result) {
            var obj = result.data;

            console.log(obj)

            // debugger;
            $('#InputNIK').val(obj.nik);
            $('#InputFirstName').val(obj.firstName);
            $('#InputLastName').val(obj.lastName);
            $('#InputDOB').val(obj.birthDate.slice(0, 10));
            $('#InputGender').val(obj.gender);
            $('#InputEmail').val(obj.email);
            $('#InputPhone').val(obj.phone);
            $('#InputSalary').val(obj.salary);
            $('#InputDepartement').val(obj.departement_Id);

            $.ajax({
                "url": urlBackend + "/departement/GetDataManager/" + obj.departement_Id,
                "type": "GET",
                "datatype": "json",
                "dataSrc": "data",
                "contentType": "application/json;charset=utf-8",
                "success": (result) => {
                    var obj = result.data
                    console.log(obj)
                    $("#InputManagerId").html("");
                    $("#InputManagerId").removeAttr("disabled");
                    $("#InputManagerId").append(`<option value="" selected disabled>Choose The Manager</option>`)
                    $("#InputManagerId").append(`<option value="${obj.manager_NIK}">${obj.manager_Name}</option>`)
                    $('#InputManagerId').val(obj.manager_NIK);
                },
                "error": (e) => {
                    $("#InputManagerId").html("");
                    $("#InputManagerId").attr("disabled", "disabled");
                    $("#InputManagerId").append(`<option value="" selected disabled>Data Not Available</option>`)
                }
            })

            $('#InputRole').val(obj.role_Id)

            $("#buttonSubmit").attr("onclick", "Update()");
            $("#buttonSubmit").attr("class", "btn btn-warning");
            $("#buttonSubmit").html("Update");
            $("#CreateModalLabel").html("Update Employee");
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
        $("#InputNIK").val() == "" ||
        $("#InputFirstName").val() == "" ||
        $("#InputLastName").val() == "" ||
        $("#InputDOB").val() == "" ||
        $("#InputGender").val() == "" ||
        $("#InputEmail").val() == "" ||
        $("#InputPhone").val() == "" ||
        $("#InputSalary").val() == "" ||
        $("#InputRole").val() == "" ||
        $("#InputManagerId").val() == "" ||
        $("#InputDepartement").val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    } else {
        if (!$("#InputEmail").val().match(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)) {
            Swal.fire({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your email is not valid",
            })
            validateForm = false
        }
        if (!$("#InputPhone").val().match(/^\d*\d$/)) {
            Swal.fire({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your phone number is not valid",
            })
            validateForm = false
        }
    }

    if (validateForm) {
        var Employee = {};
        Employee.NIK = $("#InputNIK").val();
        Employee.FirstName = $("#InputFirstName").val();
        Employee.LastName = $("#InputLastName").val();
        Employee.BirthDate = $("#InputDOB").val();
        Employee.Gender = parseInt($("#InputGender").val());
        Employee.Email = $("#InputEmail").val();
        Employee.Phone = $("#InputPhone").val();
        Employee.Salary = $("#InputSalary").val();
        Employee.Departement_Id = parseInt($("#InputDepartement").val());
        Employee.Manager_Id = $("#InputManagerId").val();
        Employee.Role_Id = parseInt($("#InputRole").val());
    
        console.log(Employee)
    
        $.ajax({
            "url": urlBackend + "/employee/register/update",
            "type": "PUT",
            "data": JSON.stringify(Employee),
            "contentType": "application/json; charset=utf-8",
            "headers": {
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
            "success": (result) => {
                if (result.status == 200 || result.status == 201) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Data successfully updated',
                    })
                    $('#EmployeesTable').DataTable().ajax.reload();
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
        "url": urlBackend + "/employee/" + id,
        "type": "GET",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var obj = result.data;

            Swal.fire({
                title: 'Delete data',
                icon: 'info',
                html: `Are you sure want to delete data with name <b>${obj.firstName} ${obj.lastName}</b>?`,
                showCloseButton: true,
                showConfirmButton: false,
                showDenyButton: true,
                showCancelButton: true,
                denyButtonText: `<span onclick="Delete('${obj.nik}')">Delete</span>`,
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
        "url": urlBackend + "/employee/register/delete/" + id,
        "type": "DELETE",
        "dataType": "json",
        "headers": {
            'Authorization': 'Bearer ' + localStorage.getItem("token")
        },
        "success": (result) => {
            if (result.status == 200 || result.status == 201) {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Data successfully deleted',
                })
                $('#EmployeesTable').DataTable().ajax.reload();
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