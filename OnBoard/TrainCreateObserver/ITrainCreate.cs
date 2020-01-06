using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public interface ITrainCreate
    {
        void InformWatcher();
        void AddWatcher(ITrainCreateWatcher watcher);
    }
}
