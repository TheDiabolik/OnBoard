using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoard
{
    public interface ITrainObserver
    {
        void InformTrainCreatedWatcher();

        void InformTrainMovementCreatedWatcher();
        void InformTrainMovementRouteCreatedWatcher();

        void InformTrainMovementUIWatcher();

        void AddTrainCreatedWatcher(ITrainCreatedWatcher watcher);

        void AddTrainMovementCreatedWatcher(ITrainMovementCreatedWatcher watcher);
        void AddTrainMovementRouteCreatedWatcher(ITrainMovementRouteCreatedWatcher watcher);
        void AddTrainMovementUIWatcher(ITrainMovementUIWatcher watcher);
    }
}
