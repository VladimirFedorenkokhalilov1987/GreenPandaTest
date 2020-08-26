namespace GreenPandaAssets.Scripts
{
    public class TruckUpgradable : AUpgradable
    {
        public override void Upgrade()
        {
            base.Upgrade();
            TruckControl.Instance.MoveSpeed -= 0.7f;
        }
    }
}