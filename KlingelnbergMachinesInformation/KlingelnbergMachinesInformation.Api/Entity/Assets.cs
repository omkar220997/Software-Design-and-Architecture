using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace KlingelnbergMachinesInformation.Api.Entity
{
    /// <summary>
    /// Machines data with Machine name, Asset name and series number of asset.
    /// </summary>
    public class Assets
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }=null;

        /// <summary>
        /// Machine Type
        /// </summary>
        [Required]
        [BsonElement("machine_type")]
        public string MachineName { get; set; }

        /// <summary>
        /// Asset name
        /// </summary>
        [Required]
        [BsonElement("asset_name")]
        public string AssetName { get; set; }

        /// <summary>
        /// Series number of asset
        /// </summary>
        [Required]
        [BsonElement("series_number")]
        public string SeriesNumberOfAsset { get; set; }

        public Assets(string machineName, string assetName, string seriesNumberOfAsset)
        {
            MachineName = machineName;
            AssetName = assetName;
            SeriesNumberOfAsset = seriesNumberOfAsset;
        }
        public Assets()
        {

        }

    }
}
