namespace ClearReadHub.Documents.Service.IntegrationTests.Common;

public class AppHttpClient(HttpClient httpClient)
{
    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return await httpClient.GetAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
    {
        return await httpClient.PostAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
    {
        return await httpClient.PutAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        return await httpClient.DeleteAsync(requestUri);
    }
}