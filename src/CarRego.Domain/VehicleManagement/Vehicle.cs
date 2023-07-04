using CarRego.Domain.VehicleManagement.Exceptions;

namespace CarRego.Domain.VehicleManagement;

public class Vehicle
{
    private List<Owner> owners = new List<Owner>();

    private Vehicle() { }
    private Vehicle(VIN vin) 
    {
        VIN = vin;
    }

    public Owner SetOwner(Person newOwner)
    {
        if (CurrentOwner != null)
        {
            if (newOwner.Id == CurrentOwner.Id)
            {
                throw new DuplicateOwnerException();
            }
            CurrentOwner!.EndOwnership();
        }

        var owner = Owner.Create(newOwner);
        owners.Add(owner);

        return owner;
    }

    public static Vehicle Create(string vin, Person owner)
    {
        if (!VIN.IsValid(vin))
        {
            throw new InvalidVinException();
        }

        var vehicle = new Vehicle(VIN.Create(vin));

        vehicle.owners.Add(Owner.Create(owner));

        return vehicle;
    }

    public VIN VIN { get; private set; } = VIN.Empty;
    public Owner? CurrentOwner => owners.FirstOrDefault(x => x.To == null);
    public Owner[] PreviousOwners => owners.Where(x => x.To != null).ToArray();
}