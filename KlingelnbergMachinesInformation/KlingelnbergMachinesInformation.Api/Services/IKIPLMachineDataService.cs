using KlingelnbergMachinesInformation.Api.Entity;
using System.Threading.Tasks;

namespace KlingelnbergMachinesInformation.Api.Services
{
    public interface IKIPLMachineDataService
    {
        Task<IEnumerable<Assets>> GetAllMachineData();
        Task<IEnumerable<Assets>> GetAssetForMachineType(string machineType);
        Task<IEnumerable<Assets>> GetAllMachineTypeOnUsedAsset(string assetName);
        Task<IEnumerable<Assets>> GetMachineOfLatestAssets();
        Task<IEnumerable<Assets>> InsertDataToDb();
        Task<IEnumerable<Assets>> DeleteDataFromDb();
    }
}
