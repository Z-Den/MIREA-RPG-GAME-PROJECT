namespace Units.Input
{
    public interface IUnitActionController
    {
        protected IUnitInput InputActions { get; set; }

        public void SetInput(IUnitInput input)
        {
            InputActions = input;
        }
    }
}