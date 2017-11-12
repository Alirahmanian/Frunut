var ItemGroups = []
//fetch categories from database
//=======================================
//=======================================
function LoadItemGroups(element)
{
    
    if (ItemGroups.length === 0)
    {
        //ajax function for fetch data
        $.ajax
            ({
                type: "GET",
                url: '/Orders/GetItemGroups',
                success: function (data)
                {
                    ItemGroups = data;
                  //render catagory
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
      //render itemgroups to the element
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
          //render items to appropriate dropdown
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
            //render warehouse to appropriate dropdown
            renderWarehouses($('#Warehouse'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
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
    alert($("#HOrderID").val());
    return false;
}
//=======================================
//=======================================
function EnableDisableOrderDetails() {
    if ($("#HOrderID").val() === "")
    {
        $("#OrderDetails").find(":input").prop("disabled", true);
        //document.getElementById("OrderDetails").disabled = true;
    }
    else
    {
        $("#OrderDetails").find(":input").prop("disabled", false);
       // document.getElementById("OrderDetails").disabled = false;
    }
    $("#ExtendedPrice").prop('disabled', true);
}
//=======================================
//=======================================
function ValidateOrderDetails() {
   
    var isAllValid = true;
   
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
    /*
   if ($('#ExtendedPrice').val() === "0") {
       isAllValid = false;
       $('#ExtendedPrice').siblings('span.error').css('visibility', 'visible');
   }
   else {
       $('#ExtendedPrice').siblings('span.error').css('visibility', 'hidden');
   }
   */
   if (isAllValid) {
       CalcExtendexPrice();
       saveOrderDetailsRow();
   }


}
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
                //render items to appropriate dropdown
                debugger;
                alert(data);
                addOrderDetail();
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
    } else {
        return false;
    }
    return true;
}
//=======================================
//=======================================
function addOrderDetail() {
    var rowCount = $('#orderDetailsTable tr').length;
    var row = '';
    if ((rowCount + 1) % 2 === 0)
        row = '<tr class=" alter-info">';
    else
        row = '<tr>';
    row += '<td>' + $("#Item option:selected").text() + '</td>';
    row +='<td>' + $("#Warehouse option:selected").text().split('|')[0] + '</td>';
    row += '<td>' + $("#Boxes").val() + '</td>';
    row +='<td>' + $("#ReserveBoxes").val() + '</td>';
    row +='<td>' + $("#ExtraKg").val() + '</td>';
    row +='<td>' + $("#Price").val() + '</td>';
    row +='<td>' + $("#ExtendedPrice").val() + $("#ExtendedPrice").text() + '</td>';
    row += '<td><button id="EditOrderRow" style="background: green;" type="button" class="editRow btn btn-warning" onclick=" DeleteOrderRow()">Edit</ button></td>';
    row += '<td><button id="DeleteOrderRow" style="background: red;" type="button" class="removeRow btn btn-danger">Delete</ button></td>';
    row += '</tr>';
    $('#orderDetailsTable tbody').last().append(row);
    $(".removeRow").on('click',function(){
        $(this).parent().parent().remove();
    })
}
//=======================================
//=======================================
function DeleteOrderRow() {
    alert("shaba");
     $("#DeleteOrderRow").closest( 'tr').remove();
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

       // addOrderDetail();
        /*if ($('#ExtendedPrice').val() !== "" && !$.isNumeric($('#ExtendedPrice').val())) {
            // try save
        }*/
    }
}
//=======================================
//=======================================
$(document).ready(
    EnableDisableOrderDetails(),
    LoadItemGroups($('#ItemGroup')),
    

    //, $('#Add').on('click', ValidateOrderDetails())
   
    
    );