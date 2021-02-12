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
        private readonly bool useObjectCache;

        public PrizesService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache) {
            var siteConfig = new SiteConfig();
            configuration.Bind(siteConfig);

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = siteConfig.TrackerUrl;
            _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache" );
            _cache = memoryCache;
            useObjectCache = siteConfig.UseCache;
        }

        public async Task<IEnumerable<Prize>> GetPrizes() {
            if (!useObjectCache) {
                return await FetchPrizes();
            }
            return await _cache.GetOrCreateAsync<IEnumerable<Prize>>("PrizeService_Prizes", FetchPrizes);   
        }

        private async Task<IEnumerable<Prize>> FetchPrizes()
        {
            var response = await _httpClient.GetAsync("/search/?type=prize&feed=current");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            var prizes = await JsonSerializer.DeserializeAsync<IEnumerable<TrackerPrize>>(responseStream);
            return prizes.Select(tp => tp.Fields).OrderByDescending(p => p.MinimumBid);
        }

        private async Task<IEnumerable<Prize>> FetchPrizes(ICacheEntry arg)
        {
            arg.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            arg.SetSlidingExpiration(TimeSpan.FromSeconds(30));

            return await FetchPrizes();
        }
    }

    class TrackerPrize {
        [JsonPropertyName("pk")]
        public int PrimaryKey { get; set; }

        [JsonPropertyName("fields")]
        public Prize Fields { get; set; }
    }
}