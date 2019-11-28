// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

class Modal {
    constructor(contents) {
        this.contents = contents;
        this.element = document.createElement("div");
        this.element.classList.add("modal");
        document.appendChild(element);
    }

    open() {
        this.element.classList.add("open");
    }

    close() {
        this.element.classList.remove("open");
    }

    toggle() {
        this.element.classList.toggle("open");
    }

    destroy() {
        document.removeChild(element);
    }
}

var modal = null;

function initializeModals() {
    document.querySelectorAll(".prize img")
    .forEach(function(element) {
        element.onclick(openModal)
    });
}

function openModal() {
    
}
