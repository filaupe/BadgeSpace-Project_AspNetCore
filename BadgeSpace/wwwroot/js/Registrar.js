const CheckBox = document.getElementById("Switch")
const CPF = document.getElementById("CPF")

CheckBox.onclick = function () {
    CheckBox.classList.toggle('active')
    if (CheckBox.classList.contains('active')) {
        CPF.setAttribute("disabled", "disabled")
    } else {
        CPF.removeAttribute("disabled")
    }
}


// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
