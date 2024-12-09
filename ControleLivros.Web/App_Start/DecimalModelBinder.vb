Imports System.Globalization
Imports System.Web.Mvc

Public Class DecimalModelBinder
    Implements IModelBinder

    Public Function BindModel(controllerContext As ControllerContext, bindingContext As ModelBindingContext) As Object Implements IModelBinder.BindModel
        Dim value As ValueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName)
        Dim result As Decimal

        If Decimal.TryParse(value.AttemptedValue, NumberStyles.Any, CultureInfo.CurrentCulture, result) Then
            Return result
        End If

        Return 0D
    End Function
End Class
