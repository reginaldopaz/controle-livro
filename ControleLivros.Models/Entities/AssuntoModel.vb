Imports System.ComponentModel.DataAnnotations

Namespace ControleLivros.Models.Entities
    Public Class Assunto
        <Key>
        Public Property CodAs As Integer

        <Required(ErrorMessage:="A descrição é obrigatória")>
        <StringLength(20)>
        Public Property Descricao As String
        Public Overridable Property Livros As ICollection(Of Livro) = New HashSet(Of Livro)
    End Class
End Namespace
