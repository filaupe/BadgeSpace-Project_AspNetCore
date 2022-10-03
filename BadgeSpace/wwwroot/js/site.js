﻿const CheckBox = document.getElementById("Switch")
const CPF = document.getElementById("CPF")
const span = document.getElementById("spancpf")

CheckBox.onchange = function () {
    CheckBox.classList.toggle('active')
    if (CheckBox.classList.contains('active')) {
        CPF.setAttribute("disabled", "disabled")
        CPF.removeAttribute("required")
        span.style.display = "none"
    } else {
        CPF.removeAttribute("disabled")
        CPF.setAttribute("required", "")
        span.style.display = "block"
    }
}


// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
