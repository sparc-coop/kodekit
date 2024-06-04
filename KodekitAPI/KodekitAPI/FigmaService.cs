using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace KodekitAPI
{
    public class FigmaService
    {
        private readonly HttpClient _httpClient;
        private readonly string clientId;
        private readonly string redirectUri;
        private readonly string scope;
        private readonly string responseType = "code";

        public FigmaService(HttpClient httpClient, string clientId, string redirectUri, string scope)
        {
            _httpClient = httpClient;
            this.clientId = clientId;
            this.redirectUri = redirectUri;
            this.scope = scope;
        }

        public string GetAuthenticationUrl(string state)
        {
            return $"https://www.figma.com/oauth?client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope={Uri.EscapeDataString(scope)}&state={state}&response_type={responseType}";
        }

        public async Task<string> GetFigmaFileDetails(string fileId, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"https://api.figma.com/v1/files/{fileId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> ExchangeAuthorizationCodeForToken(string code)
        {
            var requestBody = new Dictionary<string, string>
            {
                {"client_id", clientId},
                {"client_secret", "Ueq1mgoRmrELfgZLniMAWtb2Z57Zk1"}, // Keep this secure
                {"redirect_uri", redirectUri},
                {"code", code},
                {"grant_type", "authorization_code"}
            };

            var response = await _httpClient.PostAsync("https://www.figma.com/api/oauth/token", new FormUrlEncodedContent(requestBody));
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);

            return tokenResponse?.AccessToken;
        }
        public async Task<List<Team>> GetTeams(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://api.figma.com/v1/me/teams");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var teamsResponse = JsonSerializer.Deserialize<TeamsResponse>(json);
            return teamsResponse?.Teams;
        }
        public async Task<List<Project>> GetProjectsInTeam(string teamId, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"https://api.figma.com/v1/teams/{teamId}/projects");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var projectsResponse = JsonSerializer.Deserialize<ProjectsResponse>(json);
            return projectsResponse?.Projects;
        }
        public async Task<List<FigmaFile>> GetFilesInProject(string projectId, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"https://api.figma.com/v1/projects/{projectId}/files");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var filesResponse = JsonSerializer.Deserialize<FigmaFilesResponse>(json);
            return filesResponse?.Files;
        }

        public class FigmaFilesResponse
        {
            [JsonPropertyName("files")]
            public List<FigmaFile> Files { get; set; }
        }

        public class FigmaFile
        {
            [JsonPropertyName("key")]
            public string Key { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("thumbnail_url")]
            public string ThumbnailUrl { get; set; }
        }
        public class TeamsResponse
        {
            [JsonPropertyName("teams")]
            public List<Team> Teams { get; set; }
        }

        public class Team
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
        public class ProjectsResponse
        {
            [JsonPropertyName("projects")]
            public List<Project> Projects { get; set; }
        }

        public class Project
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
        private class TokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }

            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonPropertyName("refresh_token")]
            public string RefreshToken { get; set; }
        }
    }

}
