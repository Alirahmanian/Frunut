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
                url: '/OrderAjax/GetItemGroups',
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
          url: "/OrderAjax/GetItemsByGroupID",
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
        url: "/OrderAjax/GetItemWarehousesByItemID",
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

    if ($("#HOrderID").val() !== "")
    {
        $.ajax({
            type: "GET",
            url: "/OrderAjax/GetOrderDetailsForAjax",
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
   
    $ele.append('<div class=" col-md-10  ">');
    $ele.append('<div class="row">');
    $ele.append('<div class="col-md-1"><b>Item</b></div>');
    $ele.append('<div class="col-md-1"><b>Warehouse</b></div>');
    $ele.append('<div class="col-md-1" align="right"><b>Boxes</b></div>');
    $ele.append('<div class="col-md-1" align="right"><b>Reserve</b></div>');
    $ele.append('<div class="col-md-1" align="right"><b>Extra kg</b></div>');
    $ele.append('<div class="col-md-1" align="right"><b>Price/kg</b></div>');
    $ele.append('<div class="col-md-1" align="right"><b>Ext. price</b></div>');
    $ele.append('<div class="col-md-1"><b></b></div>');
    $ele.append('</div>');
    
    $.each(data,
        function (i, val) {
            OrderTotalPrice += val.Extended_Price;
            rowCounter++;
            if (rowCounter % 2 > 0) {
                $ele.append('<div class="row" style=" background-color: ghostwhite;">');
            } else {
                $ele.append('<div class="row">');
            }
            $ele.append('<div class="col-md-1">' + val.Item.Name.trim() + '</div>');
            $ele.append('<div class="col-md-1">' + val.Warehouse.Name.trim() + '</div>');
            $ele.append('<div class="col-md-1" align="right">' + val.QtyBoxes + '</div>');
            $ele.append('<div class="col-md-1" align="right">' + val.QtyReservBoxes + '</div>');
            $ele.append('<div class="col-md-1" align="right">' + val.QtyKg + '</div>');
            $ele.append('<div class="col-md-1" align="right">' + val.Price + '</div>');
            $ele.append('<div class="col-md-1" align="right">' + val.Extended_Price.toLocaleString() + '</div>');
            $ele.append('<div class="col-md-1"><a  id="EditOrderDetails_"' + val.Id + ' href=""> <span class="glyphicon glyphicon-pencil"></span> </a>  |  <a  id="DeleteOrderDetails_"' + val.Id + ' href=""><span class="glyphicon glyphicon-trash"></span></a></div>');
            $ele.append('</div>');
        }
    )
    $ele.append(' <div class="col-md-7 oddRowbgColor " align="right"><b>' + OrderTotalPrice.toLocaleString() + '</b></div>');
    $ele.append('<div class="col-md-5"></div>'); 

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
function ClearOrderDetailsForm()
{
    $("#ItemGroup").val($("#ItemGroup option:first").val());
    $('#ItemGroup').siblings('span.error').css('visibility', 'hidden');
    //$('#Item').val('Select');
    $("#Item").val($("#Item option:first").val());
    $('#Item').siblings('span.error').css('visibility', 'hidden');
    $("#Warehouse").val($("#Warehouse option:first").val());
    $('#Warehouse').siblings('span.error').css('visibility', 'hidden');
    $('#Boxes').val('');
    $('#Boxes').siblings('span.error').css('visibility', 'hidden');
    $('#ReserveBoxes').val('');
    $('#ReserveBoxes').siblings('span.error').css('visibility', 'hidden');
    $('#ExtraKg').val('');
    $('#ExtraKg').siblings('span.error').css('visibility', 'hidden');
    $('#Price').val('');
    $('#Price').siblings('span.error').css('visibility', 'hidden');
    $("#ExtendedPrice").val('');
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
            url: "/OrderAjax/SaveOrderDetailsRow",
            data: JSON.stringify(orderItem),
            contentType: 'application/json',
            success: function (data) {
                LoadOrderDetails();
                ClearOrderDetailsForm();
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
    //return true;
}
//=======================================
//=======================================
function addOrderDetail() {
   
    $.ajax({
        type: 'POST',
        url: '@Url.Content("OrderAjax/GetOrderDetails")',
        data: { 'Id': $("#HOrderID").val() },
        success: function (data) {
            $('#divid').innerHTML = data;
        }
    });
    //var rowCount = $('#orderDetailsTable tr').length;
    //var row = '';
    //if ((rowCount + 1) % 2 === 0)
    //    row = '<tr class=" alter-info">';
    //else
    //    row = '<tr>';
    //row += '<td>' + $("#Item option:selected").text() + '</td>';
    //row +='<td>' + $("#Warehouse option:selected").text().split('|')[0] + '</td>';
    //row += '<td>' + $("#Boxes").val() + '</td>';
    //row +='<td>' + $("#ReserveBoxes").val() + '</td>';
    //row +='<td>' + $("#ExtraKg").val() + '</td>';
    //row +='<td>' + $("#Price").val() + '</td>';
    //row +='<td>' + $("#ExtendedPrice").val() + $("#ExtendedPrice").text() + '</td>';
    //row += '<td><button id="EditOrderRow" style="background: green;" type="button" class="editRow btn btn-warning" onclick=" DeleteOrderRow()">Edit</ button></td>';
    //row += '<td><button id="DeleteOrderRow" style="background: red;" type="button" class="removeRow btn btn-danger">Delete</ button></td>';
    //row += '</tr>';
    //$('#orderDetailsTable tbody').last().append(row);
    //$(".removeRow").on('click',function(){
    //    $(this).parent().parent().remove();
    //})
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
     
  
   // $('#OrderDetailsForPartial').load("~/Sale/Orders/GetOrderDetails/", { Id: $("#HOrderID").val(), viewName: "_OrderDetails"}),
    //$.get('@Url.Action("GetOrderDetails","Orders", new { id = ' + $("#HOrderID").val() + ' } )', function (data) {
    //    $('#OrderDetailsForPartial').html(data);
    //}),
    
    //$('#Add').on('click', ValidateOrderDetails())
    LoadItemGroups($('#ItemGroup')),
    $("a[id^='EditOrderDetails_']").on('click', function () {
        alert(this.id);
    })
 );