<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestFile.aspx.cs" Inherits="ProtectedDownload.RequestFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Pitt-Logo.gif" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Please  select the file you want to download and enter your contact information so we can send you a download link."></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Description" DataValueField="FileName" Height="17px" Width="300px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:rocConnectionString %>" SelectCommand="SELECT DISTINCT Description, FileName, ID FROM FileList ORDER BY ID"></asp:SqlDataSource>
        <br />
        <br />
    </div>
    <table>
        <tr>
        
        <td>
            First Name
        </td>
        <td>
            <asp:TextBox ID="TextBoxFName" runat="server" Width="200px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TextBoxFName" ErrorMessage="Required"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
            <td>
                Last Name
            </td>
            <td>
                <asp:TextBox ID="TextBoxLName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBoxLName" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Email Address
            </td>
            <td>
                <asp:TextBox ID="TextBoxEmail" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="TextBoxEmail" ErrorMessage="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBoxEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>   
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
    </form>
</body>
</html>
