﻿<xsl:stylesheet version="1.0" xmlns:user="urn:my-scripts" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2011-09-16T21:40:36" xmlns:xd="http://schemas.microsoft.com/office/infopath/2003" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:xdExtension="http://schemas.microsoft.com/office/infopath/2003/xslt/extension" xmlns:xdXDocument="http://schemas.microsoft.com/office/infopath/2003/xslt/xDocument" xmlns:xdSolution="http://schemas.microsoft.com/office/infopath/2003/xslt/solution" xmlns:xdFormatting="http://schemas.microsoft.com/office/infopath/2003/xslt/formatting" xmlns:xdImage="http://schemas.microsoft.com/office/infopath/2003/xslt/xImage" xmlns:xdUtil="http://schemas.microsoft.com/office/infopath/2003/xslt/Util" xmlns:xdMath="http://schemas.microsoft.com/office/infopath/2003/xslt/Math" xmlns:xdDate="http://schemas.microsoft.com/office/infopath/2003/xslt/Date" xmlns:sig="http://www.w3.org/2000/09/xmldsig#" xmlns:xdSignatureProperties="http://schemas.microsoft.com/office/infopath/2003/SignatureProperties" xmlns:ipApp="http://schemas.microsoft.com/office/infopath/2006/XPathExtension/ipApp" xmlns:xdEnvironment="http://schemas.microsoft.com/office/infopath/2006/xslt/environment" xmlns:xdUser="http://schemas.microsoft.com/office/infopath/2006/xslt/User" xmlns:xdServerInfo="http://schemas.microsoft.com/office/infopath/2009/xslt/ServerInfo">
<!--  <msxsl:script language="C#" implements-prefix="user">
<![CDATA[
      
	
		public string ConvertXmlDateTimeToNormalized(string data)
        {
			if (string.IsNullOrEmpty(data)) return "";
           	return System.Convert.ToDateTime(data).ToString("MM/dd/yyyy HH:mm:ss");
        }
  ]]>
	</msxsl:script>  -->
	<xsl:output method="html" indent="no" />
	<xsl:template match="FAILURE">
		<html>
			<head>
				<style tableEditor="TableStyleRulesID">TABLE.xdLayout TD {
	BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; BORDER-TOP: medium none; BORDER-RIGHT: medium none
}
TABLE {
	BEHAVIOR: url (#default#urn::tables/NDTable)
}
TABLE.msoUcTable TD {
	BORDER-BOTTOM: 1pt solid; BORDER-LEFT: 1pt solid; BORDER-TOP: 1pt solid; BORDER-RIGHT: 1pt solid
}
</style>
				<meta content="text/html" http-equiv="Content-Type"></meta>
				<style controlStyle="controlStyle">@media screen 			{ 			BODY{margin-left:21px;background-position:21px 0px;} 			} 		BODY{color:windowtext;background-color:window;layout-grid:none;} 		.xdListItem {display:inline-block;width:100%;vertical-align:text-top;} 		.xdListBox,.xdComboBox{margin:1px;} 		.xdInlinePicture{margin:1px; BEHAVIOR: url(#default#urn::xdPicture) } 		.xdLinkedPicture{margin:1px; BEHAVIOR: url(#default#urn::xdPicture) url(#default#urn::controls/Binder) } 		.xdHyperlinkBox{word-wrap:break-word; text-overflow:ellipsis;overflow-x:hidden; OVERFLOW-Y: hidden; WHITE-SPACE:nowrap; display:inline-block;margin:1px;padding:5px;border: 1pt solid #dcdcdc;color:windowtext;BEHAVIOR: url(#default#urn::controls/Binder) url(#default#DataBindingUI)} 		.xdSection{border:1pt solid transparent ;margin:0px 0px 0px 0px;padding:0px 0px 0px 0px;} 		.xdRepeatingSection{border:1pt solid transparent;margin:0px 0px 0px 0px;padding:0px 0px 0px 0px;} 		.xdMultiSelectList{margin:1px;display:inline-block; border:1pt solid #dcdcdc; padding:1px 1px 1px 5px; text-indent:0; color:windowtext; background-color:window; overflow:auto; behavior: url(#default#DataBindingUI) url(#default#urn::controls/Binder) url(#default#MultiSelectHelper) url(#default#ScrollableRegion);} 		.xdMultiSelectListItem{display:block;white-space:nowrap}		.xdMultiSelectFillIn{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;overflow:hidden;text-align:left;}		.xdBehavior_Formatting {BEHAVIOR: url(#default#urn::controls/Binder) url(#default#Formatting);} 	 .xdBehavior_FormattingNoBUI{BEHAVIOR: url(#default#CalPopup) url(#default#urn::controls/Binder) url(#default#Formatting);} 	.xdExpressionBox{margin: 1px;padding:1px;word-wrap: break-word;text-overflow: ellipsis;overflow-x:hidden;}.xdBehavior_GhostedText,.xdBehavior_GhostedTextNoBUI{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#TextField) url(#default#GhostedText);}	.xdBehavior_GTFormatting{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#Formatting) url(#default#GhostedText);}	.xdBehavior_GTFormattingNoBUI{BEHAVIOR: url(#default#CalPopup) url(#default#urn::controls/Binder) url(#default#Formatting) url(#default#GhostedText);}	.xdBehavior_Boolean{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#BooleanHelper);}	.xdBehavior_Select{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#SelectHelper);}	.xdBehavior_ComboBox{BEHAVIOR: url(#default#ComboBox)} 	.xdBehavior_ComboBoxTextField{BEHAVIOR: url(#default#ComboBoxTextField);} 	.xdRepeatingTable{BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; BORDER-COLLAPSE: collapse; WORD-WRAP: break-word;}.xdScrollableRegion{BEHAVIOR: url(#default#ScrollableRegion);} 		.xdLayoutRegion{display:inline-block;} 		.xdMaster{BEHAVIOR: url(#default#MasterHelper);} 		.xdActiveX{margin:1px; BEHAVIOR: url(#default#ActiveX);} 		.xdFileAttachment{display:inline-block;margin:1px;BEHAVIOR:url(#default#urn::xdFileAttachment);} 		.xdSharePointFileAttachment{display:inline-block;margin:2px;BEHAVIOR:url(#default#xdSharePointFileAttachment);} 		.xdAttachItem{display:inline-block;width:100%%;height:25px;margin:1px;BEHAVIOR:url(#default#xdSharePointFileAttachItem);} 		.xdSignatureLine{display:inline-block;margin:1px;background-color:transparent;border:1pt solid transparent;BEHAVIOR:url(#default#SignatureLine);} 		.xdHyperlinkBoxClickable{behavior: url(#default#HyperlinkBox)} 		.xdHyperlinkBoxButtonClickable{border-width:1px;border-style:outset;behavior: url(#default#HyperlinkBoxButton)} 		.xdPictureButton{background-color: transparent; padding: 0px; behavior: url(#default#PictureButton);} 		.xdPageBreak{display: none;}BODY{margin-right:21px;} 		.xdTextBoxRTL{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-align:right;word-wrap:normal;} 		.xdRichTextBoxRTL{display:inline-block;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow-x:hidden;word-wrap:break-word;text-overflow:ellipsis;text-align:right;font-weight:normal;font-style:normal;text-decoration:none;vertical-align:baseline;} 		.xdDTTextRTL{height:100%;width:100%;margin-left:22px;overflow:hidden;padding:0px;white-space:nowrap;} 		.xdDTButtonRTL{margin-right:-21px;height:17px;width:20px;behavior: url(#default#DTPicker);} 		.xdMultiSelectFillinRTL{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;overflow:hidden;text-align:right;}.xdTextBox{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-align:left;word-wrap:normal;} 		.xdRichTextBox{display:inline-block;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow-x:hidden;word-wrap:break-word;text-overflow:ellipsis;text-align:left;font-weight:normal;font-style:normal;text-decoration:none;vertical-align:baseline;} 		.xdDTPicker{;display:inline;margin:1px;margin-bottom: 2px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-indent:0; layout-grid: none} 		.xdDTText{height:100%;width:100%;margin-right:22px;overflow:hidden;padding:0px;white-space:nowrap;} 		.xdDTButton{margin-left:-21px;height:17px;width:20px;behavior: url(#default#DTPicker);} 		.xdRepeatingTable TD {VERTICAL-ALIGN: top;}</style>
				<style languageStyle="languageStyle">BODY {
	FONT-FAMILY: Calibri; FONT-SIZE: 10pt
}
SELECT {
	FONT-FAMILY: Calibri; FONT-SIZE: 10pt
}
TABLE {
	TEXT-TRANSFORM: none; FONT-STYLE: normal; FONT-FAMILY: Calibri; COLOR: black; FONT-SIZE: 10pt; FONT-WEIGHT: normal
}
.optionalPlaceholder {
	FONT-STYLE: normal; PADDING-LEFT: 20px; FONT-FAMILY: Calibri; COLOR: #333333; FONT-SIZE: 9pt; FONT-WEIGHT: normal; TEXT-DECORATION: none; BEHAVIOR: url(#default#xOptional)
}
.langFont {
	WIDTH: 150px; FONT-FAMILY: Calibri; FONT-SIZE: 10pt
}
.defaultInDocUI {
	FONT-FAMILY: Calibri; FONT-SIZE: 9pt
}
.optionalPlaceholder {
	PADDING-RIGHT: 20px
}
</style>
				<style themeStyle="urn:office.microsoft.com:themeSummer">TABLE {
	BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; BORDER-COLLAPSE: collapse; BORDER-TOP: medium none; BORDER-RIGHT: medium none
}
TD {
	BORDER-BOTTOM-COLOR: #d8d8d8; BORDER-TOP-COLOR: #d8d8d8; BORDER-RIGHT-COLOR: #d8d8d8; BORDER-LEFT-COLOR: #d8d8d8
}
TH {
	BORDER-BOTTOM-COLOR: #000000; BACKGROUND-COLOR: #f2f2f2; BORDER-TOP-COLOR: #000000; COLOR: black; BORDER-RIGHT-COLOR: #000000; BORDER-LEFT-COLOR: #000000
}
.xdTableHeader {
	BACKGROUND-COLOR: #f2f2f2; COLOR: black
}
.light1 {
	BACKGROUND-COLOR: #ffffff
}
.dark1 {
	BACKGROUND-COLOR: #000000
}
.light2 {
	BACKGROUND-COLOR: #f7f8f4
}
.dark2 {
	BACKGROUND-COLOR: #2b4b4d
}
.accent1 {
	BACKGROUND-COLOR: #6c9a7f
}
.accent2 {
	BACKGROUND-COLOR: #bb523d
}
.accent3 {
	BACKGROUND-COLOR: #c89d11
}
.accent4 {
	BACKGROUND-COLOR: #fccf10
}
.accent5 {
	BACKGROUND-COLOR: #568ea1
}
.accent6 {
	BACKGROUND-COLOR: #decf28
}
</style>
				<style tableStyle="Professional">TR.xdTitleRow {
	MIN-HEIGHT: 83px
}
TD.xdTitleCell {
	TEXT-ALIGN: center; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 14px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 32px; valign: bottom
}
TR.xdTitleRowWithHeading {
	MIN-HEIGHT: 69px
}
TD.xdTitleCellWithHeading {
	TEXT-ALIGN: center; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 0px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 32px; valign: bottom
}
TR.xdTitleRowWithSubHeading {
	MIN-HEIGHT: 75px
}
TD.xdTitleCellWithSubHeading {
	TEXT-ALIGN: center; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 6px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 32px; valign: bottom
}
TR.xdTitleRowWithOffsetBody {
	MIN-HEIGHT: 72px
}
TD.xdTitleCellWithOffsetBody {
	TEXT-ALIGN: left; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 2px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 32px; valign: bottom
}
TR.xdTitleHeadingRow {
	MIN-HEIGHT: 37px
}
TD.xdTitleHeadingCell {
	TEXT-ALIGN: center; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 14px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 0px; valign: top
}
TR.xdTitleSubheadingRow {
	MIN-HEIGHT: 70px
}
TD.xdTitleSubheadingCell {
	BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 16px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 8px; valign: top
}
TD.xdVerticalFill {
	BORDER-BOTTOM: #bfbfbf 1pt solid; BORDER-LEFT: #bfbfbf 1pt solid; BACKGROUND-COLOR: #354d3f; BORDER-TOP: #bfbfbf 1pt solid
}
TD.xdTableContentCellWithVerticalOffset {
	BORDER-BOTTOM: #bfbfbf 1pt solid; TEXT-ALIGN: left; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 2px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 95px; PADDING-RIGHT: 0px; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 32px; valign: bottom
}
TR.xdTableContentRow {
	MIN-HEIGHT: 140px
}
TD.xdTableContentCell {
	BORDER-BOTTOM: #bfbfbf 1pt solid; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 0px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 0px; valign: top
}
TD.xdTableContentCellWithVerticalFill {
	BORDER-BOTTOM: #bfbfbf 1pt solid; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-BOTTOM: 0px; BACKGROUND-COLOR: #ffffff; PADDING-LEFT: 1px; PADDING-RIGHT: 1px; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-TOP: 0px; valign: top
}
TD.xdTableStyleOneCol {
	PADDING-BOTTOM: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; PADDING-TOP: 4px
}
TR.xdContentRowOneCol {
	MIN-HEIGHT: 45px; valign: center
}
TR.xdHeadingRow {
	MIN-HEIGHT: 27px
}
TD.xdHeadingCell {
	BORDER-BOTTOM: #a6c2b2 1pt solid; TEXT-ALIGN: center; PADDING-BOTTOM: 2px; BACKGROUND-COLOR: #e1eae5; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-TOP: #a6c2b2 1pt solid; PADDING-TOP: 2px; valign: bottom
}
TR.xdSubheadingRow {
	MIN-HEIGHT: 28px
}
TD.xdSubheadingCell {
	BORDER-BOTTOM: #a6c2b2 1pt solid; TEXT-ALIGN: center; PADDING-BOTTOM: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; PADDING-TOP: 4px; valign: bottom
}
TR.xdHeadingRowEmphasis {
	MIN-HEIGHT: 27px
}
TD.xdHeadingCellEmphasis {
	BORDER-BOTTOM: #a6c2b2 1pt solid; TEXT-ALIGN: center; PADDING-BOTTOM: 2px; BACKGROUND-COLOR: #e1eae5; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BORDER-TOP: #a6c2b2 1pt solid; PADDING-TOP: 2px; valign: bottom
}
TR.xdSubheadingRowEmphasis {
	MIN-HEIGHT: 28px
}
TD.xdSubheadingCellEmphasis {
	BORDER-BOTTOM: #a6c2b2 1pt solid; TEXT-ALIGN: center; PADDING-BOTTOM: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; PADDING-TOP: 4px; valign: bottom
}
TR.xdTableLabelControlStackedRow {
	MIN-HEIGHT: 45px
}
TD.xdTableLabelControlStackedCellLabel {
	PADDING-BOTTOM: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 5px; PADDING-TOP: 4px
}
TD.xdTableLabelControlStackedCellComponent {
	PADDING-BOTTOM: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 22px; PADDING-TOP: 4px
}
TR.xdTableRow {
	MIN-HEIGHT: 30px
}
TD.xdTableCellLabel {
	PADDING-BOTTOM: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 5px; PADDING-TOP: 4px;MIN-WIDTH: 275px
}
TD.xdTableCellComponent {
	PADDING-BOTTOM: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 22px; PADDING-TOP: 4px
}
TD.xdTableMiddleCell {
	PADDING-BOTTOM: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 4px
}
TR.xdTableEmphasisRow {
	MIN-HEIGHT: 30px
}
TD.xdTableEmphasisCellLabel {
	PADDING-BOTTOM: 4px; BACKGROUND-COLOR: #c4d6cb; PADDING-LEFT: 22px; PADDING-RIGHT: 5px; PADDING-TOP: 4px
}
TD.xdTableEmphasisCellComponent {
	PADDING-BOTTOM: 4px; BACKGROUND-COLOR: #c4d6cb; PADDING-LEFT: 5px; PADDING-RIGHT: 22px; PADDING-TOP: 4px
}
TD.xdTableMiddleCellEmphasis {
	PADDING-BOTTOM: 4px; BACKGROUND-COLOR: #c4d6cb; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 4px
}
TR.xdTableOffsetRow {
	MIN-HEIGHT: 30px
}
TD.xdTableOffsetCellLabel {
	PADDING-BOTTOM: 4px; BACKGROUND-COLOR: #c4d6cb; PADDING-LEFT: 22px; PADDING-RIGHT: 5px; PADDING-TOP: 4px
}
TD.xdTableOffsetCellComponent {
	PADDING-BOTTOM: 4px; BACKGROUND-COLOR: #c4d6cb; PADDING-LEFT: 5px; PADDING-RIGHT: 22px; PADDING-TOP: 4px
}
P {
	MARGIN-TOP: 0px; COLOR: #354d3f; FONT-SIZE: 11pt
}
H1 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #354d3f; FONT-SIZE: 24pt; FONT-WEIGHT: normal
}
H2 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #354d3f; FONT-SIZE: 16pt; FONT-WEIGHT: bold
}
H3 {
	TEXT-TRANSFORM: uppercase; MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #354d3f; FONT-SIZE: 12pt; FONT-WEIGHT: normal
}
H4 {
	FONT-STYLE: italic; MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #262626; FONT-SIZE: 10pt; FONT-WEIGHT: normal
}
H5 {
	FONT-STYLE: italic; MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #354d3f; FONT-SIZE: 10pt; FONT-WEIGHT: bold
}
H6 {
	MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; COLOR: #262626; FONT-SIZE: 10pt; FONT-WEIGHT: normal
}
BODY {
	COLOR: black
}
</style>
			</head>
			<body>
				<div align="left">
					<table style="BORDER-BOTTOM-STYLE: none; BORDER-LEFT-STYLE: none; WIDTH: 650px; BORDER-COLLAPSE: collapse; WORD-WRAP: break-word; BORDER-TOP-STYLE: none; TABLE-LAYOUT: fixed; BORDER-RIGHT-STYLE: none" class="xdFormLayout xdTableStyleTwoCol">
						<colgroup>
							<col style="WIDTH: 220px"></col>
							<col style="WIDTH: 530px"></col>
						</colgroup>
						<tbody vAlign="top">
							<tr style="MIN-HEIGHT: 31px" class="xdTableRow">
								<td colSpan="2" class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 22px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<div>
										<font size="3">An error occurred during the processing of the following information that was submitted to Neuron. The original document accompanies this email as an attachment. Details of the issue are described below. Thank you!</font>
									</div>
								</td>
							</tr>
							<tr style="MIN-HEIGHT: 31px" class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4> </h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel"></span> </div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Neuron Message ID</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:disableEditing="yes" xd:CtrlId="CTRL1" xd:xctname="PlainText" xd:binding="@messageId" style="WIDTH: 100%; WHITE-SPACE: nowrap">
										<xsl:value-of select="@messageId" />
									</span>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Created On:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:disableEditing="yes" xd:CtrlId="CTRL2" xd:xctname="PlainText" xd:binding="@created" style="WIDTH: 100%; WHITE-SPACE: nowrap">
										<!-- <xsl:value-of select="user:ConvertXmlDateTimeToNormalized(@created)" /> -->
										<xsl:value-of select="@created"/>
									</span>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Neuron Topic</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:disableEditing="yes" xd:CtrlId="CTRL3" xd:xctname="PlainText" xd:binding="Topic" style="WIDTH: 100%; WHITE-SPACE: nowrap">
											<xsl:value-of select="Topic" />
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Neuron Party:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel"><span hideFocus="1" class="xdTextBox" title="" xd:CtrlId="CTRL13" xd:xctname="PlainText" xd:binding="Party" tabIndex="0" style="WIDTH: 100%">
												<xsl:value-of select="Party" />
											</span>
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Machine:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel">
											<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:CtrlId="CTRL14" xd:xctname="PlainText" xd:binding="Machine" tabIndex="0" style="WIDTH: 100%">
												<xsl:value-of select="Machine" />
											</span>
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<em>
										<font color="#262626"></font>
									</em>
									<h4>User Name:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel">
											<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:CtrlId="CTRL15" xd:xctname="PlainText" xd:binding="User" tabIndex="0" style="WIDTH: 100%">
												<xsl:value-of select="User" />
											</span>
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<em>
										<font color="#262626"></font>
									</em>
									<h4>Source File Name:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel">
											<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:CtrlId="CTRL15" xd:xctname="PlainText" xd:binding="FileName" tabIndex="0" style="WIDTH: 100%">
												<xsl:value-of select="FileName" />
											</span>
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<em>
										<font color="#262626"></font>
									</em>
									<h4>File Attachment Type:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel">
											<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:CtrlId="CTRL15" xd:xctname="PlainText" xd:binding="BodyType" tabIndex="0" style="WIDTH: 100%">
												<xsl:value-of select="BodyType" />
											</span>
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4> </h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div> </div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Failure Date:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:disableEditing="yes" xd:CtrlId="CTRL4" xd:xctname="PlainText" xd:binding="FailureDatetime" style="WIDTH: 100%; WHITE-SPACE: nowrap">
											<!--  <xsl:value-of select="user:ConvertXmlDateTimeToNormalized(FailureDatetime)" /> -->
											<xsl:value-of select="FailureDatetime"/>
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Failure Type:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div>
										<span class="xdlabel"></span><span hideFocus="1" class="xdTextBox" title="" xd:disableEditing="yes" xd:CtrlId="CTRL7" xd:xctname="PlainText" xd:binding="FailureType" style="WIDTH: 100%; WHITE-SPACE: nowrap">
											<xsl:value-of select="FailureType" />
										</span>
									</div>
								</td>
							</tr>
							<tr class="xdTableRow">
								<td class="xdTableCellLabel" style="PADDING-BOTTOM: 4px; PADDING-LEFT: 0px; PADDING-RIGHT: 5px; VERTICAL-ALIGN: top; PADDING-TOP: 4px">
									<h4>Failure Details:</h4>
								</td>
								<td vAlign="top" class="xdTableCellComponent">
									<div><span hideFocus="1" class="xdTextBox" title="" xd:disableEditing="yes" xd:CtrlId="CTRL8" xd:xctname="PlainText" xd:binding="FailureDetail" style="OVERFLOW-X: auto; OVERFLOW-Y: auto; WIDTH: 100%; WORD-WRAP: break-word; WHITE-SPACE: normal">
											<xsl:value-of select="FailureDetail" />
										</span>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div align="center"> </div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>