function CreateAccount() {
    let userName = $("#usernameInput").val();
    let password = $("#passwordInput").val();
    let email = $("#emailInput").val();
    hashedPass = hex_md5(password);

    let payLoad = '{"username": "' + userName + '", "password": "' + hashedPass + '", "email": "' + email + '"}';

    $.ajax({
        url: "http://localhost:53579/api/CreateAccount/",
        dataType: 'json',
        contentType: 'application/json',
        type: 'POST',
        data: payLoad,
        success: function (response) {
            alert("Succesfully created account : " + response);
            ClearInputs();
        },
        error: function (err) {
            alert(err.responseText);
            ClearInputs();
        }
    });
    //TODO: Pass this to /api/CreateAccount in the body w/ Ajax call

 
}

function ClearInputs() {
    let userName = document.getElementById("usernameInput");
    let password = document.getElementById("passwordInput");
    let email = document.getElementById("emailInput");

    userName.value = "";
    password.value = "";
    email.value = "";
}

function encodePassword(password) {

}


function loginUser() {
    let userName = $("#usernameInput").val();
    let password = $("#passwordInput").val();
    //let email = $("#emailInput").val();
    hashedPass = hex_md5(password);

    let payLoad = '{"username": "' + userName + '", "password": "' + hashedPass + '"}';

    $.ajax({
        url: "/api/LoginUser/",
        dataType: 'json',
        contentType: 'application/json',
        type: 'POST',
        data: payLoad,
        success: function (response) {
            alert("Successfully logged in.");
        },
        error: function (err) {
            alert("Error: " + err.responseText);
            console.log("Error hit " + err.responseText);
        }
    });
}

function ResendEmail() {
    let userName = $("#usernameInput").val();
    let password = $("#passwordInput").val();
    //let email = $("#emailInput").val();
    hashedPass = hex_md5(password);

    let payLoad = '{"username": "' + userName + '", "password": "' + hashedPass + '"}';

    $.ajax({
        url: "/api/LoginUser/",
        dataType: 'json',
        contentType: 'application/json',
        type: 'POST',
        data: payLoad,
        success: function (response) {
            alert("Successfully logged in.");
        },
        error: function (err) {
            alert("Error: " + err.responseText);
            console.log("Error hit " + err.responseText);
        }
    });
}