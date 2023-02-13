//using Newtonsoft.Json;
//using SoftwareDesignAndArchitecture.API.Entity;
//using System.Collections;

//namespace SoftwareDesignAndArchitecture.API.Helper;

//public class DataAccess
//{
//    public IEnumerable<Assets> GetDataFromFile(string fileName)
//    {
//        var filePath=Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
//        List<string> KIPLMachineData=new List<string>();
//       // List<Assets> KIPLAllMachinesData=new List<Assets>();
//        if (File.Exists(filePath) && filePath.EndsWith(".txt"))
//        {
//            var rows=File.ReadAllLines(filePath).ToList();
//            var listOfMachineData=new List<Assets>();
//            foreach (string row in rows)
//            {
//                KIPLMachineData = row.Split(",").ToList();
//                if (KIPLMachineData != null && KIPLMachineData.Any())
//                {
//                    listOfMachineData.Add(new Assets
//                    {
//                        MachineName = KIPLMachineData[0].Trim(),
//                        AssetName = KIPLMachineData[1].Trim(),
//                        SeriesNumberOfAsset = KIPLMachineData[2].Trim()
//                    }).ToList();
//                }

//            }  
//            return listOfMachineData;
//        }
//        //else if (File.Exists(filePath) && filePath.EndsWith(".json"))
//        //{
//        //    string json = File.ReadAllText(filePath);
//        //    KIPLAllMachinesData = JsonConvert.DeserializeObject<List<Assets>>(json);
//        //    return KIPLAllMachinesData;
//        //}
//        else
//        {
//            Console.WriteLine("File Not Found");
//            return null;
//        }

//    }
//}
