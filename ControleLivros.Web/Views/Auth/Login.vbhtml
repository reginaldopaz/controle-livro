@Code
    ViewData("Title") = "Login"
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login</title>


    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
        
</head>
<body>

    <div class="login-container">
        <div class="login-box">
            <img src="~/Imagens/Login.jpg" alt="Imagem de Login" />
            <div class="login-form">
                <h2>Login</h2>
                @Using Html.BeginForm("Login", "Auth", FormMethod.Post)
                    @<div Class="form-group">
                        <Label for="username">Usuário</Label>
                        <input type="text" Class="form-control" id="username" name="username" required />
                    </div>

                    @<div Class="form-group">
                        <Label for="password">Senha</Label>
                        <input type="password" Class="form-control" id="password" name="password" required />
                    </div>

                    @If Not String.IsNullOrEmpty(TryCast(ViewData("ErrorMessage"), String)) Then
                        @<div class="error-message">
                            @ViewData("ErrorMessage")
                        </div>
                    End If

                    @<button type="submit" Class="btn btn-primary btn-block">Entrar</button>
                End Using
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
