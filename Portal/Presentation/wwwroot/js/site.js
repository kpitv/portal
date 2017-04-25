// Write your Javascript code.

function AddProperty() {
    $("#assetTypeProperties").append('<div class="form-group">');
    $("#assetTypeProperties .form-group:last").append('<input class="form-control"><button class="btn btn-default removeProperty glyphicon glyphicon-remove" type="button"></button>');
    $("#assetTypeProperties .form-group:last .removeProperty").click(RemoveProperty);
}

function CreateAssetType() {
    var assetType = {
        Name: $("#assetTypeName > input").val(),
        Properties: []
    };
    $("#assetTypeProperties input").each(function () {
        assetType.Properties.push($(this).val());
    });
    $.ajax({
        url: "/Assets/CreateType",
        type: "post",
        data: assetType
    });
}

function RemoveProperty() {
    $(this).parent().remove();
}

$(document).ready(function () {
    $("#addPropertyButton").click(AddProperty);
    $("#createAssetType").click(CreateAssetType);
    $(".removeProperty").click(RemoveProperty);
})