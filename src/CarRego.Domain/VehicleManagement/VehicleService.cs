using CarRego.Domain.Infrastructure;
using CarRego.Domain.VehicleManagement.Data;
using CarRego.Domain.VehicleManagement.DTOs;
using CarRego.Domain.VehicleManagement.Exceptions;

namespace CarRego.Domain.VehicleManagement;

public class VehicleService
{
    private readonly VehicleManagementContext context;

    public VehicleService(VehicleManagementContext context)
    {
        this.context = context;
    }

    public async Task<ServiceResult<DTOs.DTOs.VehicleDTO>> AddVehicle(string vin, int personId)
    {
        if (string.IsNullOrWhiteSpace(vin))
        {
            return ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(new ArgumentNullException(nameof(vin)));
        }

        if (!VIN.IsValid(vin))
        {
            return ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(new InvalidVinException());
        }

        var existing = await context.VehicleWithVIN(VIN.Create(vin));
        if (existing != null)
        {
            return ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(new DuplicateVinException());
        }

        var owner = await context.PersonWithId(personId);
        if (owner == null)
        {
            return ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(new PersonNotFoundException());
        }

        try
        {
            var vehicle = Vehicle.Create(vin, owner);
            context.Add(vehicle);
            await context.SaveChangesAsync();
            return ServiceResult.Success(vehicle.ToModel());
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(ex);
        }
    }

    public async Task<ServiceResult<DTOs.DTOs.VehicleDTO>> GetVehicleByVin(string vin)
    {
        if (!VIN.IsValid(vin))
        {
            return ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(new InvalidVinException());
        }

        var vehicle = await context.VehicleWithVIN(VIN.Create(vin), true);

        return vehicle != null ?
                    ServiceResult.Success(vehicle.ToModel()) :
                    ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(new VehicleNotFoundException());
    }

    public async Task<ServiceResult<DTOs.DTOs.VehicleDTO>> GetVehicleById(int id)
    {
        var vehicle = await context.VehicleWithId(id, true);

        return vehicle != null ?
                    ServiceResult.Success(vehicle.ToModel()) :
                    ServiceResult.Fail<DTOs.DTOs.VehicleDTO>(new VehicleNotFoundException());
    }

    public async Task<ServiceResult<DTOs.DTOs.OwnerDTO>> GetCurrentOwnerByVin(string vin)
    {
        if (!VIN.IsValid(vin))
        {
            return ServiceResult.Fail<DTOs.DTOs.OwnerDTO>(new InvalidVinException());
        }

        var vehicle = await context.VehicleWithVIN(VIN.Create(vin), true);
        if (vehicle == null)
        {
            return ServiceResult.Fail<DTOs.DTOs.OwnerDTO>(new VehicleNotFoundException());
        }

        if (vehicle.CurrentOwner == null)
        {
            return ServiceResult.Fail<DTOs.DTOs.OwnerDTO>(new OwnerNotFoundException());
        }

        return ServiceResult.Success(vehicle.CurrentOwner.ToModel());
    }

    public async Task<ServiceResult<DTOs.DTOs.OwnerDTO>> SetCurrentOwner(string vin, int personId)
    {
        if (!VIN.IsValid(vin))
        {
            return ServiceResult.Fail<DTOs.DTOs.OwnerDTO>(new InvalidVinException());
        }

        var vehicle = await context.VehicleWithVIN(VIN.Create(vin));
        if (vehicle == null)
        {
            return ServiceResult.Fail<DTOs.DTOs.OwnerDTO>(new VehicleNotFoundException());
        }
        
        var newOwner = await context.PersonWithId(personId);
        if (newOwner == null)
        {
            return ServiceResult.Fail<DTOs.DTOs.OwnerDTO>(new PersonNotFoundException());
        }

        try
        {
            vehicle.SetOwner(newOwner);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail<DTOs.DTOs.OwnerDTO>(ex);
        }

        return ServiceResult.Success(vehicle.CurrentOwner!.ToModel());
    }
}