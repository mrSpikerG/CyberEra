   M S B A M L   `   `   `  ????N  JCyberEra-Server-wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CyberEra_Server_wpf.ViewCyberEra_Server_wpf.View  5   	   M+clr-namespace:CyberEra_Server_wpf.ViewModelCyberEra_Server_wpf.ViewModel  6>   ,    &CyberEra_Server_wpf.View.PasswordGiver   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:CyberEra_Server_wpf.View     5   	   A	viewmodel+clr-namespace:CyberEra_Server_wpf.ViewModel     6>   $	??250??5   	   $	??300??5	   	   $	??300??5
   	   $	??350??5   	     ? Icon$  /View/icon.png??5   
    ? Title$ Password Giver??5   	   $	??250??6    $	??300??6-   .??5      ? ???#131C29 ? RowDefinitions 5   
   ?? 5      6   ?? 5      $
??0.5* ?6   6,   .+?5   
   ?? ???#fff ?? TextAlignment$ Center=?6E   $??40 55q?6f   ???#1F2A38??6   ?? *PasswordMinutes+.??6?    J? Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         	Direction$
 -100Q?6#      
BlurRadius$ 20Q?64   6F   .??5       ? ToolTip$
 dddd??5      .??5      ?? 5   
   ???#F1AA02
?????? $??	ExtraBold"?6a   $
??60 0q?6x   $??Brutal Type)?6?   $??1??6?   ??6   ?? *SendPassword+.??6?   Отправить6?   5      5       