namespace CarRego.Domain.VehicleManagement.DTOs;

public static class DTOExtensions
{
    public static DTOs.VehicleDTO ToModel(this Vehicle vehicle)
        => new DTOs.VehicleDTO(vehicle.VIN.Value,
            vehicle.CurrentOwner != null ? vehicle.CurrentOwner.ToModel() : null,
            vehicle.PreviousOwners.OrderBy(x => x.To).Select(x => x.ToModel()).ToArray());
    public static DTOs.OwnerDTO ToModel(this Owner owner)
        => new DTOs.OwnerDTO(owner.Id, owner.FirstName, owner.LastName, owner.From, owner.To);
}