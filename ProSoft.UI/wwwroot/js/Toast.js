// Configure SweetAlert toast
const Toast = Swal.mixin({
    toast: true,
    iconColor: 'white',
    customClass: {
        popup: 'colored-toast',
    },
    position: 'top-end', // Position of the toast (top-right corner)
    showConfirmButton: false, // Hide the "OK" button
    timer: 5000, // Auto-close after 3 seconds
    timerProgressBar: true, // Show a progress bar
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer); // Pause on hover
        toast.addEventListener('mouseleave', Swal.resumeTimer); // Resume on leave
    }
});