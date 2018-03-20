using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Properties;

namespace ChaarrRescueMission.Model.Factory
{
    class CargoFactory
    {
        public static Cargo Create(string action,
            string place, string repairing, string production, string order, string suppliesValue)
        {
            if (action == Resources.CaptionScan ||
                action == Resources.CaptionMove ||
                action == Resources.CaptionHarvest)
                return new Cargo()
                {
                    Command = action,
                    Parameter = place,
                };
            if (action == Resources.CaptionProduce)
            {
                if (production == Resources.CaptionSupplies)
                    return new Cargo()
                    {
                        Command = action,
                        Parameter = production,
                        Value = suppliesValue,
                    };
                if (production == Resources.CaptionDecoy ||
                    production == Resources.CaptionWeapons ||
                    production == Resources.CaptionTools || 
                    production == Resources.CaptionEnergy ||
                    production == Resources.CaptionShuttlewrench)
                    return new Cargo()
                    {
                        Command = action,
                        Parameter = production,
                    };
            }
            if (action == Resources.CaptionRepair)
                return new Cargo()
                {
                    Command = action,
                    Parameter = repairing,
                };
            if (action == Resources.CaptionOrder)
                return new Cargo()
                {
                    Command = action,
                    Parameter = order,
                    Value = place,
                };
            if (action == Resources.CaptionRestart)
                return new Cargo()
                {
                    Command = action,
                };
            return new Cargo();
        }
    }
}
