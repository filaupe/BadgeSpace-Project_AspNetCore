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

/*Rolagem Suave*/

const menuLinks = document.querySelectorAll('.menu a[href^="#"]');

function getDistanceFromTheTop(element) {
  const id = element.getAttribute("href");
  return document.querySelector(id).offsetTop;
}

function nativeScroll(distanceFromTheTop) {
      window.scroll({
    top: distanceFromTheTop,
    behavior: "smooth",
  });
}

function scrollToSection(event) {
  event.preventDefault();
  const distanceFromTheTop = getDistanceFromTheTop(event.target) - 90;
  smoothScrollTo(0, distanceFromTheTop);
}

menuLinks.forEach((link) => {
  link.addEventListener("click", scrollToSection);
});

function smoothScrollTo(endX, endY, duration) {
  const startX = window.scrollX || window.pageXOffset;
  const startY = window.scrollY || window.pageYOffset;
  const distanceX = endX - startX;
  const distanceY = endY - startY;
  const startTime = new Date().getTime();

  duration = typeof duration !== "undefined" ? duration : 400;

  const easeInOutQuart = (time, from, distance, duration) => {
    if ((time /= duration / 2) < 1)
      return (distance / 2) * time * time * time * time + from;
    return (-distance / 2) * ((time -= 2) * time * time * time - 2) + from;
  };

  const timer = setInterval(() => {
    const time = new Date().getTime() - startTime;
    const newX = easeInOutQuart(time, startX, distanceX, duration);
    const newY = easeInOutQuart(time, startY, distanceY, duration);
    if (time >= duration) {
      clearInterval(timer);
    }
    window.scroll(newX, newY);
  }, 1000 / 60);
}


//Animação do sistema


AOS.init({
  offset: 120, 
  delay: 0, 
  duration: 900, 
  easing: 'ease', 
  once: false, 
  mirror: false, 
  anchorPlacement: 'top-bottom', 

});
