using ObjectStorage.MasterNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Commands;

namespace UI.Controllers
{
    public interface IController
    {
        void Execute(IMaster master);
        ICommand FindCommand(string command);
    }
}
