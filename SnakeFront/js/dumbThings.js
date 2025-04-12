document.addEventListener("DOMContentLoaded", function () {
    const darkModeToggle = document.getElementById("dark-mode-toggle");
    const lightIcon = document.querySelector(".light-mode-icon");
    const darkIcon = document.querySelector(".dark-mode-icon");

    if (localStorage.getItem("dark-mode") === "true" ||
        (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches)){
            enableDarkMode();

    }

    darkModeToggle.addEventListener('click', function () {
        if (document.documentElement.classList.contains("dark")) {
            disableDarkMode();
        }
        else{
            enableDarkMode();
        }
    });

    function enableDarkMode() {
        document.documentElement.classList.add("dark");
        localStorage.setItem("dark-mode", "true");
        lightIcon.classList.add('hidden');
        darkIcon.classList.remove('hidden');
    }

    function disableDarkMode() {
        document.documentElement.classList.remove("dark");
        localStorage.setItem("dark-mode", "false");
        darkIcon.classList.remove('hidden');
        lightIcon.classList.remove('hidden');
    }
})