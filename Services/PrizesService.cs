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
using Microsoft.Extensions.Caching.Memory;

namespace ESAPrizes.Services {
    public class PrizesService {

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public PrizesService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache) {
            var siteConfig = new SiteConfig();
            configuration.Bind(siteConfig);

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = siteConfig.TrackerUrl; //TODO move to config
            _cache = memoryCache;
        }

        public async Task<IEnumerable<Prize>> GetPrizes() {
            return await _cache.GetOrCreateAsync<IEnumerable<Prize>>("PrizeService_Prizes", FetchPrizes);    
        }

        private async Task<IEnumerable<Prize>> FetchPrizes(ICacheEntry arg)
        {
            arg.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            arg.SetSlidingExpiration(TimeSpan.FromMinutes(5));

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