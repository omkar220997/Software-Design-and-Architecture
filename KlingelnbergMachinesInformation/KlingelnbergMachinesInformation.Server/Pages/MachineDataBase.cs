using KlingelnbergMachinesInformation.Api.Entity;
using KlingelnbergMachinesInformation.Server.Services;
using Microsoft.AspNetCore.Components;

namespace KlingelnbergMachinesInformation.Server.Pages
{
    public class MachineDataBase:ComponentBase
    {
        [Inject]
        protected IKIPLMachineData KIPLMachineData { get; set; }

        [Parameter]
        public string MachineType { get; set; }

        [Parameter]
        public string AssetName { get; set; }

        public IEnumerable<Assets>? Asset { get; set; }

        protected async void OnButtonClick1()
        {
            Asset=(await KIPLMachineData.GetAllMachineData()).ToList()??Enumerable.Empty<Assets>();
            StateHasChanged();
        }
        protected async void OnButtonClick2()
        {
            Asset= (await KIPLMachineData.GetMachineOfLatestAssets()).ToList()??Enumerable.Empty<Assets>();
            StateHasChanged();
        }

        protected async void OnButtonClick3()
        {
            if(AssetName!=null)
            {
                Asset = (await KIPLMachineData.GetAllMachineTypeOnUsedAsset(AssetName)).ToList()??Enumerable.Empty<Assets>();
                StateHasChanged();
            }
        }
        protected async void OnButtonClick4()
        {
            if (MachineType != null)
            {
                Asset=(await KIPLMachineData.GetAssetForMachineType(MachineType)).ToList()??Enumerable.Empty<Assets>();
                StateHasChanged();
            }
        }
    }
}
