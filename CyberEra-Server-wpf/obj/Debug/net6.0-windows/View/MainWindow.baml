   M S B A M L   `   `   `  ????N  JCyberEra-Server-wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CyberEra_Server_wpfCyberEra_Server_wpf  5   	   C&clr-namespace:CyberEra_Server_wpf.ViewCyberEra_Server_wpf.View  5   	   M+clr-namespace:CyberEra_Server_wpf.ViewModelCyberEra_Server_wpf.ViewModel  5   	   $    CyberEra_Server_wpf.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:CyberEra_Server_wpf     5   	   5uc&clr-namespace:CyberEra_Server_wpf.View     5   	   A	viewmodel+clr-namespace:CyberEra_Server_wpf.ViewModel     5   	   $	??400??5   	   $	??720??5   	   $	??500??5   	   $	??900??5   	     ? Icon$  /View/icon.png??5   	    ? Title$ CyberEra | Server??5   	   $	??450??6#   $	??800??60   .??5      ?  ? RowDefinitions 5   
   ?? 5      6   ?? 5      6   ?? 5      6   ?? 5      6   ?? 5      6    ? ColumnDefinitions 5   
   ?? 5      $
??160* ?6   6.   ?? 5      $
??377* ?6   6.   ?? 5      $
??263* ?6   .+?5    
   ?? $??5??6   $??3??6%   ???#131C296J   ?? 5"   
   $??5??6   ???#1F2A38 J? Effect 5#      3  -System.Windows.Media.Effects.DropShadowEffect  5$         
BlurRadius$ 8Q?6#      ShadowDepth$ 0Q?62   5&      ?? 5(   
   ???#F1AA02
?????? $??	ExtraBold"?6m   $??Brutal Type)?6?   $??0??6?   ??6   ?? *ChangeComputerVisibility+.??6?   Компьютеры6?   ?? 5)   
   ???#F1AA02
?????? $??	ExtraBold"?6m   $??Brutal Type)?6?   $??1??6?   ??6   ?? *ChangePasswordVisibility+.??6?   Пароли6?   ?? 50   
   ???#F1AA02
?????? $??	ExtraBold"?6A   $??Brutal Type)?6X   $??1??6q   $??100 0q?6?   $??4??6?   ??6?   ?? *OpenPasswordGiver+.??6?   Выдать пароль6?   ?? 51   
   ???#F1AA02
?????? $??	ExtraBold"?6@   $??Brutal Type)?6W   $??2??6p   $
??30 0q?6?   $??4??6?   ??6?   ?? *KickCommand+.??6?   '%Отключить компьютер6?   ?? 54   
   $??1??6?   $??2??6?   $??4??6?   $??150 50 50 50q?6?   y?6   ?? *IsComputersHidden+ ?? UpdateSourceTrigger$ PropertyChanged=? ?? NotifyOnSourceUpdated$
 True??.??55      ?? ???#1F2A38$??2??6+   ??6?   ?? *	Computers+$ PropertyChanged=?	 ?? SelectedItem	 56      ?? *SelectedComputer+??57      ?? 58      
 ?? 
TargetType"
 ??6   .?59      ??  ?? Property ? ?? ?? Value$ Stretch=?6G   5;      ??5=      ?? 5>      .R?5?      '   !CyberEra_Server_wpf.View.Computer  $	??0 2q?6&    ? DataContext 63   ?? 6M   5@      5B      5C      ?? 5E   
   $??1??6   $??2??6?   $??4??6?   $??150 50 50 50q?6?   y?6   ?? *IsPasswordHiden+$ PropertyChanged=?$
 True??.??5F      ?? ???#1F2A38$??2??6+   ??6?   ?? *	Passwords+$ PropertyChanged=???5G      ?? 5H      "
 ??6   .?5I      ??  ? ??$ Stretch=?6G   5K      ??5M      ?? 5N      .R?5O        $	??0 2q?6&    63   ?? 6M   5P      5R      5S      5U      5V      