using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataClasses
{
    [Serializable]
    public class PenSaveDTO
    {
        public const string folderPath = "Saves";
        public const string filePath = "PenSave";

        [JsonProperty] public List<PenDTO> savedPens = new List<PenDTO>();
        [JsonProperty] public string currentSelectedPen;
    }
}