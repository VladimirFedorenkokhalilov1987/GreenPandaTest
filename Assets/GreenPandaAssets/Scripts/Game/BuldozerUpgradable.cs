namespace GreenPandaAssets.Scripts
{
    public class BuldozerUpgradable : AUpgradable
    {
        public override void Upgrade()
        {
            base.Upgrade();
            TruckControl.Instance.TimeLoadTruck -= 0.1f;
        }
    }
}