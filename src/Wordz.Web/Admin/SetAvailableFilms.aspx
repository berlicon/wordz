<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetAvailableFilms.aspx.cs" Inherits="Wordz.Web.Admin.SetAvailableFilms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<asp:Button ID="btnLoad" runat="server" Text="Load" onclick="btnLoad_Click" />
		Start at index <asp:TextBox ID="txtStartIndex" runat="server" Text="60" />
		<div id="divContent" runat="server" />    
    </form>
</body>
</html>
