// Write your JavaScript code.
$(document).ready(function() {
    $('.heart-icon').on('click', function() {
        $(this).toggleClass('heart-icon-active');
    });
});

$(document).ready(function() {
    $('.heart-icon-active').on('click', function() {
        $(this).toggleClass('heart-icon-active');
    });
});


document.getElementById("yourLinkId").onclick = function() {
    document.getElementById("yourFormId").submit();
}

function itemadded(tala){
        var strengur = "isadded" + tala;
        var elem2 = document.getElementById(strengur);
        elem2.innerText = "Added to cart!";
        elem2.classList.remove("btn-primary");
        elem2.classList.add("btn-success");
    }