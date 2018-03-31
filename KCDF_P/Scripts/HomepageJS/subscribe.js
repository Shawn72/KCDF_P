jQuery(document).ready(function() {
		    jQuery('#subscribe').submit(function() {
		        if (!valid_email_address(jQuery("#txtemail").val()))
		        {
		            jQuery(".message2").html("<span>Please make sure you enter a valid email address.</span>");
					jQuery(".message2").show();
					
		        }else{
		            
		            jQuery(".message").html("<span style='color:green;'>Almost done.</span>");
					jQuery("#sub").val('Submitting...');
		            jQuery.ajax({
		                //url: 'https://squad.wpp-scangroup.com/kcb/site/templates/corporate/app/subscribe.php', 
						url: 'https://ke.kcbbankgroup.com/templates/corporate/app/subscribe.php', 
		                data: jQuery('#subscribe').serialize(),
		                type: 'POST',
		                success: function(msg) {
		                    if(msg=="success")
		                    {
		                        jQuery("#txtemail").val("");
								jQuery("#sub").val('Submit');
		                        jQuery(".message").html('<span style="color:#090">You have successfully subscribed to our mailing list.</span>');
		                        
		                    }
		                    else
		                    {
		                       jQuery(".message").html('<span style="">Seems you have already subscribed.</span>');  
							   jQuery("#sub").val('Submit');
		                    }
		                }
		            });
		        }
		 
		        return false;
		    });
		});

		function valid_email_address(email)
		{
		    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
		    return pattern.test(email);
			jQuery("#sub").val('Submit');
		}// JavaScript Document