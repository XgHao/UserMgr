function format_input_num(obj) {
    // 清除"数字"和"."以外的字符
    obj.value = obj.value.replace(/[^\d.]/g, "");
    // 验证第一个字符是数字
    obj.value = obj.value.replace(/^\./g, "");
    // 只保留第一个, 清除多余的
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    // 只能输入两个小数
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3');
}

$("#Length").change(function () {
    Calc_Density();
});
$("#Width").change(function () {
    Calc_Density();
});
$("#Height").change(function () {
    Calc_Density();
});
$("#Weight").change(function () {
    Calc_Density();
});

function Calc_Density() {
    var reg = /^\d+$/;
    var len = $("#Length").val();
    var wid = $("#Width").val();
    var hei = $("#Height").val();
    var wei = $("#Weight").val();
    var den = $("#MaterialDensity");
    if (reg.test(len) && reg.test(wid) && reg.test(hei) && reg.test(wei)) {
        den.val((len * wid * hei) / wei);
    }
}

$("#MaterialMin").change(function () {
    var min = Number($("#MaterialMin").val());
    var max = Number($("#MaterialMax").val());

    if (min < 0) {
        $("#ValidateMin").removeAttr("hidden");
        if (max < min) {
            $("#ValidateMax").removeAttr("hidden");
        } else {
            $("#ValidateMax").attr("hidden", "true");
        }
    } else {
        $("#ValidateMin").attr("hidden", "true");
        if (max < min) {
            $("#ValidateMax").removeAttr("hidden");
        } else {
            $("#ValidateMax").attr("hidden", "true");
        } 
    }
});

$("#MaterialMax").change(function () {
    var min = Number($("#MaterialMin").val());
    var max = Number($("#MaterialMax").val());

    if (max < min) {
        $("#ValidateMax").removeAttr("hidden");
    } else {
        $("#ValidateMax").attr("hidden", "true");
    }
});
