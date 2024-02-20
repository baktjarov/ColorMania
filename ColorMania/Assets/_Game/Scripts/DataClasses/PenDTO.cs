using Newtonsoft.Json;

namespace DataClasses
{
    public class PenDTO
    {
        [JsonProperty] public string penID;
        [JsonProperty] public bool isUnlocked;

        public PenDTO(string ID)
        {
            penID = ID;
        }
    }
}