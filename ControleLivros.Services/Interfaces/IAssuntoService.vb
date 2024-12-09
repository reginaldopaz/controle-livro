Imports ControleLivros.Models
Imports ControleLivros.Models.ControleLivros.Models.Entities
Imports System.Threading.Tasks

Namespace ControleLivros.Services.Interfaces
    Public Interface IAssuntoService
        Function GetAll() As IEnumerable(Of Assunto)
        Function GetById(id As Integer) As Assunto
        Sub Add(assunto As Assunto)
        Sub Update(assunto As Assunto)
        Sub Delete(id As Integer)
    End Interface
    'Public Interface IAssuntoService
    '    Function GetAllAsync() As Task(Of IEnumerable(Of Assunto))
    '    Function GetByIdAsync(id As Integer) As Task(Of Assunto)
    '    Function AddAsync(assunto As Assunto) As Task
    '    Function UpdateAsync(assunto As Assunto) As Task
    '    Function DeleteAsync(id As Integer) As Task
    'End Interface
End Namespace
