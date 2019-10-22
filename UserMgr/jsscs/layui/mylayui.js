//新增
$('#AddBasicData').on('click', function () {
    layer.open({
        type: 2,
        title: "新增",
        shade: [0.5],
        shadeClose: false,
        maxmin: true,
        area: ['60%', '60%'],
        offset: 'r',
        anim: 2,
        content: '/AddEntity/' + this.name,
        cancel: function (index, layrro) {
            if (confirm('确定关闭吗？当前信息会丢失。')) {
                layer.close(index);
            }
            return false;
        },
    });
});
//新增入库类型-提交
$('#SubmitBasicData').click(function () {
    var entityName = this.name;
    var content = $('#Name_' + entityName).val();
    var ParLayer = parent.layer;

    if (content.length > 0 && content.length < 21) {
        $.ajax({
            type: "POST",
            dataType: "text",
            url: "/AddEntity/" + entityName,
            data: {
                "Content": content
            },
            error: function (msg) {
                ParLayer.msg("请求失败，错误原因：" + msg, { shade: 0.3 });
            },
            success: function (res) {
                ParLayer.msg(res);
                if (res == "添加成功") {
                    parent.$("#BD" + entityName).bootstrapTable('refresh');
                    ParLayer.close(ParLayer.getFrameIndex(window.name));
                } 
            }
        });
    } else {
        ParLayer.msg('文本长度应在1~20个字符', { shade: 0.3 });
    }
});



//编辑入库类型
function EditEntity(id, entityName) {
    $.ajax({
        type: "POST",
        dateType: "json",
        url: "/EditEntity/" + entityName,
        data: {
            "ID": id
        },
        error: function (msg) {
            layer.msg('请求失败' + msg, { shade: 0.3 });
        },
        success: function (res) {
            if (res.Flag == "OK") {
                layer.open({
                    type: 2,
                    title: "更新信息",
                    shade: [0.5],
                    shadeClose: false,
                    maxmin: true,
                    area: ['60%', '60%'],
                    offset: 'r',
                    anim: 2,
                    content: '/EditEntity/' + entityName + '?ID=' + res.ID + '&Content=' + res.Content,
                    cancel: function (index, layrro) {
                        if (confirm('确定关闭吗？当前信息会丢失。')) {
                            layer.close(index);
                        }
                        return false;
                    }
                })
            }
        }
    });
};
//保存入库类型-更新提交
$('#EditBasicData').click(function () {
    var entityName = this.name;
    var content = $('#Name_' + entityName).val();
    var id = $('#ID_' + entityName).val();
    var ParLayer = parent.layer;

    if (content.length > 0 && content.length < 21) {
        $.ajax({
            type: "POST",
            dataType: "text",
            url: "/EditEntity/Update" + entityName,
            data: {
                "ID": id,
                "Content": content
            },
            error: function (msg) {
                ParLayer.msg("请求失败，错误原因：" + msg, { shade: 0.3 });
            },
            success: function (res) {
                ParLayer.msg(res);
                if (res == "更新成功") {
                    parent.$("#BD" + entityName).bootstrapTable('refresh');
                    ParLayer.close(ParLayer.getFrameIndex(window.name));
                }
            }
        });
    } else {
        ParLayer.msg('输入文本长度应在1~20个字符', { shade: 0.3 });
    }
});