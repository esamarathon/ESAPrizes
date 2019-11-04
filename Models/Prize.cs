using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ESAPrizes.Utils;

namespace ESAPrizes.Models {
    public class Prize {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [UIHint("Image")]
        [JsonPropertyName("image")]
        public Uri Image { get; set; }

        [JsonConverter(typeof(StringDecimalConverter))]
        [JsonPropertyName("estimatedvalue")]
        public decimal? EstimatedValue { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonConverter(typeof(StringDecimalConverter))]
        [JsonPropertyName("minimumbid")]
        public decimal? MinimumBid { get; set; }

        [JsonPropertyName("category__name")]
        public string Category { get; set; }

        public Prize() {
            Name = "Unnamed prize";
            //EstimatedValue = "0";
            Provider = "Anonymous";
        }
    }
}