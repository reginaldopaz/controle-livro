
@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Autor

@Code
    ViewData("Title") = "Excluir Autor"
End Code

<h2>Excluir Autor</h2>

@Html.ValidationSummary(True, "", New With {.class = "text-danger"})

<h3>Tem certeza de que deseja excluir este autor?</h3>
<div>
    <h4>Autor</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>@Html.DisplayNameFor(Function(model) model.CodAu)</dt>
        <dd>@Html.DisplayFor(Function(model) model.CodAu)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.Nome)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Nome)</dd>
    </dl>
</div>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()

    @<div Class="form-group">
    <input type = "submit" value="Excluir" Class="btn btn-danger" />
    <a href = "@Url.Action("Index")" Class="btn btn-secondary">Cancelar</a>
</div>

End Using

<div>
    <a href="@Url.Action("Index", "Autor")" class="btn btn-secondary">
        <i class="fas fa-arrow-left"></i> Voltar à Lista
    </a>
</div>
