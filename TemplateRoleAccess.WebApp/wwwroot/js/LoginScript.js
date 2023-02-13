$("#BtnSubmit").click(() => {
    //debugger;
    $("#loadingSpinner").removeClass("hide")
    var loginData = {}
    loginData.Email = $("#InputEmail").val()
    loginData.Password = $("#InputPassword").val()

    console.log(loginData)

    $.ajax({
        "type": "POST",
        "url": urlBackend + "/Account/Login",
        "data": JSON.stringify(loginData),
        "contentType": "application/json;charset=utf-8",
        "success": (result) => {
            //$("#loadingSpinner").addClass("hide")
            if (result.status == 200 || result.status == 201) {
                var response = result.data

                sessionStorage.setItem("nik", response.nik)
                sessionStorage.setItem("email", response.email)
                sessionStorage.setItem("name", response.name)
                sessionStorage.setItem("token", response.token)

                window.location.href = urlFrontend + "dashboard/index";
            }
        },
        "error": (result) => {
            $("#loadingSpinner").addClass("hide")
            if (result.status == 404 || result.status == 400 || result.status == 500) {
                Swal.fire({
                    icon: 'error',
                    title: 'Failed',
                    text: result.responseJSON.message,
                })
            }
        },
    })
})