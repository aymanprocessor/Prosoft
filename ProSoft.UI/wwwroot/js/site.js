// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function updateQueryParams(key, value) {
    const url = new URL(window.location.href); // Create a URL object based on the current location
    url.searchParams.set(key, value); // Set or update the stockType query param

    // Update the URL without reloading the page
    window.history.replaceState({}, '', url);
}