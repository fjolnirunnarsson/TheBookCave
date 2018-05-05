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
