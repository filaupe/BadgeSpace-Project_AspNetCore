document.addEventListener("DOMContentLoaded", function() {            
    //altera a URL do botão
    document.getElementById("facebook-share-btt").href = "https://www.facebook.com/sharer/sharer.php?u=" + encodeURIComponent(window.location.href);
}, false);