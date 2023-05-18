using Newtonsoft.Json;

namespace LeavePlanner.Models.DTOs
{
    public class LeaveDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "start")]
        public string Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public string End { get; set; }

        [JsonProperty(PropertyName = "backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty(PropertyName = "workingDays")]
        public string WorkingDays { get; set; }

        public bool AllDay => true;
    }
}
