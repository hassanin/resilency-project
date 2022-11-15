namespace web_api.State
{
    public interface IState
    {
        public Task<T> Get<T>(string key);
        public Task Set<T>(string key, T value);
    }
}
