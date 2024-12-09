Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Services.ControleLivros.Services.Interfaces
Imports ControleLivros.Models
Imports System.Threading.Tasks



Namespace ControleLivros.Services.Implementations
    Public Class AssuntoService
        Implements IAssuntoService

        Private ReadOnly _assuntoRepository As IAssuntoRepository

        Public Sub New(assuntoRepository As IAssuntoRepository)
            _assuntoRepository = assuntoRepository
        End Sub

        Public Function GetAll() As IEnumerable(Of Assunto) Implements IAssuntoService.GetAll
            Return _assuntoRepository.GetAll()
        End Function

        Public Function GetById(id As Integer) As Assunto Implements IAssuntoService.GetById
            Return _assuntoRepository.GetById(id)
        End Function

        Public Sub Add(assunto As Assunto) Implements IAssuntoService.Add
            _assuntoRepository.Add(assunto)
        End Sub

        Public Sub Update(assunto As Assunto) Implements IAssuntoService.Update
            _assuntoRepository.Update(assunto)
        End Sub

        Public Sub Delete(id As Integer) Implements IAssuntoService.Delete
            _assuntoRepository.Delete(id)
        End Sub
    End Class
End Namespace
