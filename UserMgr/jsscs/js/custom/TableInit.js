window.onload = function () {
    //初始化表格
    var Table_Url = new TableInit_Url();
    Table_Url.Init();   //URL表格

    var Table_UserGroup = new TableInit_UserGroup();
    Table_UserGroup.Init();     //用户组表格

    var Table_CheckUser = new TableInit_CheckUser();
    Table_CheckUser.Init();     //审核用户

    var Table_UserList = new TableInit_UserList();
    Table_UserList.Init();      //用户列表

    var Table_Supplier = new TableInit_Supplier();
    Table_Supplier.Init();      //供应商

    var Table_MaterialsTypes = new TableInit_MaterialsType();
    Table_MaterialsTypes.Init();    //物资种类

    var Table_MaterialList = new TableInit_MaterialList();
    Table_MaterialList.Init();      //物资列表

    var Table_WarehouseList = new TableInit_WarehouseList();
    Table_WarehouseList.Init();       //仓库表

    var Table_InventoryAreaList = new TableInit_InventoryAreaList();
    Table_InventoryAreaList.Init();     //库区表

    //“添加”按钮事件
    $("#AddUserGroup").click(function () {
        window.location.href = "/AddEntity/UserGroup";
    });
    $("#AddSupplier").click(function () {
        window.location.href = "/AddEntity/Supplier";
    });
    $("#AddMaterialType").click(function () {
        window.location.href = "/AddEntity/MaterialType";
    });
    $("#AddMaterial").click(function () {
        window.location.href ="/AddEntity/Material"
    });
    $("#AddWarehouse").click(function () {
        window.location.href = "/AddEntity/Warehouse"
    });
    $("#AddInventoryArea").click(function () {
        window.location.href = "/AddEntity/InventoryArea"
    });
};



//URL
var TableInit_Url = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#UserAccessMgr').bootstrapTable('destroy');
        //设置表格数据
        $('#UserAccessMgr').bootstrapTable({
            url: '/API/TableData/UrlMgr',
            method: 'get',
            //toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_UAM,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_UAM(row);
            },
            columns: [
                {
                    field: 'PageName',     //数据键
                    title: '页面名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'PageUrl',     //数据键
                    title: '页面URL',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'PageClass',     //数据键
                    title: '可访问用户组',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'BlackList',     //数据键
                    title: '访问黑名单',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WhiteList',     //数据键
                    title: '访问白名单',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_UAM,
                    formatter: operateFormatter_UAM,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_UAM = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_UAM = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
    };


    //按钮定义
    function operateFormatter_UAM(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_UAM" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_UAM = {
        'click #btnEdit_UAM': function (e, value, row, index) {
            window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//用户组
var TableInit_UserGroup = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#UserGroupMgr').bootstrapTable('destroy');
        //设置表格数据
        $('#UserGroupMgr').bootstrapTable({
            url: '/API/TableData/uGroupMgr',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_UGP,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_UGP(row);
            },
            columns: [
                {
                    field: 'UserGroupID',     //数据键
                    title: '用户组ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupName',     //数据键
                    title: '名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupNo',     //数据键
                    title: '编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupClass',     //数据键
                    title: '访问权限等级',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupDesc',     //数据键
                    title: '描述',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Creater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: '',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_UGP,
                    formatter: operateFormatter_UGP,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_UGP = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_UGP = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(row);
        //console.log(row["PageID"]);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/UserGroup?Id=" + row.UserGroupID;
    };


    //按钮定义
    function operateFormatter_UGP(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_UGP" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnDelete_UGP" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-trash"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_UGP = {
        'click #btnEdit_UGP': function (e, value, row, index) {
            window.location.href = "/EditEntity/UserGroup?Id=" + row.UserGroupID;
        },
        'click #btnDelete_UGP': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//审核用户列表
var TableInit_CheckUser = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#CheckUser').bootstrapTable('destroy');
        //设置表格数据
        $('#CheckUser').bootstrapTable({
            url: '/API/TableData/CheckUserMgr',
            method: 'get',
            //toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_CU,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_CU(row);
            },
            columns: [
                {
                    field: 'UserID',     //数据键
                    title: '用户ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupID',     //数据键
                    title: '所属用户组',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserName',     //数据键
                    title: '用户名',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserEmail',     //数据键
                    title: '注册邮箱',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserDesc',     //数据键
                    title: '用户说明',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'IsUse',     //数据键
                    title: '审核状态',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_CU,
                    formatter: operateFormatter_CU,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_CU = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_CU = function (row) {
        window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
    };


    //按钮定义
    function operateFormatter_CU(value, row, index) {
        return [
            '<div class="btn-group">',
            '<button id="btnYES_CU" title="审核" class="btn btn-success btn-circle" singleSelected=true>',
            '<i class="fa fa-check"></i>',
            '</button>',
            '<button id="btnNO_CU" title="拒绝" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-close"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_CU = {
        'click #btnYES_CU': function (e, value, row, index) {
            //审核通过
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/API/AJAX/CheckUser",
                data: {
                    "UserID": row['UserID']
                },
                error: function (msg) {
                    alert("操作失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#CheckUser').bootstrapTable('remove', {
                            field: 'UserID',
                            values: [row.UserID]
                        });
                        Notiy("用户 " + row['UserName'] + " 已通过审核", "success");
                    }
                    else if (res == "Error") {
                        Notiy("审核失败", "danger");
                    } else {
                        Notiy("你没有权限执行该操作", "warning");
                    }
                }
            });
        },
        'click #btnNO_CU': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/API/AJAX/DenyUser",
                data: {
                    "UserID": row['UserID']
                },
                error: function (msg) {
                    alert("操作失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#CheckUser').bootstrapTable('remove', {
                            field: 'UserID',
                            values: [row.UserID]
                        });
                        Notiy("用户 " + row['UserName'] + " 注册请求已被拒绝", "success");
                    }
                    else if (res == "Error") {
                        Notiy("拒绝失败", "danger");
                    }
                    else {
                        Notiy("你没有权限执行该操作", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//用户列表
var TableInit_UserList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#UserList').bootstrapTable('destroy');
        //设置表格数据
        $('#UserList').bootstrapTable({
            url: '/API/TableData/UserList',
            method: 'get',
            //toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_UL,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_UL(row);
            },
            columns: [
                {
                    field: 'UserID',     //数据键
                    title: '用户ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserGroupID',     //数据键
                    title: '所属用户组',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserName',     //数据键
                    title: '用户名',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserDesc',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'UserEmail',     //数据键
                    title: '注册邮箱',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'IsUse',     //数据键
                    title: '是否通过审核',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_UL,
                    formatter: operateFormatter_UL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_UL = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_UL = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/UserList?Id=" + row.UserID;
    };


    //按钮定义
    function operateFormatter_UL(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_UL" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_UL = {
        'click #btnEdit_UL': function (e, value, row, index) {
            //console.log(e);
            //console.log(value);
            //console.log(row);
            //console.log(index);
            console.log(row.IsUse);
            if (row.IsUse) {
                window.location.href = "/EditEntity/UserList?Id=" + row.UserID;
            }
            else {
                Notiy("用户还未审核", "warning");
            }
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//供应商
var TableInit_Supplier = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#SupplierList').bootstrapTable('destroy');
        //设置表格数据
        $('#SupplierList').bootstrapTable({
            url: '/API/TableData/Supplier',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_SL,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_SL(row);
            },
            columns: [
                {
                    field: 'SupplierID',     //数据键
                    title: '供应商ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierNo',     //数据键
                    title: '供应商编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierName',     //数据键
                    title: '供应商名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierPhoNum',     //数据键
                    title: '联系方式',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierEmail',     //数据键
                    title: '电子邮箱',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SupplierRemark',     //数据键
                    title: '备注信息',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_SL,
                    formatter: operateFormatter_SL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_SL = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_SL = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/UserList?Id=" + row.UserID;
    };


    //按钮定义
    function operateFormatter_SL(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_SL" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_SL = {
        'click #btnEdit_SL': function (e, value, row, index) {
            //console.log(e);
            //console.log(value);
            //console.log(row);
            //console.log(index);
            console.log(row.IsUse);
            if (row.IsUse) {
                window.location.href = "/EditEntity/UserList?Id=" + row.UserID;
            }
            else {
                Notiy("用户还未审核", "warning");
            }
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//物资种类
var TableInit_MaterialsType = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#MaterialsType').bootstrapTable('destroy');
        //设置表格数据
        $('#MaterialsType').bootstrapTable({
            url: '/API/TableData/MaterialsType',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_MT,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_MT(row);
            },
            columns: [
                {
                    field: 'MaterialTypeID',     //数据键
                    title: '物资种类ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialTypeNo',     //数据键
                    title: '物资种类编码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialTypeName',     //数据键
                    title: '物资种类名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialTypeRoot',     //数据键
                    title: '父类',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Creater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'DataVersion',     //数据键
                    title: '数据版本',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'MaterialTypePrice',     //数据键
                    title: '价格',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '80px',
                    align: 'center',
                    events: operateEvents_MT,
                    formatter: operateFormatter_MT,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_MT = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_MT = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/MaterialType?Id=" + row.MaterialTypeID;
    };


    //按钮定义
    function operateFormatter_MT(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_MT" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_MT = {
        'click #btnEdit_MT': function (e, value, row, index) {
            //console.log(e);
            //console.log(value);
            //console.log(row);
            //console.log(index);
            console.log(row.IsUse);
            window.location.href = "/EditEntity/MaterialType?Id=" + row.MaterialTypeID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//所有物资列表
var TableInit_MaterialList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#MaterialList').bootstrapTable('destroy');
        //设置表格数据
        $('#MaterialList').bootstrapTable({
            url: '/API/TableData/MaterialList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_ML,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_ML(row);
            },
            columns: [
                {
                    field: 'MaterialSizeID',     //数据键
                    title: '物资ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'MaterialTypeID',     //数据键
                    title: '物资种类ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'SizeCode',     //数据键
                    title: '规格代码',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Detail',     //数据键
                    title: '详细描述',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Unit',     //数据键
                    title: '单位',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Length',     //数据键
                    title: '长(mm)',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Width',     //数据键
                    title: '宽(mm)',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Height',     //数据键
                    title: '高(mm)',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: '质量(g)',     //数据键
                    title: '单位',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialDensity',     //数据键
                    title: '密度(g/(mm^3))',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialModel',     //数据键
                    title: '物资型号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ParcelUnit',     //数据键
                    title: '小件单位',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'ParcelMeasure',     //数据键
                    title: '小件计量',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialMin',     //数据键
                    title: '物料最低值',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialMax',     //数据键
                    title: '物料最高值',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'MaterialContainer',     //数据键
                    title: '容器',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'IsCKD',     //数据键
                    title: '是否组合商品',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Creater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    align: 'center',
                    events: operateEvents_ML,
                    formatter: operateFormatter_ML,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_ML = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_ML = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        //console.log(row.PageID);
        window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
    };


    //按钮定义
    function operateFormatter_ML(value, row, index) {
        //console.log(row);
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_UAM" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_ML = {
        'click #btnEdit_UAM': function (e, value, row, index) {
            window.location.href = "/EditEntity/AccessMgr?Id=" + row.PageID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//仓库列表
var TableInit_WarehouseList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#WarehouseList').bootstrapTable('destroy');
        //设置表格数据
        $('#WarehouseList').bootstrapTable({
            url: '/API/TableData/WarehouseList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_WL,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_WL(row);
            },
            columns: [
                {
                    field: 'WarehouseID',     //数据键
                    title: '仓库ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'WarehouseNo',     //数据键
                    title: '仓库编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WarehouseName',     //数据键
                    title: '仓库名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Enable',     //数据键
                    title: '是否启用',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Remark',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WarehouseType',     //数据键
                    title: '仓库类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'OtherInfo',     //数据键
                    title: '其他信息',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Creater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    align: 'center',
                    events: operateEvents_WL,
                    formatter: operateFormatter_WL,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_WL = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_WL = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        console.log(row);
        console.log(row.WarehouseID);
        window.location.href = "/EditEntity/Warehouse?Id=" + row.WarehouseID;
    };


    //按钮定义
    function operateFormatter_WL(value, row, index) {
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_WL" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_WL = {
        'click #btnEdit_WL': function (e, value, row, index) {
            console.log(row);
            window.location.href = "/EditEntity/Warehouse?Id=" + row.WarehouseID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};

//库区列表
var TableInit_InventoryAreaList = function () {
    var TableInit = new Object();
    //初始化Table
    TableInit.Init = function () {
        //清空表格数据
        $('#InventoryAreaList').bootstrapTable('destroy');
        //设置表格数据
        $('#InventoryAreaList').bootstrapTable({
            url: '/API/TableData/InventoryAreaList',
            method: 'get',
            toolbar: '#toolbar',
            striped: false,
            cache: true,
            pagination: true,   //分页
            pageNumber: 1,   //分页起始页
            pageSize: 10,    //分页显示的条数
            pageList: [10, 25, 50, 'All'],    //分页可以显示的条数
            sortable: true,     //排序
            sortOrder: 'asc',    //排序方式
            queryParams: TableInit.queryParams_IA,  //传递参数
            sidePagination: 'server',    //分页类型“服务端”还是“客户端”
            showextendedpagination: 'true',
            totalnotfilteredfield: "totalNotFiltered",
            search: true,   //搜索
            strictSearch: true,
            showColumns: true,  //设置可以显示的列
            minimumCountColumns: 2,  //最少显示的列数
            showRefresh: true,      //刷新按钮
            clickToSelect: true,    //点击选择
            singleSelect: true,     //单选
            //showFooter: true,       //设置表底
            //height: "600",
            //双击选择方法
            onDblClickRow: function (row) {
                Dbclick_IA(row);
            },
            columns: [
                {
                    field: 'InventoryAreaID',     //数据键
                    title: '库区ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'InventoryAreaName',     //数据键
                    title: '库区名称',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryAreaNo',     //数据键
                    title: '库区编号',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'InventoryAreaType',     //数据键
                    title: '库区类型',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'WarehouseID',     //数据键
                    title: '仓库ID',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Enable',     //数据键
                    title: '是否开放',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Remark',     //数据键
                    title: '备注',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'OtherInfo',     //数据键
                    title: '其他信息',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                }, {
                    field: 'Creater',     //数据键
                    title: '创建人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'CreateTime',     //数据键
                    title: '创建时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'Changer',     //数据键
                    title: '修改人',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'ChangeTime',     //数据键
                    title: '修改时间',    //列名
                    sortable: true,     //是否允许排序
                    align: 'center',     //居中
                    visible: false
                }, {
                    field: 'operate',
                    title: '操作',
                    width: '100px',
                    align: 'center',
                    events: operateEvents_IA,
                    formatter: operateFormatter_IA,
                }
            ],
        });
    };

    //得到查询的参数
    TableInit.queryParams_IA = function (params) {
        return {
            "offset": params.offset,    //从第几条数据开始
            "limit": params.limit,      //每页显示的数据条数
            "keyword": params.search,   //搜索条件
            "sortName": params.sort,    //排序列
            "sortOrder": params.order,  //排序方式
        }
        return params;
    };

    //双击选中行事件
    Dbclick_IA = function (row) {
        //对象转换为json
        //data = JSON.stringify(row);
        //console.log(data);
        console.log(row);
        console.log(row.WarehouseID);
        window.location.href = "/EditEntity/InventoryArea?Id=" + row.InventoryAreaID;
    };


    //按钮定义
    function operateFormatter_IA(value, row, index) {
        return [
            '<div class="btn-group">',
            '<button id="btnEdit_IA" class="btn btn-info btn-circle" singleSelected=true>',
            '<i class="fa fa-pencil"></i>',
            '</button>',
            '<button id="btnRefresh_UAM" class="btn btn-danger btn-circle" singleSelected=true>',
            '<i class="fa fa-refresh"></i>',
            '</button>',
            '</div>'
        ].join('');
    };

    //按钮事件定义
    window.operateEvents_IA = {
        'click #btnEdit_IA': function (e, value, row, index) {
            console.log(row);
            window.location.href = "/EditEntity/InventoryArea?Id=" + row.InventoryAreaID;
        },
        'click #btnRefresh_UAM': function (e, value, row, index) {
            //移除该项
            $.ajax({
                type: "POST",
                dataType: "text",
                url: "/Home/DeleteUser",
                data: {
                    "UserId": row['Id']
                },
                error: function (msg) {
                    alert("删除失败，错误原因：" + msg);
                },
                success: function (res) {
                    if (res == "OK") {
                        $('#UserRoleTable').bootstrapTable('remove', {
                            field: 'Id',
                            values: [row.Id]
                        });
                        Notiy("删除" + row['UserName'] + "用户成功", "succedd");
                    }
                    else if (res == "Error") {
                        Notiy("删除失败", "danger");
                    }
                    else {
                        Notiy("当前用户没有权限", "warning");
                    }
                }
            });
        }
    };

    return TableInit;
};




//通知消息
function Notiy(msg, type) {
    var notiy = "<div class='alert alert-" + type + " alert-dismissable'><button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" + msg + "</div>";

    $("#notiy").html(notiy);
};