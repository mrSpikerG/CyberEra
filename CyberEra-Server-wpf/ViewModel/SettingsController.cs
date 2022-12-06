
using CyberEra_Server_wpf.Model;
using System;
using System.IO;
using System.Text.Json;

namespace CyberEra_Server_wpf.Control {
    internal class SettingsController {
        private static SettingsController? SettingsInstance;
        private readonly string Name = "Config.json";
        private Settings? Settings;


        public static SettingsController GetInstance() {
            return SettingsInstance ?? (SettingsInstance = new SettingsController());
        }

        public Settings? GetSettings() {
            if (this.Settings == null) {
                this.ReadFromJSON();
            }
            return this.Settings;
        }

        private bool ReadFromJSON() {
            try {
                if (!File.Exists(this.Name)) {
                    SetDefaultJSON();
                } else {
                    this.Settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(this.Name));
                }

                return true;
            } catch {
                return false;
            }
        }

        private void SetDefaultJSON() {

            this.Settings = new Settings();
            this.Settings.Port = 8888;
            this.Settings.IpAddress = "127.0.0.1";
            this.Settings.IsBlocked = false;
            this.Settings.DBConnectionString = "Data Source=host;Initial Catalog=db_name;User Id=user_name;Password=db_password";
            File.WriteAllText(this.Name, JsonSerializer.Serialize<Settings>(this.Settings));
        }

    }
}
