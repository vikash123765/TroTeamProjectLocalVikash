// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const themeSlider = document.getElementById("theme-slider");
const body = document.body;

// The theme based on slider state
themeSlider.addEventListener("change", () => {
    const newTheme = themeSlider.checked ? "dark" : "light";
    body.setAttribute("data-bs-theme", newTheme);

    // Save the preference in localStorage
    localStorage.setItem("theme", newTheme);
});

// Load the saved theme preference on page load
window.addEventListener("DOMContentLoaded", () => {
    const savedTheme = localStorage.getItem("theme") || "light";
    body.setAttribute("data-bs-theme", savedTheme);

    // Set slider state based on saved theme
    themeSlider.checked = savedTheme === "dark";
});
document.addEventListener("DOMContentLoaded", function () {

    const backToTopButton = document.createElement("button");
    backToTopButton.innerText = "↑ Top";
    backToTopButton.className = "btn btn-primary back-to-top";
    backToTopButton.style.position = "fixed";
    backToTopButton.style.bottom = "20px";
    backToTopButton.style.right = "20px";
    backToTopButton.style.display = "none";
    backToTopButton.style.zIndex = "1000";
    backToTopButton.style.padding = "10px 15px";
    backToTopButton.style.borderRadius = "50%";
    backToTopButton.style.boxShadow = "0px 4px 8px rgba(0, 0, 0, 0.2)";
    document.body.appendChild(backToTopButton);

    window.addEventListener("scroll", function () {
        if (window.scrollY > 300) {
            backToTopButton.style.display = "block";
        } else {
            backToTopButton.style.display = "none";
        }
    });

    backToTopButton.addEventListener("click", function () {
        window.scrollTo({ top: 0, behavior: "smooth" });
    });

});
