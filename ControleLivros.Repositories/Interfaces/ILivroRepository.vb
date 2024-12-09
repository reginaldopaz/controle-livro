Imports ControleLivros.Models.ControleLivros.Models.Entities

Namespace ControleLivros.Repositories.Interfaces
    Public Interface ILivroRepository
        Function GetAll() As IEnumerable(Of Livro)
        Function GetById(id As Integer) As Livro
        Sub Add(livro As Livro)
        Sub Update(livro As Livro)
        Sub Delete(id As Integer)
    End Interface
End Namespace
