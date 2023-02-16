$(document).ready(function () {
    $('#tab-profile').addClass("active")

    $.ajax({
        "url": urlBackend + "/employee/SpecificDataEmployees/" + localStorage.getItem("nik"),
        "type": "GET",
        "datatype": "json",
        "dataSrc": "data",
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            var response = result.data

            console.log(response)

            $("#InputNIK").val(response.nik);
            $("#InputFullName").val(response.firstName + " " + response.lastName);
            $("#InputDOB").val(response.birthDate.slice(0, 10));
            $("#InputGender").val(response.gender == 0 ? "Male" : "Female");
            $("#InputEmail").val(response.email);
            $("#InputPhone").val(response.phone);
            $("#InputDepartement").val(response.departement_Name);
            $("#InputRole").val(response.role_Name);
            $("#InputSalary").val(response.salary);

            $("#InputPassword").val("");
            $("#InputPassword").attr("placeholder", "Type here to change your password");

            fetch(urlBackend + "/employee/" + response.manager_Id)
                .then(function (response) {
                    return response.json();
                })
                .then(function (result) {
                    if (result.data.firstName != null || result.data.lastName != null) {
                        $("#InputManagerId").val(result.data.firstName + " " + result.data.lastName);
                    }
                })
                .catch(function (err) {
                    $("#InputManagerId").val("Data Not Available");
                });

        },
        "error": (e) => {
            $("#InputNIK").val("Data Not Available");
            $("#InputFullName").val("Data Not Available");
            $("#InputDOB").val("Data Not Available");
            $("#InputGender").val("Data Not Available");
            $("#InputEmail").val("Data Not Available");
            $("#InputPhone").val("Data Not Available");
            $("#InputDepartement").val("Data Not Available");
            $("#InputManagerId").val("Data Not Available");
            $("#InputRole").val("Data Not Available");
            $("#InputSalary").val("Data Not Available");
            $("#InputPassword").val("");
        }
    })
});

function Update() {
    let validateForm = true;

    if ($("#InputPassword").val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out your password",
        })
        validateForm = false
    }

    if (validateForm) {
        var Account = {};
        Account.NIK = $("#InputNIK").val();
        Account.Password = $("#InputPassword").val();

        console.log(Account)

        $.ajax({
            "url": urlBackend + "/account/Update-password",
            "type": "PUT",
            "data": JSON.stringify(Account),
            "contentType": "application/json; charset=utf-8",
            "success": (result) => {
                if (result.status == 200 || result.status == 201) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: result.message,
                    })

                    $("#InputPassword").val("");
                }
            },
            "error": (result) => {
                if (result.status == 400 || result.status == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: result.responseJSON.message,
                    })

                    $("#InputPassword").val("");
                }
            },
        })
    }
}