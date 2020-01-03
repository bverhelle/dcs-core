// Write your JavaScript code.

$("#hairdreams-video-modal").on('focus.bs.modal', function (e) {
    $("#hairdreams-video-modal iframe").attr("src", "https://player.vimeo.com/video/71098722?autoplay=1");
});

$("#hairdreams-video-modal").on('hidden.bs.modal', function (e) {
    $("#hairdreams-video-modal iframe").attr("src", "");
});

$("#myAppointmentModal").on('focus.bs.modal', function (e) {
    fbq('track', 'Optios');
    fbq('trackCustom', 'Optios');
    //fbq('track', 'Search', {search_string: 'Open Appointment Window'})
});



function changeBackground(backgroundImage, backgroundPosition, backgroundAttachment, backgroundRepeat, backgroundSize, backgroundColor){
    $("body").css('background-image', backgroundImage);
    $("body").css('background-position', backgroundPosition);
    $("body").css('background-attachment', backgroundAttachment);
    $("body").css('background-repeat', backgroundRepeat);
    $("body").css('background-size', backgroundSize);
    $("body").css('background-color', backgroundColor);
    //$("body").css('height', height);
}

$(function () {
    $('div[onload]').trigger('onload');
});