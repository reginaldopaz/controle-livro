Imports System.Web.Mvc
Imports System.Web.Security

Namespace Controllers
    Public Class AuthController
        Inherits Controller

        ' GET: Auth/Login
        Public Function Login(Optional ByVal returnUrl As String = "") As ActionResult
            ViewData("ReturnUrl") = returnUrl
            Return View()
        End Function

        ' POST: Auth/Login
        <HttpPost>
        Public Function Login(username As String, password As String, Optional ByVal returnUrl As String = "") As ActionResult
            Dim user As UserCredentials.Users

            ' Verifique se o usuário e a senha estão corretos
            If [Enum].TryParse(username, True, user) AndAlso UserCredentials.Passwords.TryGetValue(user, password) Then
                ' Autenticação bem-sucedida, definir cookie de autenticação
                FormsAuthentication.SetAuthCookie(username, False)

                ' Redireciona para a URL de retorno ou página inicial
                If Not String.IsNullOrEmpty(returnUrl) AndAlso Url.IsLocalUrl(returnUrl) Then
                    Return Redirect(returnUrl)
                Else
                    Return RedirectToAction("Index", "Home")
                End If
            Else
                ' Autenticação falhou, mostrar mensagem de erro
                ViewData("ErrorMessage") = "Usuário ou senha inválidos."
                Return View()
            End If
        End Function

        ' POST: Auth/Logout
        <HttpPost>
        Public Function Logout() As ActionResult
            ' Limpar autenticação
            FormsAuthentication.SignOut()
            Return RedirectToAction("Login")
        End Function
    End Class
End Namespace
