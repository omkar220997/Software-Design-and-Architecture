using KlingelnbergMachinesInformation.Api.Entity;

namespace KlingelnbergMachinesInformation.Server.Services
{
    public interface IKIPLMachineData
    {
        Task<IEnumerable<Assets>> GetAllMachineData();
        Task<IEnumerable<Assets>> GetAssetForMachineType(string machineType);
        Task<IEnumerable<Assets>> GetAllMachineTypeOnUsedAsset(string assetName);
        Task<IEnumerable<Assets>> GetMachineOfLatestAssets();
      
    }
}
