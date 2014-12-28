$(document).ready(function(){
	$( "#enter" ).button();
	$( "#send" ).button();
	$( "#photo" ).button();
	$( "#theme" ).selectmenu({
                menuWidth: 220,
				width: 200,
                positionOptions: {
                    collision: 'none'
                }
            });
	$("#enter").change(function(){
		$("#send").fadeToggle("fast");
		if($("#enter").prop("checked") == true){
			$( "#enter" ).button( "option", "icons", {} );
		}
		else{
			$( "#enter" ).button( "option", "icons", {primary: "ui-icon-check"} );
		}
	});
	//I finally made it work, so please:
	//Don't f*ck with this
	$("#wrapper").height($(window).height()*0.96 - $("#top").height() - $("#new").height() - $("footer").height());
	$(window).resize(function(){
		$("#wrapper").height($(window).height()*0.96 - $("#top").height() - $("#new").height() - $("footer").height());
	});
}); 
