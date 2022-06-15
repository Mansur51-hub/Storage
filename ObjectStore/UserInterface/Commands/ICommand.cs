using ObjectStorage.MasterNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Commands
{
    public interface ICommand
    {
        string GetName();
        string GetDescription();
        void Execute(string command, IMaster master);
    }
}
