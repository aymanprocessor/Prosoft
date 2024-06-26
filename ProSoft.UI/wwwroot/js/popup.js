//const { event } = require("jquery");

const openModalButtons = document.querySelectorAll("[data-modal-target]");
const closeModalButtons = document.querySelectorAll("[data-close-button]");
const overlays = document.querySelectorAll("#overlay");
//console.log(closeModalButtons);

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

////////////////////////////////////////////////////
// For Alerts That Aren't In The Dom

function openAlert(event) {
    setTimeout(() => {
        openModalListener(event);
    }, 50)
}

function closeOverlay(event) {
    overlayListener(event);
    setTimeout(() => {
        event.target.closest(".modal-container").remove();
    }, 200)
}

function closeAlert(event) {
    closeModelListener(event);
    setTimeout(() => {
        event.target.closest(".modal-container").remove();
    }, 200)
}