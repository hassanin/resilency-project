using System.Collections.Concurrent;

namespace web_api.State
{
    public class DictionaryState : IState
    {
        private IDictionary<string, object> _dictionary;
        public DictionaryState()
        {
            _dictionary = new ConcurrentDictionary<string, dynamic>();
        }

        public Task<T> Get<T>(string key)
        {
            Console.WriteLine("I am in get");
            return Task.FromResult((T)_dictionary[key]);

        }

        public Task Set<T>(string key, T value)
        {
            Console.WriteLine($"I have set value to {value}");
            _dictionary[key] = value;
            return Task.CompletedTask;
        }
    }
}
