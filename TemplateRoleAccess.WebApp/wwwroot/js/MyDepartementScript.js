$(document).ready(function () {
    $('#tab-mydepartement').addClass("active")

    $.ajax({
        "type": "GET",
        "url": urlBackend + "/departement/GetDepartementName/" + localStorage.getItem("nik"),
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (result) {
            var obj = result.data;

            console.log(obj)

            $('#departementName').html("Departement of " + obj.departement_Name);
        },
        error: function (errormesage) {
            swal("Data failed to input!", "You clicked the button!", "error");
        }
    })

    $('#MyDepartementTable').DataTable({
        "ajax": {
            "url": urlBackend + "/departement/GetMyDepartement/" + localStorage.getItem("nik"),
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data",
            "error": (e) => {
                document.querySelector("#MyDepartementTable > tbody > tr > td").innerHTML = "Data Not Available";
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
            { "data": "phone" },
            { "data": "email" },
            {
                "data": "nik",
                "className": "text-center",
                "render": function (data) {
                    if (localStorage.getItem("role") == "Employee") {
                        $("#MyDepartementTable > thead > tr > th:nth-child(7)").hide()
                        return ``
                    }
                    return `
                    <button class="btn btn-info" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="GetById('${data}')">
                        <i class="fa fa-pen"></i>
                    </button>`
                }, "width": "12%"
            }
        ],
        "width": "100%"
    });

});

function GetById(id) {
    // debugger;
    $.ajax({
        "type": "GET",
        "url": urlBackend + "/employee/" + id,
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (result) {
            var obj = result.data;

            // debugger;
            $('#InputNIK').val(obj.nik);
            $('#InputFirstName').val(obj.firstName);
            $('#InputLastName').val(obj.lastName);
            $('#InputDOB').val(obj.birthDate.slice(0, 10));
            $('#InputGender').val(obj.gender == 0 ? "Male" : "Female");
            $('#InputEmail').val(obj.email);
            $('#InputPhone').val(obj.phone);
            $('#InputSalary').val(obj.salary);

            $("#CreateModalLabel").html("Detail Employee");
            $('#CreateModal').modal("show");
        },
        error: function (errormesage) {
            swal("Data failed to input!", "You clicked the button!", "error");
        }
    })
}