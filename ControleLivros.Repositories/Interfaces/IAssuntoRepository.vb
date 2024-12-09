Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities

Namespace ControleLivros.Repositories.Interfaces
    Public Interface IAssuntoRepository
        Function GetAll() As IEnumerable(Of Assunto)
        Function GetById(id As Integer) As Assunto
        Sub Add(assunto As Assunto)
        Sub Update(assunto As Assunto)
        Sub Delete(id As Integer)
    End Interface
End Namespace
