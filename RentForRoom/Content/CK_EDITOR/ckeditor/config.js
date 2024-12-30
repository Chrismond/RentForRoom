
CKEDITOR.editorConfig = function( config )
{
            config.language = 'en';
            config.contentsCss = '/CKEditor/CustomFonts/fonts.css';

            config.font_names = 'SFUFuturaRegular,Open Sans,Arial' + config.font_names;
        
        config.toolbar_Full = [
            ['Source','-','Save','NewPage','Preview','-','Templates'],
            ['Cut','Copy','Paste','PasteText','PasteFromWord','-','Print', 'SpellChecker', 'Scayt'],
            ['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat'],
            '/',
            ['Bold','Italic','Underline','Strike','-','Subscript','Superscript'],
            ['NumberedList','BulletedList','-','Outdent','Indent','Blockquote','CreateDiv'],
            ['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
            ['BidiLtr', 'BidiRtl' ],
            ['Link','Unlink','Anchor'],
            ['Image','Flash','Table','HorizontalRule','Smiley','SpecialChar','PageBreak','Iframe'],
            '/',
            ['Styles','Format','Font','FontSize'],
            ['TextColor','BGColor'],
            ['Maximize', 'ShowBlocks','-','About']
            ];
        config.entities = false;

    // Remove some buttons provided by the standard plugins, which are
    // not needed in the Standard(s) toolbar.
        config.removeButtons = 'Underline,Subscript,Superscript';

    // Set the most common block elements.
        config.format_tags = 'p;h1;h2;h3;pre';

    // Simplify the dialog windows.
        config.removeDialogTabs = 'image:advanced;link:advanced';
        config.filebrowserBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html';
        config.filebrowserImageBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html?type=Images';
        config.filebrowserFlashBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html?type=Flash';
        config.filebrowserUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
        config.filebrowserImageUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
        config.filebrowserFlashUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

};  