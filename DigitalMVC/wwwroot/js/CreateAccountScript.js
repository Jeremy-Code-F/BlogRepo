function CreateAccount() {
    let userName = $("#usernameInput").val();
    let password = $("#passwordInput").val();
    let email = $("#emailInput").val();
    hashedPass = hex_md5(password);

    let payLoad = '{"username": "' + userName + '", "password": "' + password + '", "email": "' + email + '"}';

    $.ajax({
        url: "/api/CreateAccount/",
        dataType: 'json',
        contentType: 'application/json',
        type: 'GET',
        data: payLoad,
        success: function (response) {
            console.log("Success : " + response);
        },
        error: function (err) {
            console.log("Error hit " + err.responseText);
        }
    });
    //TODO: Pass this to /api/CreateAccount in the body w/ Ajax call

 
}

function encodePassword(password) {

}