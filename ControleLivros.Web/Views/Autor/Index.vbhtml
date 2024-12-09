@ModelType PagedList.IPagedList(Of ControleLivros.Models.ControleLivros.Models.Entities.Autor)

@Code
    ViewData("Title") = "Lista de Autores"
End Code

<h2>@ViewData("Title")</h2>

<p>
    @Html.ActionLink("Criar Novo Autor", "Create", "Autor", Nothing, New With {.class = "btn btn-primary"})
</p>

<Table Class="table">
    <thead>
        <tr>
            <th>Nome</th>
            <th>Ações</th>
        </tr>
    </thead>
    <tbody>
        @If Model IsNot Nothing AndAlso Model.Any() Then
            For Each item In Model
                @<tr>
                    <td>@Html.DisplayFor(Function(modelItem) item.Nome)</td>
                    <td>
                        <div class="btn-group" role="group">
                            <a href="@Url.Action("Edit", "Autor", New With {.CodAu = item.CodAu})" class="btn btn-warning btn-sm">
                                <i class="fa fa-edit"></i> Editar
                            </a>
                            <a href="@Url.Action("Details", "Autor", New With {.CodAu = item.CodAu})" class="btn btn-info btn-sm">
                                <i class="fa fa-info-circle"></i> Detalhes
                            </a>
                            <a href="@Url.Action("Delete", "Autor", New With {.CodAu = item.CodAu})" class="btn btn-danger btn-sm">
                                <i class="fa fa-trash"></i> Excluir
                            </a>
                        </div>
                    </td>
                </tr>
            Next
        Else
            @<tr>
                <td colspan="2" Class="text-center">Nenhum autor encontrado.</td>
            </tr>
        End If
    </tbody>
</Table>

<div class="pagination-container">
    @If Model.PageCount > 1 Then
        @<ul Class="pagination">
            @If Model.HasPreviousPage Then
                @<li>
                    <a href="@Url.Action("Index", New With {.page = Model.PageNumber - 1})">&laquo; Anterior</a>
                </li>
            End If

            @For i As Integer = 1 To Model.PageCount
                @<li class="@(If(i = Model.PageNumber, "active", ""))">
                    <a href="@Url.Action("Index", New With {.page = i})">@i</a>
                </li>
            Next

            @If Model.HasNextPage Then
                @<li>
                    <a href="@Url.Action("Index", New With {.page = Model.PageNumber + 1})">Próximo &raquo;</a>
                </li>
            End If
        </ul>
    End If
</div>
