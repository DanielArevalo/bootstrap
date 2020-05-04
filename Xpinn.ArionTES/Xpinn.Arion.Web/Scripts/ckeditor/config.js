/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    config.language = "es";
    config.uiColor = "#1F618D";
    config.height = 300;
    config.toolbarCanCollapse = true;
    config.skin = "office2013";
    config.removePlugins = "image";
    config.allowedContent = true;
    config.fullPage = true;
    config.extraPlugins = ["base64image", "docprops"];
};
