using System;
namespace BrewingModel.BrewingProcessEquipment
{
    public interface IStateDescription
    {
        string GetStateDescription();
        string GetShortState();
    }
}
