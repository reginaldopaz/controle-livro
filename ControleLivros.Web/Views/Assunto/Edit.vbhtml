@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Assunto

@Code
    ViewData("Title") = "Editar Assunto"
End Code

<h2>Editar Assunto</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @<div class="form-horizontal">
        <h4> Assunto</h4>
        <hr />

        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        @Html.HiddenFor(Function(m) m.CodAs)

        <div class="form-group">
            @Html.LabelFor(Function(m) m.CodAs, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <p class="form-control-static">@Html.DisplayFor(Function(m) m.CodAs)</p>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(m) m.Descricao, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(m) m.Descricao, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(m) m.Descricao, "", New With {.class = "text-danger"})
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
