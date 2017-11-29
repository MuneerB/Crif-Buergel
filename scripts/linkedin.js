

$(document).ready(function() { 
    $.getScript("https://platform.linkedin.com/in.js?async=true", function success() { 
        IN.init({ 
           // api_key: "75xq113gk33z2t",
			api_key : "759cw3fpknvmgu",
            authorize: true,
			onLoad : onLinkedInLoad
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
	IN.API.Profile("me").fields( [ "id", "firstName", "lastName", "pictureUrl", "headline", "publicProfileUrl", "email-address", "positions:(title,company)", "location:(name,country:(code))" ] ).result(display);
}

// 2. Runs when the Profile() API call returns successfully
function display(result) {
	member = result.values[0];
	console.log(member.location.country.code);
	//console.log(member.firstName + " " + member.lastName + " " + member.emailAddress + " " + member.positions.values[0].company.name + " " + member.positions.values[0].title + " " + member.location.name);
	document.getElementById('FirstName').value = member.firstName;
    document.getElementById('LastName').value = member.lastName;	
	
	if(member.emailAddress){
    	document.getElementById('Email').value = member.emailAddress;
	}	
	
	if(member.positions.values[0].company.name){
    	document.getElementById('Company').value = member.positions.values[0].company.name;
	}
	
	if(member.positions.values[0].title){
    	document.getElementById('JobTitle').value = member.positions.values[0].title;
	}
	if(member.location.name){
    	document.getElementById('Country').value = member.location.country.code.toUpperCase();
	}
	if(member.publicProfileUrl){
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
