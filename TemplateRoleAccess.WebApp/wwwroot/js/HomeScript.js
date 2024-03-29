﻿if (localStorage.getItem("role") == "Admin") {
    $("#home-non-admin").hide();
    $("#home-admin").show();
}else if (localStorage.getItem("role") != "Admin") {
    $("#home-admin").hide();
    $("#home-non-admin").show();

    $("#hello-user").html(`Hello, ${localStorage.getItem("name")} (${localStorage.getItem("nik")})`);
}

$(document).ready(function () {
    $('#tab-home').addClass("active")

    $.ajax({
        "type": "GET",
        "url": urlBackend + "/employee",
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "headers": {
            'Authorization': 'Bearer ' + localStorage.getItem("token")
        },
        "success": function (result) {
            var obj = result.data;

            $("#TotalEmployees").html(obj.length)
        },
        "error": (e) => {
            $("#TotalEmployees").html("0")
        },
    })

    $.ajax({
        "type": "GET",
        "url": urlBackend + "/departement",
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "headers": {
            'Authorization': 'Bearer ' + localStorage.getItem("token")
        },
        "success": function (result) {
            var obj = result.data;

            $("#TotalDepartements").html(obj.length)
            
        },
        "error": (e) => {
            $("#TotalDepartements").html("0")
        },
    })

    $.ajax({
        "type": "GET",
        "url": urlBackend + "/role",
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (result) {
            var obj = result.data;

            $("#TotalRoles").html(obj.length)
            
        },
        "error": (e) => {
            $("#TotalRoles").html("0")
        },
    })

    $.ajax({
        "type": "GET",
        "url": urlBackend + "/accountrole",
        "contentType": "application/json; charset=utf-8",
        "dataType": "json",
        "success": function (result) {
            var obj = result.data;

            $("#TotalAccounts").html(obj.length)
            
        },
        "error": (e) => {
            $("#TotalAccounts").html("0")
        },
    })

});