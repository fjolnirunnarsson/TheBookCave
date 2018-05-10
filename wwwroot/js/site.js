// Write your JavaScript code.
$(document).ready(function() {
    $("heart-icon").on('click', function() {
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
        var elem = document.getElementById(strengur);
        elem.innerText = "Added to cart!";
        elem.classList.remove("btn-primary");
        elem.classList.add("btn-success");
    }

function wishadded(tala){
        var strengur = "wishadded" + tala;
        var elem = document.getElementById(strengur);
        elem.classList.add("red");
}

function addclass(tala){
    var strengur = "wishadded" + tala;
    var elem = document.getElementById(strengur);
    elem.classList.add("red");
}

$(document).ready(function() {
    $('#mycheckbox').change(function() {
        $('#mycheckboxdiv').toggle();
    });
});

