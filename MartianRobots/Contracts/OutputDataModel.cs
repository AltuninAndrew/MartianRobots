using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Contracts
{
    public class OutputDataModel
    {
        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; set; }

        public ICollection<IActorModel> CollectionRobotsOutputInfo { get; set; }

        public OutputDataModel(ICollection<IActorModel> collectionRobotsOutputInfo)
        {
            CollectionRobotsOutputInfo = collectionRobotsOutputInfo;
            IsSuccess = true;
        }

        public OutputDataModel(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public interface IActorModel
        {
            public string BaseOutputActorStringVeiw { get; }
        }
    }
}
