@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Livro

@Code
    ViewData("Title") = "Cadastrar Livro"
End Code

<h2>@ViewData("Title")</h2>

<div id="mensagem" class="alert alert-success" style="display:none;">
    Livro salvo com sucesso! Agora você pode adicionar autores e assuntos.
</div>

@Using Html.BeginForm("Create", "Livro", FormMethod.Post, New With {.id = "livroForm"})

    @Html.AntiForgeryToken()
    @<input type="hidden" id="livroId" name="livroId" value="@ViewBag.LivroId" />

    @<div class="form-horizontal">
        <h4>Livro</h4>
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        <table class="table">
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.Titulo, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Titulo, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Titulo, "", New With {.class = "text-danger"})
                </td>
                <td>
                    @Html.LabelFor(Function(model) model.Editora, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Editora, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Editora, "", New With {.class = "text-danger"})
                </td>
                <td>
                    @Html.LabelFor(Function(model) model.Edicao, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Edicao, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Edicao, "", New With {.class = "text-danger"})
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.AnoPublicacao, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.AnoPublicacao, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.AnoPublicacao, "", New With {.class = "text-danger"})
                </td>
                <td colspan="2">
                    @Html.LabelFor(Function(model) model.Valor, htmlAttributes:=New With {.class = "control-label"})
                    @Html.EditorFor(Function(model) model.Valor, New With {.htmlAttributes = New With {.class = "form-control", .id = "valor"}})
                    @Html.ValidationMessageFor(Function(model) model.Valor, "", New With {.class = "text-danger"})
                </td>
            </tr>
        </table>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <button type="submit" class="btn btn-primary">
                    <i class="fa fa-save"></i> Salvar
                </button>

                <a href="@Url.Action("Index", "Livro")" class="btn btn-secondary">
                    <i class="fa fa-list"></i> Voltar
                </a>
            </div>
        </div>
    </div>

End Using

<div id="addSection" style="display:@(If(ViewBag.ShowAddSection, "block", "none"));">
    <h4>Adicionar Autor e Assunto</h4>

    @Using Html.BeginForm("AddAuthorAssunto", "Livro", FormMethod.Post, New With {.id = "addForm"})

        @Html.AntiForgeryToken()

        @<input type="hidden" id="livroId" name="livroId" value="@ViewBag.LivroId" />

        @<div class="form-group">
            <table>
                <tr>
                    <td style="padding-right: 10px; width: 250px;">
                        <div>
                            @Html.DropDownList("selectedAuthor", New SelectList(DirectCast(ViewBag.Autores, IEnumerable(Of SelectListItem)), "Value", "Text"), "Selecione um Autor", New With {.class = "form-control"})
                        </div>
                    </td>
                    <td style="padding-right: 10px; width: 250px;">
                        <div>
                            @Html.DropDownList("selectedAssunto", New SelectList(DirectCast(ViewBag.Assuntos, IEnumerable(Of SelectListItem)), "Value", "Text"), "Selecione um Assunto", New With {.class = "form-control"})
                        </div>
                    </td>
                    <td>
                        <button type="button" class="btn btn-secondary" id="adicionar">
                            <i class="fa fa-plus"></i> Adicionar
                        </button>
                    </td>
                </tr>
            </table>
        </div>


    End Using

    <div id="gridViewSection">

        <table class="table table-bordered">
            <thead>
                <tr>
                    <th>Autor</th>
                    <th>Assunto</th>
                </tr>
            </thead>
            <tbody id="gridViewBody">
                @* Linhas serão preenchidas dinamicamente *@
            </tbody>
        </table>
    </div>

</div>

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/5.0.6/jquery.inputmask.min.js"></script>

    <script type="text/javascript">

       $(document).ready(function () {
        // Configuração global para adicionar o token antifalsificação a todas as requisições POST
        $.ajaxSetup({
            beforeSend: function (xhr, settings) {
                if (settings.type === 'POST') {
                    var token = $('input[name="__RequestVerificationToken"]').val();
                    xhr.setRequestHeader('RequestVerificationToken', token);
                }
            }
        });

        $('#valor').inputmask('decimal', {
            radixPoint: ',',
            groupSeparator: '.',
            autoGroup: true,
            digits: 2,
            rightAlign: false,
            removeMaskOnSubmit: true
        });

        // Ação para salvar o livro
        $('#salvarLivro').click(function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: '@Url.Action("Create", "Livro")',
                data: $('#livroForm').serialize(),
                success: function (response) {
                    if (response.success) {
                        $('#mensagem').show();
                        $('#addSection').show();
                        $('#livroId').val(response.livroId); // Armazena o ID do livro salvo
                        $('#salvarLivro').prop('disabled', true);
                    } else {
                        alert(response.message || 'Erro ao salvar o livro.');
                    }
                },
                error: function () {
                    alert('Erro ao tentar salvar o livro.');
                }
            });
        });

        // Ação para adicionar autor e assunto
        $('#adicionar').click(function (e) {
            e.preventDefault();
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                type: 'POST',
                url: '@Url.Action("AddAuthorAssunto", "Livro")',
                data: {
                    __RequestVerificationToken: token, // Inclua o token no data
                    livroId: $('#livroId').val(),
                    selectedAuthor: $('#selectedAuthor').val(),
                    selectedAssunto: $('#selectedAssunto').val()
                },
                success: function (response) {
                    if (response.success) {
                        $('#gridViewBody').append('<tr><td>' + response.autorNome + '</td><td>' + response.assuntoDescricao + '</td></tr>');
                    } else {
                        alert(response.message || 'Erro ao adicionar autor e assunto aqui.');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert('Erro ao tentar adicionar autor e assunto Erro: ' + textStatus + ' - ' + errorThrown);
                    console.log(jqXHR.responseText);
                }
            });
        });


    });
    </script>

End Section
<style>
    .table thead th {
        background-color: #007bff; /* Cor de fundo do cabeçalho */
        color: white; /* Cor do texto do cabeçalho */
        text-align: center; /* Centraliza o texto */
        padding: 10px; /* Espaçamento interno */
    }

    /* Estilo zebra para as linhas do corpo da tabela */
    .table tbody tr:nth-child(odd) {
        background-color: #f2f2f2; /* Cor de fundo para linhas ímpares */
    }

    .table tbody tr:nth-child(even) {
        background-color: #ffffff; /* Cor de fundo para linhas pares */
    }

    /* Estilo para bordas da tabela */
    .table, .table th, .table td {
        border: 1px solid #dddddd; /* Cor das bordas */
        border-collapse: collapse; /* Colapsa bordas adjacentes */
        padding: 8px; /* Espaçamento interno das células */
        text-align: left; /* Alinha o texto à esquerda */
    }

        /* Adiciona um hover effect nas linhas */
        .table tbody tr:hover {
            background-color: #e9ecef; /* Cor de fundo ao passar o mouse */
        }

</style>