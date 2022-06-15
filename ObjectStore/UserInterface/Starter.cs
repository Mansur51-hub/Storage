using ObjectStorage.MasterNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Controllers;

namespace UI
{
    public class Starter
    {
        public static void Execute()
        {
            IController controller = new Controller();

            controller.Execute(GetSetUp());
        }

        private static IMaster GetSetUp()
        {
            return new Master();
        }
    }
}
