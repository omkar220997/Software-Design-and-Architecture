using Microsoft.Extensions.Options;
using MongoDB.Driver;
using KlingelnbergMachinesInformation.Api.Configurations;
using KlingelnbergMachinesInformation.Api.Entity;


namespace KlingelnbergMachinesInformation.Api.Services
{
    public class KIPLMachineDataService : IKIPLMachineDataService
    {
        private readonly IMongoCollection<Assets> _KIPLDbSetting;

        public KIPLMachineDataService(IOptions<KIPLDatabaseSetting> KIPLDbSetting)
        {
            var mongoClient = new MongoClient(KIPLDbSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(KIPLDbSetting.Value.DatabaseName);
            _KIPLDbSetting = mongoDatabase.GetCollection<Assets>(KIPLDbSetting.Value.CollectionName);
        }

        public async  Task<IEnumerable<Assets>> GetAllMachineData()
        {
            var filter = Builders<Assets>.Filter.Empty;
            return await _KIPLDbSetting.Find(filter).ToListAsync();
                   
        }

        public async Task<IEnumerable<Assets>> GetAllMachineTypeOnUsedAsset(string assetName)
        {
            if(assetName == null)
            {
                throw new ArgumentNullException(nameof(assetName));
            }
            else
            {
                //var filter = Builders<Assets>.Filter.Empty;
                return await _KIPLDbSetting.Find(asset=>asset.AssetName.Equals(assetName)).ToListAsync();
                
            }
        }

        public async Task<IEnumerable<Assets>> GetAssetForMachineType(string machineType)
        {
            if (machineType == null)
            {
                throw new ArgumentNullException(nameof(machineType));
            }
            else
            {
                //var filter = Builders<Assets>.Filter.Empty;
                return await _KIPLDbSetting.Find(asset => asset.MachineName.Equals(machineType)).ToListAsync();
                
            }
        }

        public async Task<IEnumerable<Assets>> GetMachineOfLatestAssets()
        {
            var filter = Builders<Assets>.Filter.Empty;
            var assets= _KIPLDbSetting.Find(filter).ToList();

            Dictionary<string, Assets> latestSeriesOfAsset=new Dictionary<string, Assets>();
            List<string>machineName=new List<string>();
            if(assets != null)
            {
                foreach(var asset in assets)
                {
                    if (!latestSeriesOfAsset.ContainsKey(asset.AssetName))
                    {
                        var machineNameForAsset=await _KIPLDbSetting.Find(assetname=>assetname.AssetName.Equals(asset.AssetName)).ToListAsync();
                        machineNameForAsset = machineNameForAsset.OrderByDescending(asset=> int.Parse(asset.SeriesNumberOfAsset.Substring(1))).ToList();

                        if (!machineName.Contains(machineNameForAsset[0].MachineName))
                        {
                            latestSeriesOfAsset.Add(machineNameForAsset[0].AssetName, machineNameForAsset[0]);
                            for(int i=1; i<machineNameForAsset.Count; i++)
                            {
                                machineName.Add(machineNameForAsset[i].MachineName);
                            }
                        }
                    }
                }
            }
            return latestSeriesOfAsset.Values.ToList();
        }

        public async Task<IEnumerable<Assets>> InsertDataToDb()
        {
            var fileName = "matrix.txt";
            var filePath = Path.Combine(Environment.CurrentDirectory, "DbSeed", fileName);
            var lines = File.ReadAllLines("DbSeed/matrix.txt").ToList();
            var allMachineData = lines.Select(line => new Assets
            {
                MachineName = line.Split(",")[0].Trim(),
                AssetName = line.Split(",")[1].Trim(),
                SeriesNumberOfAsset = line.Split(",")[2].Trim()
            }).ToList();

            _KIPLDbSetting.InsertMany(allMachineData);
            return allMachineData;

        }

        public async Task<IEnumerable<Assets>> DeleteDataFromDb()
        {
            var filter=Builders<Assets>.Filter.Empty;
            _KIPLDbSetting.DeleteMany(filter);
            return null;
        }

       
    }
}
