using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleProject.Tools.Caching.Policy;

namespace SampleProject.Tools.Caching.Abstractions;

/// <summary>
/// Defines an interface for a cache client which provides methods to interact with a cache.
/// </summary>
public interface ICacheClient
{
    /// <summary>
    /// Checks if a specific cache policy exists.
    /// </summary>
    /// <param name="policyName">Name of the policy to check.</param>
    /// <returns>True if the policy exists, false otherwise.</returns>
    bool PolicyExists(string policyName);
    
    /// <summary>
    /// Adds a new cache policy.
    /// </summary>
    /// <param name="policyName">Name of the new policy.</param>
    /// <param name="itemPolicy">The policy to add.</param>
    void AddPolicy(string policyName, ItemPolicy itemPolicy);

    /// <summary>
    /// Retrieves an item from the cache.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <returns>The cached item if found, default value otherwise.</returns>
    T Get<T>(string key);
    
    /// <summary>
    /// Asynchronously retrieves an item from the cache.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <returns>A task representing the asynchronous operation, with a value that is the cached item if found, default value otherwise.</returns>
    Task<T> GetAsync<T>(string key);
    
    /// <summary>
    /// Adds a new item to the cache if it does not exist, or retrieves it if it does.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="fetch">The function to generate the item.</param>
    /// <param name="policy">The caching policy.</param>
    /// <returns>The newly added or existing cached item.</returns>
    T AddOrGet<T>(string key, Func<T> fetch, ItemPolicy policy);
    
    /// <summary>
    /// Asynchronously adds a new item to the cache if it does not exist, or retrieves it if it does.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="fetch">The function to generate the item.</param>
    /// <param name="policy">The caching policy.</param>
    /// <returns>A task representing the asynchronous operation, with a value that is the newly added or existing cached item.</returns>
    Task<T> AddOrGetAsync<T>(string key, Func<T> fetch, ItemPolicy policy);
    
    /// <summary>
    /// Adds a new item to the cache if it does not exist, or retrieves it if it does. Uses a named policy.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="fetch">The function to generate the item.</param>
    /// <param name="policyName">The name of the caching policy.</param>
    /// <returns>The newly added or existing cached item.</returns>
    T AddOrGet<T>(string key, Func<T> fetch, string policyName);

    /// <summary>
    /// Asynchronously adds a new item to the cache if it does not exist, or retrieves it if it does. Uses a named policy.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="fetch">The function to generate the item.</param>
    /// <param name="policyName">The name of the caching policy.</param>
    /// <returns>A task representing the asynchronous operation, with a value that is the newly added or existing cached item.</returns>
    Task<T> AddOrGetAsync<T>(string key, Func<T> fetch, string policyName);

    /// <summary>
    /// Asynchronously adds a new item to the cache if it does not exist, or retrieves it if it does. Uses a named policy.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="fetch">The function to asynchronously generate the item.</param>
    /// <param name="policyName">The name of the caching policy.</param>
    /// <returns>A task representing the asynchronous operation, with a value that is the newly added or existing cached item.</returns>
    Task<T> AddOrGetAsync<T>(string key, Func<Task<T>> fetch, string policyName);
    
    /// <summary>
    /// Sets a new item in the cache.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="item">The item to cache.</param>
    /// <param name="policy">The caching policy.</param>
    void Set<T>(string key, T item, ItemPolicy policy);

    /// <summary>
    /// Asynchronously sets a new item in the cache.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="item">The item to cache.</param>
    /// <param name="policy">The caching policy.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetAsync<T>(string key, T item, ItemPolicy policy);

    /// <summary>
    /// Sets a new item in the cache using a named policy.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="item">The item to cache.</param>
    /// <param name="policyName">The name of the caching policy.</param>
    void Set<T>(string key, T item, string policyName);
    
    /// <summary>
    /// Asynchronously sets a new item in the cache using a named policy.
    /// </summary>
    /// <param name="key">The cache key of the item.</param>
    /// <param name="value">The item to cache.</param>
    /// <param name="policyName">The name of the caching policy.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetAsync<T>(string key, T value, string policyName);

    /// <summary>
    /// Invalidates a cache item with the given key.
    /// </summary>
    /// <param name="key">The cache key of the item to invalidate.</param>
    void Invalidate(string key);

    /// <summary>
    /// Asynchronously invalidates a cache item with the given key.
    /// </summary>
    /// <param name="key">The cache key of the item to invalidate.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task InvalidateAsync(string key);

    /// <summary>
    /// Invalidates all cache items that start with the given key prefix.
    /// </summary>
    /// <param name="keyPrefix">The key prefix of the items to invalidate.</param>
    void InvalidateAllStartingWith(string keyPrefix);

    /// <summary>
    /// Asynchronously invalidates all cache items that start with the given key prefix.
    /// </summary>
    /// <param name="keyPrefix">The key prefix of the items to invalidate.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task InvalidateAllStartingWithAsync(string keyPrefix);

    /// <summary>
    /// Removes a cache item with the given key.
    /// </summary>
    /// <param name="key">The cache key of the item to remove.</param>
    void Remove(string key);

    /// <summary>
    /// Asynchronously removes a cache item with the given key.
    /// </summary>
    /// <param name="key">The cache key of the item to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveAsync(string key);

    /// <summary>
    /// Removes all keys that start with the specified prefix.
    /// </summary>
    /// <param name="keyPrefix">The cache key prefix.</param>
    /// <returns>Returns a list of keys which have been deleted.</returns>
    IEnumerable<string> RemoveAllStartingWith(string keyPrefix);

    /// <summary>
    /// Asynchronously removes all keys that start with the specified prefix.
    /// </summary>
    /// <param name="keyPrefix">The cache key prefix.</param>
    /// <returns>A task representing the asynchronous operation, returning a list of keys which have been deleted.</returns>
    Task<IEnumerable<string>> RemoveAllStartingWithAsync(string keyPrefix);

    /// <summary>
    /// Checks if the cache is in a working state.
    /// </summary>
    /// <returns>True if the cache is in a working state, false otherwise.</returns>
    bool IsInWorkingState();
}