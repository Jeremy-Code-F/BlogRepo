function CreateAccount() {
    let userName = $("#usernameInput").val();
    let password = $("#passwordInput").val();
    let email = $("#emailInput").val();
    hashedPass = hex_md5(password);

    //TODO: Pass this to /api/CreateAccount in the body w/ Ajax call

 
}

function encodePassword(password) {

}