@ModelType IEnumerable(Of ControleLivros.Models.ControleLivros.Models.Entities.Livro)

@Code
    ViewData("Title") = "Index"
End Code

<h2>Livros</h2>

<div class="d-flex mb-3">
    @Using Html.BeginForm("Create", "Livro", FormMethod.Get, New With {.class = "mr-2"})
        @<button type="submit" class="btn btn-primary">
            <i class="fas fa-plus"></i> Incluir Livro
        </button>
    End Using
    <a href="@Url.Action("Index", "Visao")" class="btn btn-secondary">
        <i class="fas fa-chart-bar"></i> Ver Relatório
    </a>
</div>

<Table Class="table">
    <thead>
        <tr>
            <th>@Html.DisplayNameFor(Function(model) model.Titulo)</th>
            <th>@Html.DisplayNameFor(Function(model) model.Editora)</th>
            <th>@Html.DisplayNameFor(Function(model) model.AnoPublicacao)</th>
            <th>@Html.DisplayNameFor(Function(model) model.Valor)</th>
            <th> Ações</th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td>@Html.DisplayFor(Function(modelItem) item.Titulo)</td>
                <td>@Html.DisplayFor(Function(modelItem) item.Editora)</td>
                @*<td>@Html.DisplayFor(Function(modelItem) item.Edicao)</td>*@
                <td>@Html.DisplayFor(Function(modelItem) item.AnoPublicacao)</td>
                <td>@Html.DisplayFor(Function(modelItem) item.Valor)</td>
                <td>
                    <div class="btn-group" role="group">
                        <a href="@Url.Action("Details", "Livro", New With {.Codl = item.Codl})" class="btn btn-info btn-sm">
                            <i class="fas fa-info-circle"></i> Detalhes
                        </a>
                        <a href="@Url.Action("Edit", "Livro", New With {.Codl = item.Codl})" class="btn btn-warning btn-sm">
                            <i class="fas fa-edit"></i> Editar
                        </a>
                        <a href="@Url.Action("Delete", "Livro", New With {.Codl = item.Codl})" class="btn btn-danger btn-sm">
                            <i class="fas fa-trash"></i> Excluir
                        </a>

                    </div>
                </td>
            </tr>
        Next
    </tbody>
</Table>

