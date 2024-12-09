Imports ControleLivros.Data.ControleLivros.Data.Context
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports System.Data.Entity

Namespace ControleLivros.Repositories.Implementations
    Public Class AssuntoRepository
        Implements IAssuntoRepository

        Private ReadOnly _context As ControleLivrosContext

        Public Sub New(context As ControleLivrosContext)
            _context = context
        End Sub

        Public Function GetAll() As IEnumerable(Of Assunto) Implements IAssuntoRepository.GetAll
            Return _context.Assuntos.ToList()
        End Function

        Public Function GetById(id As Integer) As Assunto Implements IAssuntoRepository.GetById
            Return _context.Assuntos.Find(id)
        End Function

        Public Sub Add(assunto As Assunto) Implements IAssuntoRepository.Add
            _context.Assuntos.Add(assunto)
            _context.SaveChanges()
        End Sub

        Public Sub Update(assunto As Assunto) Implements IAssuntoRepository.Update
            _context.Entry(assunto).State = EntityState.Modified
            _context.SaveChanges()
        End Sub

        Public Sub Delete(id As Integer) Implements IAssuntoRepository.Delete
            Dim assunto = _context.Assuntos.Find(id)
            If assunto IsNot Nothing Then
                _context.Assuntos.Remove(assunto)
                _context.SaveChanges()
            End If
        End Sub
    End Class
End Namespace
