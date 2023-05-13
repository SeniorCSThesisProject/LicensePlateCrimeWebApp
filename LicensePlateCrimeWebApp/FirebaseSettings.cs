using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LicensePlateCrimeWebApp
{
	public class FirebaseSettings
	{
		public string WebApiKey { get; set; } = "";

		[JsonPropertyName("project_id")]
		public string ProjectId { get; set; } = "";
		public string ServiceAccountJsonFileName { get; set; } = "";

		// get content from ServiceAccountJsonPath and serialize into json string
		public string ServiceAccountJsonPath
		{
			get => Path.GetFullPath(ServiceAccountJsonFileName);
		}
		public string ServiceAccountJson
		{
			get => File.ReadAllText(ServiceAccountJsonPath);
		}

		public string StorageBucketName { get; set; } = "";
    }
}
