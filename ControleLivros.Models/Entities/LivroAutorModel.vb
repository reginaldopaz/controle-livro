Imports System.ComponentModel.DataAnnotations

Namespace ControleLivros.Models.Entities
    Public Class LivroAutor
        <Key>
        Public Property Livro_Codl As Integer
        Public Property Autor_CodAu As Integer

        Public Overridable Property Livro As Livro
        Public Overridable Property Autor As Autor
    End Class
End Namespace