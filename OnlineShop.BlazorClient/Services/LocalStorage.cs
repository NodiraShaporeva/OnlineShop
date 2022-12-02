using System.IO.Compression;
using System.Text;
using Microsoft.JSInterop;

namespace OnlineShop.BlazorClient.Services
{
    public interface ILocalStorage
    {
        public Task RemoveAsync(string key);
        public Task SaveStringAsync(string key, string value);
        public Task<string> GetStringAsync(string key);
    }

    public class LocalStorage : ILocalStorage
    {
        private readonly IJSRuntime _jsruntime;

        public LocalStorage(IJSRuntime jSRuntime)
        {
            _jsruntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
        }

        public async Task RemoveAsync(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            await _jsruntime.InvokeVoidAsync("localStorage.removeItem", key).ConfigureAwait(false);
        }

        public async Task SaveStringAsync(string key, string value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));
            var compressedBytes = await Compressor.CompressBytesAsync(Encoding.UTF8.GetBytes(value));
            await _jsruntime.InvokeVoidAsync(
                    "localStorage.setItem",
                    key,
                    Convert.ToBase64String(compressedBytes)
                    )
                .ConfigureAwait(false);
        }

        public async Task<string> GetStringAsync(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            var str = await _jsruntime.InvokeAsync<string>("localStorage.getItem", key).ConfigureAwait(false);
            if (str == null) return null;
            var bytes = await Compressor.DecompressBytesAsync(Convert.FromBase64String(str));
            return Encoding.UTF8.GetString(bytes);
        }
    }

    internal static class Compressor
    {
        public static async Task<byte[]> CompressBytesAsync(byte[] bytes)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var compressionStream = new GZipStream(outputStream, CompressionLevel.Optimal))
                {
                    await compressionStream.WriteAsync(bytes, 0, bytes.Length);
                }
                return outputStream.ToArray();
            }
        }
        public static async Task<byte[]> DecompressBytesAsync(byte[] bytes)
        {
            using (var inputStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        await compressionStream.CopyToAsync(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }
    }
}