Imports ControleLivros.Data.ControleLivros.Data.Context
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports System.Data.Entity

Namespace ControleLivros.Repositories.Implementations
    Public Class AutorRepository
        Implements IAutorRepository

        Private ReadOnly _context As ControleLivrosContext

        Public Sub New(context As ControleLivrosContext)
            _context = context
        End Sub

        Public Function GetAll() As IEnumerable(Of Autor) Implements IAutorRepository.GetAll
            Return _context.Autores.ToList()
        End Function

        Public Function GetById(id As Integer) As Autor Implements IAutorRepository.GetById
            Return _context.Autores.Find(id)
        End Function

        Public Sub Add(autor As Autor) Implements IAutorRepository.Add
            _context.Autores.Add(autor)
            _context.SaveChanges()
        End Sub

        Public Sub Update(autor As Autor) Implements IAutorRepository.Update
            _context.Entry(autor).State = EntityState.Modified
            _context.SaveChanges()
        End Sub

        Public Sub Delete(id As Integer) Implements IAutorRepository.Delete
            Dim autor = _context.Autores.Find(id)
            If autor IsNot Nothing Then
                _context.Autores.Remove(autor)
                _context.SaveChanges()
            End If
        End Sub
    End Class
End Namespace
