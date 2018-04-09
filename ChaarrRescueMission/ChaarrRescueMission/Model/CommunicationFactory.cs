using ChaarrRescueMission.Enum;
using ChaarrRescueMission.Properties;

namespace ChaarrRescueMission.Model
{
    class CommunicationFactory
    {
        public static CommunicationManager Create(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Chaarr:
                    return new CommunicationManager()
                    {
                        ExecuteConnectionString = Resources.CaptionChaarrExecute,
                        DescribeConnectionString = Resources.CaptionChaarrDescribe,
                    };
                case GameType.Simulation:
                    return new CommunicationManager()
                    {
                        ExecuteConnectionString = Resources.CaptionSimulationExecute,
                        DescribeConnectionString = Resources.CaptionSimulationDescribe,
                    };
                default:
                    return new CommunicationManager();
            }
        }
    }
}
