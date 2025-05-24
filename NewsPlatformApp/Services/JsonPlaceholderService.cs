using System.Net.Http.Json;
using NewsPlatformApp.Models;

namespace NewsPlatformApp.Services
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostsAsync()
            => await _httpClient.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts") ?? new();

        public async Task<Post?> GetPostAsync(int postId)
            => await _httpClient.GetFromJsonAsync<Post>($"https://jsonplaceholder.typicode.com/posts/{postId}");

        public async Task<User?> GetUserAsync(int userId)
            => await _httpClient.GetFromJsonAsync<User>($"https://jsonplaceholder.typicode.com/users/{userId}");

        public async Task<List<Comment>> GetCommentsAsync(int postId)
            => await _httpClient.GetFromJsonAsync<List<Comment>>($"https://jsonplaceholder.typicode.com/comments?postId={postId}") ?? new();
    }
}
