﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace ProSoft.UI.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly JsonSerializer _serializer =new() ;
        private readonly IDistributedCache _cache;
        public JsonStringLocalizer(IDistributedCache cache)
        {
            _cache = cache ;
        }
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name,value);  
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actulValue = this[name];
                return !actulValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actulValue.Value, arguments))
                    : actulValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Localization/Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(fileStream);
            using JsonTextReader reader = new(streamReader);

            while(reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    continue;
                var key = reader.Value as string;
                reader.Read();
                var value = _serializer.Deserialize<string>(reader);
                yield return new LocalizedString(key, value);
            }
        }
        private string GetString (string Key)
        {
            var filePath = $"Localization/Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath =Path.GetFullPath (filePath);

            if (File.Exists(fullFilePath))
            {
                var casheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{Key}";
                var casheValue = _cache.GetString (casheKey);
                if ( !String.IsNullOrEmpty(casheValue))
                    return casheValue;

                string result = GetValueFromJSON(Key, fullFilePath);
                if (!String.IsNullOrEmpty(result))
                    _cache.SetString(casheKey, result);

                    return result;
            }
            return string.Empty;
        }
        private string GetValueFromJSON(string propertyName ,string filePath) 
        { 
            if(string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;

            using FileStream fileStream = new (filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new (fileStream);
            using JsonTextReader reader = new (streamReader);

            while (reader.Read()) 
            {
                if (reader.TokenType ==JsonToken.PropertyName && reader.Value as string== propertyName)
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader);
                }
            }
            return string.Empty;
        }
    }
}
