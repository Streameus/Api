//$(document).ready(function () {
function getParameterByName(name) {
    if (location.hash == undefined || location.hash == '')
        return null;
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\#&!]" + name + "=([^&#!]*)"),
        results = regex.exec(location.hash);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}


$('#logo').html("STREAMEUS").attr('href', '/');
$('#show-pet-store-icon').parent().next().remove();
$('#show-pet-store-icon').parent().next().remove();
$('#show-pet-store-icon').parent().remove();
$('#input_apiKey').change(function() {
    var key = $('#input_apiKey')[0].value;
    if (key && key.trim() != "") {
        key = "bearer " + key;
        window.authorizations.add("key", new ApiKeyAuthorization("Authorization", key, "header"));
    }
}).attr('placeholder', 'bearer token');
$('#explore').html('Get token from url').click(function() {
    var accessToken = getParameterByName('access_token');
    if (accessToken != '' && accessToken !== undefined) {
        $('#input_apiKey').val(accessToken).trigger('change');
        localStorage.bearerToken = accessToken;
    }
    return false;
});

if (localStorage.getItem('bearerToken'))
    $('#input_apiKey').val(localStorage.getItem('bearerToken')).trigger('change');
//});