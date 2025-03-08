using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASC.Tests.TestUtilities
{
    public class FakeSession : ISession
    {
        private Dictionary<string, byte[]> storageSession = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;
        public string Id => Guid.NewGuid().ToString();
        public IEnumerable<string> Keys => storageSession.Keys;

        public void Clear() => storageSession.Clear();

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask; // Giả lập commit session
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask; // Giả lập load session
        }

        public void Remove(string key) => storageSession.Remove(key);

        public void Set(string key, byte[] value)
        {
            if (!storageSession.ContainsKey(key))
                storageSession.Add(key, value);
            else
                storageSession[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            if (storageSession.ContainsKey(key) && storageSession[key] != null)
            {
                value = storageSession[key];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

    }
}