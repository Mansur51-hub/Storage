﻿using ObjectStorage.MasterNode;
using UI.Tools;
using ObjectStorage.UserInterface.Tools;

namespace UI.Commands
{
    public class BalanceNodesCommand : ICommand
    {
        public void Execute(string command, IMaster master)
        {
            try
            {
                master.BalanceNodes();

                Response.PrintResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string GetDescription()
        {
            string description = "Balancing nodes by load";

            return description;
        }

        public string GetName()
        {
            const string name = "/balance-node";

            return name;
        }
    }
}
