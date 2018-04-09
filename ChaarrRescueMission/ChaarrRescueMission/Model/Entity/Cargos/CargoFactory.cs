using ChaarrRescueMission.Model.Entity.Cargos;
using ChaarrRescueMission.Properties;

namespace ChaarrRescueMission.Model.Cargos
{
    public class CargoFactory
    {
        private enum CargoType { Restart, Place, ProductSupplies, ProductNoSupplies, Repair, Order, NoCargo }

        public static Cargo Create(string action,
            string place, string repairing, string production, string order, string suppliesValue)
        {
            switch (CheckCargoType(action, place, repairing, production, order, suppliesValue))
            {
                case CargoType.Restart:
                    return new Cargo()
                    {
                        Command = action,
                    };
                case CargoType.Place:
                    return new Cargo()
                    {
                        Command = action,
                        Parameter = place,
                    };
                case CargoType.ProductSupplies:
                    return new Cargo()
                    {
                        Command = action,
                        Parameter = production,
                        Value = suppliesValue,
                    };
                case CargoType.ProductNoSupplies:
                    return new Cargo()
                    {
                        Command = action,
                        Parameter = production,
                    };
                case CargoType.Repair:
                    return new Cargo()
                    {
                        Command = action,
                        Parameter = repairing,
                    };
                case CargoType.Order:
                    return new Cargo()
                    {
                        Command = action,
                        Parameter = order,
                        Value = place,
                    };
                default:
                    return new Cargo();
            }
        }

        private static CargoType CheckCargoType(string action,
            string place, string repairing, string production, string order, string suppliesValue)
        {
            if (action == Resources.CaptionRestart)
                return CargoType.Restart;
            if (action == Resources.CaptionScan ||
                action == Resources.CaptionMove ||
                action == Resources.CaptionHarvest)
                return CargoType.Place;
            if (action == Resources.CaptionProduce)
            {
                if (production == Resources.CaptionSupplies)
                    return CargoType.ProductSupplies;
                if (production == Resources.CaptionDecoy ||
                    production == Resources.CaptionWeapons ||
                    production == Resources.CaptionTools ||
                    production == Resources.CaptionEnergy ||
                    production == Resources.CaptionShuttlewrench)
                    return CargoType.ProductNoSupplies;
            }
            if (action == Resources.CaptionRepair)
                return CargoType.Repair;
            if (action == Resources.CaptionOrder)
                return CargoType.Order;              
            return CargoType.NoCargo;
        }
    }
}
