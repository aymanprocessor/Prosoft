//const { event } = require("jquery");

const openModalButtons = document.querySelectorAll("[data-modal-target]");
const closeModalButtons = document.querySelectorAll("[data-close-button]");
const overlays = document.querySelectorAll("#overlay");
<<<<<<< HEAD
=======
//console.log(closeModalButtons);

>>>>>>> c15690030add8d290cd4672002db1c68819ed61f
//openModalButtons.forEach((button) => {
//    button.addEventListener("click", openModalListener(event));
//});

function openModalListener(event) {
    const modalContainer = event.target.nextElementSibling;

    const modal = modalContainer.querySelector(".modal");
    const overlay = modalContainer.querySelector("#overlay");
    //console.log(modal, overlay);

    openModal(modal, overlay);
}

//overlays.forEach((overlay) => {
//    overlay.addEventListener("click", overlayListener(event));
//});

function overlayListener(event) {
    //const modals = document.querySelectorAll(".modal.active-popup");
    const overlay = event.target;
    const modal = event.target.previousElementSibling;
    // console.log(modals);
    //modals.forEach((myModal) => {
    //   console.log(myModal);
    closeModal(modal, overlay);
    //});
}

//closeModalButtons.forEach((button) => {
//    button.addEventListener("click", closeModelListener());
//});

function closeModelListener(event) {
    const myModal = event.target.closest(".modal");
    const myOverlay = myModal.nextElementSibling;
    // console.log(myModal, myOverlay);
    closeModal(myModal, myOverlay);
}

function openModal(modal, overlay) {
    if (modal == null || overlay == null) return;
    modal.classList.add("active-popup");
    overlay.classList.add("active-popup");
}

function closeModal(modal, overlay) {
    if (modal == null || overlay == null) return;
    modal.classList.remove("active-popup");
    overlay.classList.remove("active-popup");
}
