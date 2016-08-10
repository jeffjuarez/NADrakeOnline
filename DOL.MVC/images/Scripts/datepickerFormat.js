

$(function () {

    
  

    var currentDate = new Date();
 
    var currentDateFrom = new Date(); // today!
    var x = 7; // go back 7 days!
    currentDateFrom.setDate(currentDateFrom.getDate() - x)



    $("#RecDateFrom").datepicker({
        dateFormat: 'dd/mm/yy'
    }).datepicker('setDate', currentDateFrom)

  
    $("#RecDateTo").datepicker({
        dateFormat: 'dd/mm/yy'
    }).datepicker('setDate', currentDate)


 

    $("#RecDateTo").datepicker({ dateFormat: 'dd/mm/yy' });

        $("#RecDateFrom").datepicker({ dateFormat: 'dd/mm/yy' }).bind("change", function () {
            var minValue = $(this).val();
            minValue = $.datepicker.parseDate("dd/mm/yy", minValue);
            minValue.setDate(minValue.getDate() + 1);
            $("#RecDateTo").datepicker("option", "minDate", minValue);
        })
});
