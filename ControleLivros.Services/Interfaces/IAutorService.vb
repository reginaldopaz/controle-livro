Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities

Namespace ControleLivros.Services.Interfaces
    Public Interface IAutorService
        Function GetAll() As IEnumerable(Of Autor)
        Function GetById(id As Integer) As Autor
        Sub Add(autor As Autor)
        Sub Update(autor As Autor)
        Sub Delete(id As Integer)
    End Interface
End Namespace
