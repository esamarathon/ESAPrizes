using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using ESAPrizes.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ESAPrizes.Config;

namespace ESAPrizes.Services {
    public class PrizesService {

        private readonly HttpClient _httpClient;

        public PrizesService(IConfiguration configuration, IHttpClientFactory httpClientFactory) {
            var siteConfig = new SiteConfig();
            configuration.Bind(siteConfig);

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = siteConfig.TrackerUrl; //TODO move to config
        }

        public async Task<IEnumerable<Prize>> GetPrizes() {
            var response = await _httpClient.GetAsync("/search/?type=prize&feed=unwon");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            var prizes = await JsonSerializer.DeserializeAsync<IEnumerable<TrackerPrize>>(responseStream);
            return prizes.Select(tp => tp.Fields).OrderByDescending(p => p.MinimumBid);
        }
    }

    class TrackerPrize {
        [JsonPropertyName("pk")]
        public int PrimaryKey { get; set; }

        [JsonPropertyName("fields")]
        public Prize Fields { get; set; }
    }
}