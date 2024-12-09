Imports System.ComponentModel.DataAnnotations

Namespace ControleLivros.Models.Entities
    Public Class Autor
        <Key>
        Public Property CodAu As Integer

        <Required(ErrorMessage:="O nome é obrigatório")>
        <StringLength(40)>
        Public Property Nome As String
        Public Overridable Property Livros As ICollection(Of Livro) = New HashSet(Of Livro)

    End Class
End Namespace