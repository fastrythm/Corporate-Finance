/*
 * Interação - Metro Button 1.1.0 - jQuery Plugin
 * http://www.interacaosistemas.com.br
 * Copyright (c) 2012 Roger Medeiros
 * Microsoft Reciprocal License (Ms-RL)
 * Revision: $Id: jquery.metro-btn.min.js 2 2012-06-27
 */
(function ($) {

	$.fn.AddBtnMy = function(id, col , theme,  imagem, texto, link ) {
		var el = $(this);
        if (col != '') {
            col_grid = col;
        }
        else {
            col_grid = 3;
        }
        var html_code = "<div class='col-sm-"+col_grid+" col-xs-12 da_nopadding'>\r\n";
         html_code += "<a href='" + link + "'><div";

        if (id != '') {
            html_code += " id='" + id + "'";
        }

        if (link != '') {
            html_code += " ";
        }
            html_code += " class='col-sm-12 col-xs-12 metro dab_" + theme + "'>\r\n";
        html_code += "\t<div class='imgdouble'><i class='fa fa-"+imagem+" '></i> </div>\r\n";
        html_code += "\t<div class='metro-destaque-rodape dat_"+ theme + "'><span>" + texto + "</span></div>\r\n";
        html_code += "</a></div>\r\n";

        el.append(html_code);
	};

})(jQuery);