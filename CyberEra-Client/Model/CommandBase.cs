using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberEra_Client.Model {
    internal class CommandBase {
        public string CommandName { get; set; }
        public string CommandArgs { get; set; }

        public CommandBase(string commandName, string commandArgs) {
            this.CommandName = commandName;
            this.CommandArgs = commandArgs;
        }
    }
}
