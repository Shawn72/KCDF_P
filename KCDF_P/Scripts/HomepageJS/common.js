function valueOutput(e){var s=e.value,r=jQuery(".range-value");0==s?r.css("visibility","hidden"):1==s?(r.css("visibility","visible"),r.html(s+" Year")):(r.css("visibility","visible"),r.html(s+" Years"))}jQuery(document).ready(function(){function e(){jQuery("body").hasClass("search-open")&&jQuery("body").removeClass("search-open"),jQuery(".mobile-country-selector").hasClass("open")&&jQuery(".mobile-country-selector").removeClass("open"),jQuery("body").toggleClass("mobile-menu-open")}function s(){a.removeClass("open")}function r(){var e=jQuery(".has-floating-labels .textbox, .has-floating-labels select");e.each(function(){var e=jQuery(this);o(e),e.on("change keyup",function(){o(e)})})}function o(e){""==e.val()?e.closest(".field").find("label").removeClass("float"):e.closest(".field").find("label").addClass("float")}jQuery("a[href*=member]:not([href=#])").click(function(){if(location.pathname.replace(/^\//,"")==this.pathname.replace(/^\//,"")&&location.hostname==this.hostname){var e=jQuery(this.hash);if(e=e.length?e:jQuery("[name="+this.hash.slice(1)+"]"),e.length)return jQuery("html,body").animate({scrollTop:e.offset().top-120},1e3),!1}}),jQuery("#board_members .pic-wrapper").click(function(){var e=jQuery(this).closest("li"),s=jQuery(this).height(),r=e.find(".details").innerHeight();return jQuery("#board_members li").height(s),jQuery(e).hasClass("active")?(jQuery("#board_members .details").hide(),jQuery("#board_members li").removeClass("active"),e.removeClass("active")):(e.height(s+r),jQuery("#board_members .details").hide(),e.find(".details").slideToggle("slow"),jQuery("#board_members li").removeClass("active"),e.addClass("active")),!1}),jQuery(".countries-selector .active").click(function(){return jQuery(this).hasClass("open")?(jQuery(".countries-selector .inactive").addClass("slideLeft"),jQuery(".countries-selector .inactive").removeClass("slideRight"),jQuery(this).removeClass("open")):(jQuery(this).addClass("open"),jQuery(".countries-selector .inactive").addClass("slideRight"),jQuery(".countries-selector .inactive").removeClass("slideLeft")),!1}),jQuery("#sub_nav .active").click(function(){jQuery(this).addClass("current-page")}),jQuery("#sub_nav .has-children").click(function(){jQuery("body").removeClass("search-open"),jQuery("#sub_nav .has-children").not(this).removeClass("active"),jQuery(this).toggleClass("active");var e=jQuery(this).find("a").attr("href");return jQuery(".mega-menu").slideUp("fast"),jQuery(this).hasClass("active")?jQuery(e).slideDown("fast"):jQuery(e).slideUp("fast"),!1});var i=1;jQuery(window).scroll(function(){jQuery("#sub_nav > .active").size()>0&&jQuery(".mega-menu").slideUp("fast"),jQuery(this).scrollTop()>i?jQuery("body").addClass("menu-fixed"):jQuery("body").removeClass("menu-fixed")}),jQuery("#mobile-menu-button, .mobile-menu-mask").click(function(s){e(),s.preventDefault()}),jQuery("body").on("click",".mobile-menu-open #content_mask, .menu-close",function(s){e(),s.preventDefault()}),jQuery(".accordion, .mobile-main-menu").accordion({heightStyle:"content",collapsible:!0,active:!1}),jQuery(".mobile-main-menu").accordion("option"),jQuery("#mobile-country-toggle").click(function(){jQuery("body").hasClass("mobile-menu-open")&&jQuery("body").removeClass("mobile-menu-open"),jQuery("body").hasClass("search-open")&&jQuery("body").removeClass("search-open"),jQuery(".mobile-country-selector").toggleClass("open")}),jQuery(".btn-search").click(function(){return jQuery(".mega-menu").slideUp("fast"),jQuery("#sub_nav .has-children").removeClass("active"),jQuery("body").toggleClass("search-open"),jQuery("body").hasClass("search-open")&&jQuery(".search-box").focus(),!1}),jQuery(".mobile-btn-search").click(function(){return jQuery("body").hasClass("mobile-menu-open")&&jQuery("body").removeClass("mobile-menu-open"),jQuery(".mobile-country-selector").hasClass("open")&&jQuery(".mobile-country-selector").removeClass("open"),jQuery("body").toggleClass("search-open"),jQuery("body").hasClass("search-open")&&jQuery(".search-box").focus(),!1}),jQuery(".quick-btn-search").click(function(){return jQuery("body").toggleClass("search-open"),jQuery("body").hasClass("search-open")&&jQuery(".search-box").focus(),!1}),jQuery(".search-box").keyup(function(){jQuery(this).val()?(jQuery(".search-cancel").show(),jQuery(".suggestions-list").slideDown(150)):(jQuery(".search-cancel").hide(),jQuery(".suggestions-list").slideUp(150))}),jQuery(".search-cancel").click(function(){jQuery(".search-box").val("").focus(),jQuery(".suggestions-list").slideUp(150),jQuery(this).hide()}),jQuery("#content_mask").bind("touchstart mousedown",function(){return jQuery(window).scrollTop(0),jQuery("body").removeClass("search-open"),jQuery(".suggestions-list").hide(),!1}),jQuery("#stocks-ticker").webTicker(),jQuery("#mobile-form-toggle").click(function(e){e.preventDefault(),jQuery(".mobile-form-toggle").toggleClass("toggle-open")});var a=jQuery("#quick_links"),n=jQuery("#mobile_quick_links"),t=jQuery("#mobile_quick_links .header");jQuery(window).load(function(){a.addClass("open")}),setTimeout(s,2e3),a.mouseenter(function(){jQuery(this).addClass("open")}).mouseleave(function(){jQuery(this).removeClass("open")}),t.click(function(){n.toggleClass("open"),jQuery("#content_mask").toggleClass("mobile_quick_links_open")}),jQuery(document).on("click","#content_mask",function(){jQuery("#mobile_quick_links").toggleClass("open"),jQuery("#content_mask").toggleClass("mobile_quick_links_open")}),jQuery(".has-floating-labels").length>0&&r(),jQuery(".dd").selectric();var l;l=jQuery(".back-to-top");var u=200,c=700;jQuery(window).scroll(function(){jQuery(this).scrollTop()>u?l.addClass("is-visible"):l.removeClass("is-visible")}),l.on("click",function(e){e.preventDefault(),jQuery("body,html").animate({scrollTop:0},c)})});var selector="[data-rangeslider]",element=jQuery(selector);jQuery(document).on("input",'input[type="range"], '+selector,function(e){valueOutput(e.target)}),jQuery(document).ready(function(){element.rangeslider({polyfill:!1,onInit:function(){valueOutput(this.element[0])}})}),jQuery(window).load(function(){jQuery("#preloader_wrapper .ball-grid-pulse").fadeOut(),jQuery("#preloader_wrapper").fadeOut(1e3)}),jQuery(".responsive-tab").responsiveTabs({startCollapsed:"accordion"}),jQuery("#approval-continue").click(function(e){e.preventDefault(),jQuery("#approval-tabs").responsiveTabs("activate",1)}),jQuery(".news-grid-container").mixItUp(),jQuery("#sidebar-accordion").accordion({header:".sidebar-accordion-title",heightStyle:"auto",active:0}),jQuery("#sidebar-tabs").tabs({heightStyle:"content",active:0}),jQuery(document).ready(function(){jQuery(".fancybox").fancybox()});