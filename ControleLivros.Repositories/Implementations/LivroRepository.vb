Imports ControleLivros.Data.ControleLivros.Data.Context
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports System.Data.Entity

Namespace ControleLivros.Repositories.Implementations
    Public Class LivroRepository
        Implements ILivroRepository

        Private ReadOnly _context As ControleLivrosContext

        Public Sub New(context As ControleLivrosContext)
            _context = context
        End Sub

        Public Function GetAll() As IEnumerable(Of Livro) Implements ILivroRepository.GetAll
            Return _context.Livros.ToList()
        End Function

        Public Function GetById(id As Integer) As Livro Implements ILivroRepository.GetById
            Return _context.Livros.Find(id)
        End Function

        Public Sub Add(livro As Livro) Implements ILivroRepository.Add

            _context.Livros.Add(livro)
            _context.SaveChanges()

        End Sub

        Public Sub Update(livro As Livro) Implements ILivroRepository.Update
            _context.Entry(livro).State = EntityState.Modified
            _context.SaveChanges()
        End Sub

        Public Sub Delete(id As Integer) Implements ILivroRepository.Delete
            Dim livro = _context.Livros.Find(id)
            If livro IsNot Nothing Then
                _context.Livros.Remove(livro)
                _context.SaveChanges()
            End If
        End Sub
    End Class
End Namespace
