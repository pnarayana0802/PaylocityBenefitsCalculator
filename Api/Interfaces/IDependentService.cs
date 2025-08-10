using Api.Dtos.Dependent;

namespace Api.Interfaces
{
    public interface IDependentService
    {
        Task<List<GetDependentDto>> GetAllDependents();
        GetDependentDto? GetDependentById(int depId);
        GetDependentDto AddDependentAsync(GetDependentDto dependent);
        bool UpdateDependentAsync(int depId,  GetDependentDto dependent);
        bool DeleteDependentAsync(int depId);
    }
}
