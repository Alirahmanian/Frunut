var ItemGroups = []

//=======================================
//=======================================
function LoadItemGroups(element)
{
    if (ItemGroups.length === 0)
    {
        $.ajax
            ({
                type: "GET",
                url: '/Orders/GetItemGroups',
                success: function (data)
                {
                    ItemGroups = data;
                  renderItemGroups(element);
                },
                error: function (xhr, status, error) {
                    console.log(error);
                    console.log(errxhr.responseTextor);
                }
            })
   }
   else
   {
        renderItemGroups(element);
   }
}
//=======================================
//=======================================
function LoadItems(ItemGroup) {
  $.ajax({
          type: "GET",
          url: "/Orders/GetItemsByGroupID",
          data: { 'groupId': $(ItemGroup).val() },
          success: function (data) {
              renderItems($('#Item'), data);
         },
         error: function (error) {
          console.log(error);
         }
    })
}
//=======================================
//=======================================
function LoadWarehouses(Item) {
    $.ajax({
        type: "GET",
        url: "/Orders/GetItemWarehousesByItemID",
        data: { 'itemId': $(Item).val() },
        success: function (data) {
            renderWarehouses($('#Warehouse'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
//=======================================
//=======================================
function LoadOrderDetails(Order) {

    if ($("#HOrderID").val() != "")
    {
        $.ajax({
            type: "GET",
            url: "/Orders/GetOrderDetailsForAjax",
            data: { 'Id': $("#HOrderID").val() },
            success: function (data) {
                renderOrderDetails($('#OrderDetailsList'), data);
            },
            error: function (error) {
                console.log(error);
            }
        })
    }
}
//=======================================
//=======================================
function renderOrderDetails(element, data) {

    var OrderTotalPrice = 0;
    var rowCounter = 0;
    var $ele = $(element);
    $ele.empty();
    var $container = $('<div class="details">');
    var $row = $('<div class="row">');
    var $noMoreTable = $('<div id="no-more-tables">');
    var $table = $('<table class="col-md-10  table-bordered table-striped table-condensed cf ">');
    var $head = $('<thead class="cf">');
    var $hRow = $('<tr>');
    $hRow.append('<th><b>Item</b></th>');
    $hRow.append('<th><b>Warehouse</b></th>');
    $hRow.append('<td align="right"><b>Boxes</b></td>');
    $hRow.append('<td align="right"><b>Reserve</b></td>');
    $hRow.append('<td align="right"><b>Extra kg</b></td>');
    $hRow.append('<td align="right"><b>Price/kg</b></td>');
    $hRow.append('<td align="right"><b>Ext. price</b></td>');
    $hRow.append('<td><b></b></td>');
    $hRow.append('</tr>');
    $head.append($hRow);
    $head.append('</thead>');
    $table.append($head);
    var $body = $('<tbody>');
    $.each(data,
        function (i, val) {
            OrderTotalPrice += val.Extended_Price;
            rowCounter++;
            var $row = $('<tr>');
            if (rowCounter % 2 > 0) {
                $row = $('<tr style=" background-color: ghostwhite;">');
            }
            $row.append('<td>' + val.Item.Name.trim() + '</td>');
            $row.append('<td>' + val.Warehouse.Name.trim() + '</td>');
            $row.append('<td align="right">' + val.QtyBoxes + '</td>');
            $row.append('<td align="right">' + val.QtyReservBoxes + '</td>');
            $row.append('<td align="right">' + val.QtyKg + '</td>');
            $row.append('<td align="right">' + val.Price + '</td>');
            $row.append('<td align="right">' + val.Extended_Price.toLocaleString() + '</td>');
            $row.append('<td align="center"><a id="EditOrderDetails_' + val.ID + '" href="#"> <span class="glyphicon glyphicon-pencil"></span> </a>  |  <a  id="DeleteOrderDetails_' + val.ID + '" href="#"><span class="glyphicon glyphicon-trash"></span></a></td>');
            $row.append('</tr>');
            $body.append($row);
        }
    )
    var $footerRow = $('<tr>');
   
    $footerRow.append('<td colspan="7" align="right"><b>' + OrderTotalPrice.toLocaleString() + '</b></td>');
    $footerRow.append('<td></td>');
    $body.append($footerRow);
    $body.append('</body>')
    $table.append($body);
    $noMoreTable.append($table);
    $row.append($noMoreTable);
    $container.append($row);
    $ele.append($container);
}
//=======================================
//=======================================
function renderItemGroups(element)
{
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(ItemGroups,
      function (i, val)
      {
       $ele.append($('<option/>').val(val.ID).text(val.Name));
      }
     )
}
//=======================================
//=======================================
function renderItems(element, data) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data,
      function (i, val) {
          $ele.append($('<option/>').val(val.ID).text(val.Name));
      }
   )
}
//=======================================
//=======================================
function renderWarehouses(element, data) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data,
      function (i, val) {
          $ele.append($('<option/>').val(val.ItemWarehouseID).text(val.ItemsonHand));
      }
     )
}
//=======================================
//=======================================
function CheckOrderBeforeSave() {
    //alert($("#HOrderID").val());
    return false;
}
//=======================================
//=======================================
function EnableDisableOrderDetails() {
    if ($("#HOrderID").val() === "")
    {
        $("#OrderDetails").find(":input").prop("disabled", true);
    }
    else
    {
        $("#OrderDetails").find(":input").prop("disabled", false);
    }
    $("#ExtendedPrice").prop('disabled', true);
}
//=======================================
//=======================================
function ValidateOrderDetails() {
   
    var isAllValid = true;
    if ($('#HOrderID').val() !== 'undefined' && $("#HOrderID").val() !== "")
    {
        if ($('#ItemGroup').val() === "0" || $('#ItemGroup').val() === "Select") {
            isAllValid = false;
            $('#ItemGroup').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#ItemGroup').siblings('span.error').css('visibility', 'hidden');
       }
       if ($('#Item').val() === "0" || $('#Item').val() === "Select") {
            isAllValid = false;
            $('#Item').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Item').siblings('span.error').css('visibility', 'hidden');
       }
       if ($('#Warehouse').val() === "0" || $('#Warehouse').val() === "Select") {
           isAllValid = false;
           $('#Warehouse').siblings('span.error').css('visibility', 'visible');
       }
       else {
           $('#Warehouse').siblings('span.error').css('visibility', 'hidden');
       }
        if (!$.isNumeric($('#Boxes').val())) {
           isAllValid = false;
           $('#Boxes').siblings('span.error').css('visibility', 'visible');
       }
       else {
           $('#Boxes').siblings('span.error').css('visibility', 'hidden');
       }

        if ($('#ReserveBoxes').val() !== "" && !$.isNumeric($('#ReserveBoxes').val())) {
           isAllValid = false;
           $('#ReserveBoxes').siblings('span.error').css('visibility', 'visible');
       }
       else {
           $('#ReserveBoxes').siblings('span.error').css('visibility', 'hidden');
       }
       if ($('#ExtraKg').val() !== "" && !$.isNumeric($('#ReserveBoxes').val())) {
           isAllValid = false;
           $('#ExtraKg').siblings('span.error').css('visibility', 'visible');
       }
       else {
           $('#ExtraKg').siblings('span.error').css('visibility', 'hidden');
       }
       if (!$.isNumeric($('#Price').val())) {
           isAllValid = false;
           $('#Price').siblings('span.error').css('visibility', 'visible');
       }
       else {
           $('#Price').siblings('span.error').css('visibility', 'hidden');
       }
       if (isAllValid) {
           CalcExtendexPrice();
       }
   }
}
//=======================================
//=======================================
function saveOrderDetailsRow()
{
    var orderItem = {
        OrderID: $("#HOrderID").val(),
        ItemID: $("#Item").val(),
        WarehouseID: $("#Warehouse").val(),
        QtyBoxes: $('#Boxes').val(),
        QtyReservBoxes: $('#ReserveBoxes').val(),
        QtyKg: $('#ExtraKg').val(),
        Price: parseFloat($("#Price").val()),
        Extended_Price: parseFloat($("#ExtendedPrice").val())
    }

    if (orderItem.OrderID !== "" && orderItem.OrderID !== "0" ){
        $.ajax({
            type: "Post",
            url: "/Orders/SaveOrderDetailsRow",
            data: JSON.stringify(orderItem),
            contentType: 'application/json',
            success: function (data) {
                LoadOrderDetails();
            },
            error: function (error) {
                console.log(error);
            }
        })
    }
}
//=======================================
//=======================================
function genCheckOnHands(id, searchText) {
    var valid = true;
    var list = $("#Warehouse option:selected").text().split('|');
    jQuery.each(list, (index, item) => {
        if (item.indexOf(searchText) > -1) {
            var boxes = $("#" + id).val() ? $("#" + id).val() : 0;
            var onHands = item.replace(searchText, "") ? item.replace(searchText, "") : 0;
            if (parseInt(boxes) > parseInt(onHands)) {
                $("#" + id).siblings('span.error').css('visibility', 'visible');
                $("#" + id).siblings('span.error').append(" |not avaiable");
                valid = false;
            } else {
                $("#" + id).siblings('span.error').css('visibility', 'hidden');
                valid = true;
            }
        }
    });
    return valid;
}
//=======================================
//=======================================
function checkOnHands() {
    var boxesValid = genCheckOnHands('Boxes', 'Box: ');
    var extraValid = genCheckOnHands('ExtraKg', 'Extra: ');
    if (boxesValid === true && extraValid === true) {
        return true;
    }
    else {
        return false;
    }
    return true;
}
//=======================================
//=======================================
function addOrderDetail() {
   
    $.ajax({
        type: 'POST',
        url: '@Url.Content("Orders/GetOrderDetails")',
        data: { 'Id': $("#HOrderID").val() },
        success: function (data) {
            $('#divid').innerHTML = data;
        }
    });
    
}
//=======================================
//=======================================
function DeleteOrderRow() {
    if (confirm("Do you realy want to delete this?")) {
        $("#DeleteOrderRow").closest('tr').remove();
    }
}
//=======================================
//=======================================
function CalcExtendexPrice() {
    var boxes = 0;
    var validOnHands = checkOnHands();
    if (validOnHands) {
       
        if ($('#Boxes').val() !== "" && $.isNumeric($('#Boxes').val()))
            boxes = parseFloat($('#Boxes').val());
        if ($('#ReserveBoxes').val() !== "" && $.isNumeric($('#ReserveBoxes').val()))
            boxes += parseFloat($('#ReserveBoxes').val());
        var boxWeight = $('#Item').text().split(' ')[1].replace('kg', '');
        var extraKg = 0;
        if ($('#ExtraKg').val() !== "" && $.isNumeric($('#ExtraKg').val()))
            extraKg = parseFloat($('#ExtraKg').val());
        if (boxes > 0 && boxWeight > 0)
            $('#ExtendedPrice').attr('value', ((boxes * parseFloat(boxWeight)) + extraKg) * parseFloat($('#Price').val()));
        
        if ($('#ExtendedPrice').val() !== "" && $.isNumeric($('#ExtendedPrice').val())) {
            saveOrderDetailsRow(); 
        }
    }
}
//=======================================
//=======================================
$(document).ready(
    EnableDisableOrderDetails(),
    LoadOrderDetails(),
    LoadItemGroups($('#ItemGroup'))
   
    
   
    
 );