console.log("INI LOGIN Komen");

//$('#loginUser').submit(function (e) {
//    e.preventDefault();
//    Login();
//});

function Login() {
    console.log("INI BUTTON LOGIN");
    var loginUser = new Object();
    loginUser.Email = $("#inputEmail").val();
    loginUser.Password = $("#inputPassword").val();

    $.ajax({
        url: "Login/Auth",
        //contentType: "application/json;charset=utf-8",
        type: "POST",
        data: loginUser
    }).done((result) => {
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: result.message,
                type: 'success'
            });
            let token = result.idToken;
            localStorage.setItem("Token", token);
            $("#loginUser")[0].reset();
            setTimeout(function () {
                location.href = "/Employees";
            }, 3000);
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: result.message,
                type: 'error'
            });
        }
    }).fail((error) => {
        console.log(error);
    })
}