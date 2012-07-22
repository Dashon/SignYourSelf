<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
   var htmlAttributes = new Signyourself2012.HtmlPropertiesAttribute();
   if(ViewData.ModelMetadata.AdditionalValues.ContainsKey("HtmlAttributes"))
      htmlAttributes = (Signyourself2012.HtmlPropertiesAttribute) ViewData.ModelMetadata.AdditionalValues["HtmlAttributes"];
   htmlAttributes.HtmlAttributes().Add("class", "text-box single-line " + htmlAttributes.CssClass);
 %>
    <span>
          <%=Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue,htmlAttributes.HtmlAttributes())%>                                      
   </span>
   

