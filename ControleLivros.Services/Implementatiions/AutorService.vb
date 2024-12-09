Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Services.ControleLivros.Services.Interfaces
Imports ControleLivros.Models
Imports ControleLivros.Data.ControleLivros.Data.Context
Imports System.Runtime.Remoting.Contexts


Namespace ControleLivros.Services.Implementations
    Public Class AutorService
        Implements IAutorService

        Private ReadOnly _autorRepository As IAutorRepository

        'Public Sub New(autorRepository As IAutorRepository)
        '    _autorRepository = autorRepository
        'End Sub

        Public Sub New(autorRepository As IAutorRepository, context As ControleLivrosContext)
            _autorRepository = autorRepository
            context = context
        End Sub

        Public Function GetAll() As IEnumerable(Of Autor) Implements IAutorService.GetAll
            Return _autorRepository.GetAll()
        End Function

        Public Function GetById(id As Integer) As Autor Implements IAutorService.GetById
            Return _autorRepository.GetById(id)
        End Function

        Public Sub Add(autor As Autor) Implements IAutorService.Add
            _autorRepository.Add(autor)
        End Sub

        Public Sub Update(autor As Autor) Implements IAutorService.Update
            _autorRepository.Update(autor)
        End Sub

        Public Sub Delete(id As Integer) Implements IAutorService.Delete
            _autorRepository.Delete(id)
        End Sub
    End Class
End Namespace
