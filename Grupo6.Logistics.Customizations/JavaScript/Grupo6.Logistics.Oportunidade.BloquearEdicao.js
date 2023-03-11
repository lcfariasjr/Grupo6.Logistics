if (typeof (LogisticsSecundario) == "undefined") { LogisticsSecundario = {} }
if (typeof (LogisticsSecundario.Oportunidade) == "undefined") { LogisticsSecundario.Oportunidade = {} }

LogisticsSecundario.Oportunidade = {
    Onload: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var integracao = formContext.getAttribute("gp6_integracao").getValue();


        if (integracao) {

            formContext.ui.setFormNotification("Este formulário está bloqueado e não pode ser editado", "ERROR");

            // Desativar todos os campos do formulário para que não possam ser editados
            formContext.getAttribute().forEach(function (attribute, index) {
                attribute.controls.forEach(function (control, index) {
                    control.setDisabled(true);
                });
            });
        }
    }



}