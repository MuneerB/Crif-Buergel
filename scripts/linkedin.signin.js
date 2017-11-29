
$(document).ready(function () {
    $.getScript("https://platform.linkedin.com/in.js?async=true", function success() {
        IN.init({
            api_key: $("#hdApiClientId").val(),
            authorize: true,
            onLoad: onLinkedInLoad
        });
    });
});

function liAuth() {
    IN.User.authorize(function () {
        onLinkedInAuth();
    });
}

// 2. Runs when the JavaScript framework is loaded
function onLinkedInLoad() {
    IN.Event.on(IN, "auth", getProfileData);
}

// 2. Runs when the viewer has authenticated
function onLinkedInAuth() {
    IN.API.Profile("me").fields(["id", "firstName", "lastName", "pictureUrl", "headline", "publicProfileUrl", "email-address", "positions:(title,company)", "location:(name,country:(code))"]).result(display);
}

// 2. Runs when the Profile() API call returns successfully
function display(result) {
    member = result.values[0];
    document.getElementById('FnameTxt').value = member.firstName;
    document.getElementById('LnameTxt').value = member.lastName;

    if (member.emailAddress) {
        document.getElementById('emailTxt').value = member.emailAddress;
    }

    if (member.positions.values[0].company.name) {
        document.getElementById('companyTxt').value = member.positions.values[0].company.name;
    }

    if (member.positions.values[0].title) {
        document.getElementById('jobTitleTxt').value = member.positions.values[0].title;
    }
    if (member.publicProfileUrl) {
        document.getElementById('linkedinProfileUrl').value = member.publicProfileUrl;
    }
}
// Handle the successful return from the API call
function onSuccess(data) {
    console.log(data);
}

// Handle an error response from the API call
function onError(error) {
    console.log(error);
}

// Use the API call wrapper to request the member's basic profile data
function getProfileData() {
    IN.API.Raw("/people/~").result(onSuccess).error(onError);
}
