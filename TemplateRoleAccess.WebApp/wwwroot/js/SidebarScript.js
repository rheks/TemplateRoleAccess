if (localStorage.getItem("role") == "Admin") {
    $("#tab-home").show()
    $("#tab-employees").show()
    $("#tab-departements").show()
    $("#tab-mydepartement").hide()
    $("#tab-roles").show()
    $("#tab-profile").show()
} else if (localStorage.getItem("role") == "Manager" || localStorage.getItem("role") == "Employee") {
    $("#tab-home").show()
    $("#tab-employees").hide()
    $("#tab-departements").hide()
    $("#tab-mydepartement").show()
    $("#tab-roles").hide()
    $("#tab-profile").show()
}