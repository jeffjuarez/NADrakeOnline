

$(function () {

    
  

    var currentDate = new Date();
 
    var currentDateFrom = new Date(); // today!
   

    $("#RecDateFrom").datepicker({dateFormat: 'dd/mm/yy',
        onSelect: function (dateText) {
            var selectedDate = new Date(dateText);
    
        }
    });

    $("#holiday_date").datepicker({
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText) {
            var selectedDate = new Date(dateText);

        }
    });



      
    $("#RecDateFrom").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#RecDateTo").datepicker({ dateFormat: 'dd/mm/yy' });

   

});
