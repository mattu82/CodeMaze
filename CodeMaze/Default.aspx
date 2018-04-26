<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CodeMaze.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Maze Solver by Matthew Van Vugt</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnSolve" runat="server" OnClick="btnSolve_Click" Text="Solve" /><br />
            <asp:TextBox ID="txtMaze" runat="server" Columns="185" TextMode="MultiLine" Rows="46" Wrap="False"></asp:TextBox>
        </div>
    </form>
</body>
</html>
