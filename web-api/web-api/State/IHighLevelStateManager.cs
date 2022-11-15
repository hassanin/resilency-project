namespace web_api.State
{
    public interface IHighLevelStateManager
    {
        public bool PrimaryEndpointActive { get; set; }
        public void ToggleEndpoint()
        {
            if (PrimaryEndpointActive)
            {
                PrimaryEndpointActive = false;
            }
            else
            {
                PrimaryEndpointActive = true;
            }
        }
    }
}
