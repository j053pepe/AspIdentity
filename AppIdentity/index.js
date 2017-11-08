$(function () {
    var IndexFn = {
        init: function () {
            $('#frmRegistro').on('submit', IndexFn.Registro);
            $('#frmLogin').on('submit', IndexFn.Login);
        },
        Registro: function (event) {
            // alert("Hola Perrassssss");
            

            var Usuario = {
                Nombre: $('#txtNombre').val(),
                Email: $('#txtEmail').val(),
                Pass: $('#txtPass').val()
            };

            Usuario = JSON.stringify(Usuario);

            $.ajax(
                {
                    method: "POST",
                    url: "api/person/add",
                    data: Usuario,
                    contentType:"application/json"
                }
            ).done(function (msg) {
                alert("Data Saved: " + msg);
            });

            event.preventDefault();
        },
        Login: function () {
            var Usuario = {
                Nombre: $('#txtNombreL').val(),
                Pass: $('#txtPassL').val()
            };

            Usuario = JSON.stringify(Usuario);

            $.ajax(
                {
                    method: "POST",
                    url: "api/person/find",
                    data: Usuario,
                    contentType: "application/json"
                }
            ).done(function (msg) {
                alert("Data Saved: " + msg);
            });

            event.preventDefault();
        }
    };

    IndexFn.init();
});