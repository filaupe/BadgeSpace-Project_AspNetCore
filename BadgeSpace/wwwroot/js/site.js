const CheckBox = document.getElementById("Switch")
const CPF_CNPJ = document.getElementById("CPF")
const LabelCPF = document.getElementById("labelcpf")

CheckBox.onchange = function () {
    CheckBox.classList.toggle('active')
    if (CheckBox.classList.contains('active')) {
        LabelCPF.innerHTML = "CNPJ"
        CPF_CNPJ.setAttribute("maxlength", "18")
        CPF_CNPJ.value = null
    } else {
        LabelCPF.innerHTML = "CPF"
        CPF_CNPJ.setAttribute("maxlength", "14")
        CPF_CNPJ.value = null
    }
}


// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
