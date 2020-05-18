var RiskCompiler = RiskCompiler || {};

RiskCompiler.setFocus = function (element) {
    element.focus();
};

RiskCompiler.setDocumentTitle = function (title) {
    document.title = title;
};

RiskCompiler.modifyDxCalendar = function () {
    var buttons = document.querySelectorAll(".dxbs-calendar > .card-footer > button"); // select specific child elements

    for (var button of buttons) {
        button.setAttribute("class", "btn btn-outline-primary");
    }
};