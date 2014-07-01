//$(document).ready(function () {
$('#logo').html("STREAMEUS").attr('href', '/');
$('#show-pet-store-icon').parent().next().remove();
$('#show-pet-store-icon').parent().next().remove();
$('#show-pet-store-icon').parent().siblings().last().remove();
$('#show-pet-store-icon').parent().remove();
$('#input_apiKey').change(function() {
    var key = $('#input_apiKey')[0].value;
    if (key && key.trim() != "") {
        key = "bearer " + key;
        window.authorizations.add("key", new ApiKeyAuthorization("Authorization", key, "header"));
    }
}).attr('placeholder', 'bearer token');
//});