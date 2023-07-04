namespace CarRego.Domain.PeopleManagement;

public interface IHaveId<T>
{
    T Id { get; }
}