$('#layui').on('click', function () {
    layer.open({
        type: 2,
        title: "新增供应商",
        shade: [0.5],
        shadeClose: true,
        maxmin: true,
        area: ['60%', '60%'],
        offset: 'r',
        anim: 2,
        content: '/AddEntity/Supplier',
        cancel: function (index, layrro) {
            if (confirm('确定关闭吗？当前的信息会丢失。')) {
                layer.close(index);
            }
            return false;
        }
    });
});