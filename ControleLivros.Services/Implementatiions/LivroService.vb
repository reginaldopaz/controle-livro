Imports System.Runtime.Remoting.Contexts
Imports ControleLivros.Data.ControleLivros.Data.Context
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports ControleLivros.Repositories.ControleLivros.Repositories.Interfaces
Imports ControleLivros.Services.ControleLivros.Services.Interfaces
Imports System.Threading.Tasks
Imports System.Data.Entity

Namespace ControleLivros.Services.Implementations
    Public Class LivroService
        Implements ILivroService

        Private ReadOnly _livroRepository As ILivroRepository
        Private ReadOnly _context As ControleLivrosContext
        Private ReadOnly _autorRepository As IAutorRepository
        Private ReadOnly _assuntoRepository As IAssuntoRepository

        'Public Sub New(livroRepository As ILivroRepository)
        '    _livroRepository = livroRepository
        'End Sub
        Public Sub New(livroRepository As ILivroRepository, context As ControleLivrosContext)
            _livroRepository = livroRepository
            _context = context
        End Sub


        Public Async Function GetAllAsync() As Task(Of IEnumerable(Of Livro)) Implements ILivroService.GetAllAsync
            Return Await Task.Run(Function() _livroRepository.GetAll())
        End Function

        Public Async Function GetByIdAsync(id As Integer) As Task(Of Livro) Implements ILivroService.GetByIdAsync
            Return Await Task.Run(Function() _livroRepository.GetById(id))
        End Function

        'Public Async Function AddAsync(livro As Livro, selectedAuthors As Integer()) As Task Implements ILivroService.AddAsync

        Public Async Function AddAsync(livro As Livro, selectedAuthors As IEnumerable(Of Integer), selectedAssuntos As IEnumerable(Of Integer)) As Task Implements ILivroService.AddAsync
            If livro.Codl = 0 Then
                Using transaction = _context.Database.BeginTransaction()
                    Try
                        livro.Autores = New List(Of Autor)
                        For Each authorId In selectedAuthors
                            Dim author = Await _context.Autores.FindAsync(authorId)
                            If author IsNot Nothing Then
                                _context.Entry(author).State = EntityState.Unchanged
                                livro.Autores.Add(author)
                            End If
                        Next

                        _context.Livros.Add(livro)
                        Await _context.SaveChangesAsync()

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            Else
                Throw New InvalidOperationException("Nao existe livro.")
            End If

        End Function

        Public Async Function UpdateAsync(livro As Livro) As Task Implements ILivroService.UpdateAsync
            ' Verifique se o livro existe no banco de dados
            Dim existingLivro = Await _context.Livros.FindAsync(livro.Codl)
            If existingLivro IsNot Nothing Then
                ' Atualize os valores do livro existente com os valores do livro fornecido
                _context.Entry(existingLivro).CurrentValues.SetValues(livro)

                ' Salve as alterações no contexto
                Await _context.SaveChangesAsync()
            Else
                Throw New InvalidOperationException("O livro não existe mais no banco de dados.")
            End If
        End Function


        Public Async Function DeleteAsync(id As Integer) As Task Implements ILivroService.DeleteAsync
            Await Task.Run(Sub() _livroRepository.Delete(id))
        End Function
        Public Async Function AddAuthorToLivroAsync(livroId As Integer, authorId As Integer) As Task Implements ILivroService.AddAuthorToLivroAsync
            Dim livro = Await _context.Livros.Include(Function(l) l.Autores).FirstOrDefaultAsync(Function(l) l.Codl = livroId)
            If livro IsNot Nothing Then
                Dim autor = Await _context.Autores.FindAsync(authorId)
                If autor IsNot Nothing Then
                    livro.Autores.Add(autor)
                    Await _context.SaveChangesAsync()
                End If
            End If
        End Function

        Public Async Function AddAssuntoToLivroAsync(livroId As Integer, assuntoId As Integer) As Task Implements ILivroService.AddAssuntoToLivroAsync
            Dim livro = Await _context.Livros.Include(Function(l) l.Assuntos).FirstOrDefaultAsync(Function(l) l.Codl = livroId)
            If livro IsNot Nothing Then
                Dim assunto = Await _context.Assuntos.FindAsync(assuntoId)
                If assunto IsNot Nothing Then
                    livro.Assuntos.Add(assunto)
                    Await _context.SaveChangesAsync()
                End If
            End If
        End Function

        Public Async Function GetAutoresByLivroIdAsync(livroId As Integer) As Task(Of IEnumerable(Of Integer)) Implements ILivroService.GetAutoresByLivroIdAsync
            Dim livro = Await _context.Livros.Include(Function(l) l.Autores).FirstOrDefaultAsync(Function(l) l.Codl = livroId)
            If livro IsNot Nothing Then
                Return livro.Autores.Select(Function(a) a.CodAu).ToList()
            End If
            Return Enumerable.Empty(Of Integer)()
        End Function

        Public Async Function GetAssuntosByLivroIdAsync(livroId As Integer) As Task(Of IEnumerable(Of Integer)) Implements ILivroService.GetAssuntosByLivroIdAsync
            Dim livro = Await _context.Livros.Include(Function(l) l.Assuntos).FirstOrDefaultAsync(Function(l) l.Codl = livroId)
            If livro IsNot Nothing Then
                Return livro.Assuntos.Select(Function(a) a.CodAs).ToList()
            End If
            Return Enumerable.Empty(Of Integer)()
        End Function


        Public Async Function UpdateAuthorsAsync(livroId As Integer, selectedAuthors As String()) As Task Implements ILivroService.UpdateAuthorsAsync
            Dim livro = Await _context.Livros.Include(Function(l) l.Autores).FirstOrDefaultAsync(Function(l) l.Codl = livroId)
            If livro IsNot Nothing Then
                ' Limpar autores existentes
                livro.Autores.Clear()
                Await _context.SaveChangesAsync()
                ' Adicionar novos autores
                For Each authorId As String In selectedAuthors
                    Dim autor As Autor = Await _context.Autores.FindAsync(Integer.Parse(authorId))
                    If autor IsNot Nothing Then
                        livro.Autores.Add(autor)
                    End If
                Next

                ' Salvar alterações
                Await _context.SaveChangesAsync()
            End If
        End Function
        Public Async Function UpdateAssuntosAsync(livroId As Integer, selectedAssuntos As String()) As Task Implements ILivroService.UpdateAssuntosAsync
            Dim livro = Await _context.Livros.Include(Function(l) l.Assuntos).FirstOrDefaultAsync(Function(l) l.Codl = livroId)
            If livro IsNot Nothing Then
                ' Limpar assuntos existentes
                livro.Assuntos.Clear()
                Await _context.SaveChangesAsync()
                ' Adicionar novos assuntos
                For Each assuntoId As String In selectedAssuntos
                    Dim assunto As Assunto = Await _context.Assuntos.FindAsync(Integer.Parse(assuntoId))
                    If assunto IsNot Nothing Then
                        livro.Assuntos.Add(assunto)
                    End If
                Next

                ' Salvar alterações
                Await _context.SaveChangesAsync()
            End If
        End Function


    End Class

End Namespace
