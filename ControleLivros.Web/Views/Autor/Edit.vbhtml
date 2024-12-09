@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Autor

@Code
    ViewData("Title") = "Editar Autor"
End Code

<h2>Editar Autor</h2>

@Using Html.BeginForm("Edit", "Autor", FormMethod.Post, New With {.CodAu = Model.CodAu})
    @Html.AntiForgeryToken()
    @<div class="form-horizontal">
        <h4> Autor</h4>
        <hr />

        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        @Html.HiddenFor(Function(m) m.CodAu)

        <div class="form-group">
            @Html.LabelFor(Function(m) m.CodAu, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <p class="form-control-static">@Html.DisplayFor(Function(m) m.CodAu)</p>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Nome, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(m) m.Nome, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(m) m.Nome, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Salvar" class="btn btn-primary" />
                @Html.ActionLink("Cancelar", "Index", Nothing, New With {.class = "btn btn-default"})
            </div>
        </div>
    </div>
End Using

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
