using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KlingelnbergMachinesInformation.Api.Entity;
using KlingelnbergMachinesInformation.Api.Services;

namespace KlingelnbergMachinesInformation.Api.Controllers
{
    [Route("api/machines-information")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "KlingelnbergMachineDataApiSpecification")]
    [Produces("application/json")]

    public class MachineDataController : ControllerBase
    {
        private readonly IKIPLMachineDataService _KIPLMachineDataService;


        public MachineDataController(IKIPLMachineDataService kIPLMachineDataService)
        {
            _KIPLMachineDataService = kIPLMachineDataService;


        }

        /// <summary>
        /// Get all machines data.
        /// </summary>
        /// <returns>All machines data with asset name and series number of that asset</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assets>>> GetAllMachineData()
        {
            var allMachineData = await _KIPLMachineDataService.GetAllMachineData();
            return new OkObjectResult(allMachineData);
        }

        /// <summary>
        /// Get all machine data on which given asset is used.
        /// </summary>
        /// <param name="assetName">Enter Asset Name here.</param>
        /// <returns>All machine data for respective asset</returns>
        [HttpGet("assetname", Name = "machine-machinedata")]
        public async Task<ActionResult<IEnumerable<Assets>>> GetAllMachineTypeOnUsedAsset(string assetName)
        {
            var machineData = await _KIPLMachineDataService.GetAllMachineTypeOnUsedAsset(assetName);
            return new OkObjectResult(machineData);
        }
        /// <summary>
        /// Get all asset names for given machine.
        /// </summary>
        /// <param name="machineType">Enter Machine Name here.</param>
        /// <returns>All asset data used by given machine</returns>
        [HttpGet("machinetype", Name = "machine-assetname")]
        public async Task<ActionResult<IEnumerable<Assets>>> GetAssetForMachineType(string machineType)
        {
            var machineData = await _KIPLMachineDataService.GetAssetForMachineType(machineType);
            return new OkObjectResult(machineData);
        }
        /// <summary>
        /// Get machine data whihch are using latest version of all assets.
        /// </summary>
        /// <returns>Machine used latest version of all assets</returns>
        [HttpGet("latest-series-of-asset")]
        public async Task<ActionResult<IEnumerable<Assets>>> GetMachineOfLatestAssets()
        {
            var machineData = await _KIPLMachineDataService.GetMachineOfLatestAssets();
            return new OkObjectResult(machineData);
        }

        /// <summary>
        /// Upload file data to database.
        /// </summary>
        /// <returns>Upload the data in database</returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<IEnumerable<Assets>>> PostDataToDb()
        {
            await _KIPLMachineDataService.InsertDataToDb();
            return StatusCode(StatusCodes.Status201Created);

        }

        /// <summary>
        /// Delete All Data from database
        /// </summary>
        /// <returns>All data will be delete</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<IEnumerable<Assets>>> DeleteAllDataFromDb()
        {
            await _KIPLMachineDataService.DeleteDataFromDb();
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
