@ModelType IEnumerable(Of ControleLivros.Models.ControleLivros.Models.Entities.Assunto)

@Code
    ViewData("Title") = "Lista de Assuntos"
End Code

<h2>@ViewData("Title")</h2>

<p>
    @Html.ActionLink("Incluir Assunto", "Create", "Assunto", Nothing, New With {.class = "btn btn-primary"})
</p>

<Table Class="table">
    <thead>
        <tr>
            <th>@Html.DisplayNameFor(Function(model) model.Descricao)</th>
            <th>Ações</th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td>@Html.DisplayFor(Function(modelItem) item.Descricao)</td>
                <td>
                    <div class="btn-group" role="group">
                        <a href="@Url.Action("Edit", "Assunto", New With {.CodAs = item.CodAs})" class="btn btn-warning btn-sm">
                            <i class="fa fa-edit"></i> Editar
                        </a>
                        <a href="@Url.Action("Details", "Assunto", New With {.CodAs = item.CodAs})" class="btn btn-info btn-sm">
                            <i class="fa fa-info-circle"></i> Detalhes
                        </a>
                        <a href="@Url.Action("Delete", "Assunto", New With {.CodAs = item.CodAs})" class="btn btn-danger btn-sm">
                            <i class="fa fa-trash"></i> Excluir
                        </a>
                    </div>
                </td>
            </tr>
        Next
    </tbody>
</Table>
