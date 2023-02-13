using KlingelnbergMachinesInformation.Api.Entity;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace KlingelnbergMachinesInformation.Server.Services
{
    public class KIPLMachineData : IKIPLMachineData
    {
        private readonly HttpClient httpClient;
        public KIPLMachineData(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Assets>> GetAllMachineData()
        {
            return await httpClient.GetFromJsonAsync<Assets[]>($"api/machines-information") ?? Enumerable.Empty<Assets>();

        }


        public async Task<IEnumerable<Assets>> GetAssetForMachineType(string machineType)
        {
            
            return await httpClient.GetFromJsonAsync<Assets[]>($"api/machines-information/machinetype?machineType={machineType}") ?? Enumerable.Empty<Assets>();

        }


        public async Task<IEnumerable<Assets>> GetAllMachineTypeOnUsedAsset(string assetName)
        {
            return await httpClient.GetFromJsonAsync<Assets[]>($"api/machines-information/assetname?assetName={assetName}") ?? Enumerable.Empty<Assets>();

        }


        public async Task<IEnumerable<Assets>> GetMachineOfLatestAssets()
        {
           return await httpClient.GetFromJsonAsync<Assets[]>($"/api/machines-information/latest-series-of-asset")?? Enumerable.Empty<Assets>(); 
           
        }


    }
}
