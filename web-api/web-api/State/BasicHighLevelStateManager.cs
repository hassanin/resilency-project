namespace web_api.State
{
    public class BasicHighLevelStateManager : IHighLevelStateManager
    {
        private readonly IState _stateConfig;
        private const string PrimaryEndpointKey = "PrimaryEndpointKey";
        public BasicHighLevelStateManager(IState stateConfig)
        {
            _stateConfig = stateConfig;
        }
        public bool PrimaryEndpointActive
        {
            get
            {
                try
                {
                    var isActive = _stateConfig.Get<bool>(PrimaryEndpointKey).Result;
                    return isActive;
                }
                catch (Exception ex)
                {
                    // Open by default
                    return true;
                }

            }
            set
            {
                _stateConfig.Set(PrimaryEndpointKey, value).Wait();
            }
        }
    }
}
