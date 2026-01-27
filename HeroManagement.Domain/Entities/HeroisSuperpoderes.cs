namespace HeroManagement.Domain;

public class HeroiSuperpoder
{
    public int HeroiId { get; private set; }
    public int SuperpoderId { get; private set; }

    protected HeroiSuperpoder() { }

    public HeroiSuperpoder(int heroiId, int superpoderId)
    {
        HeroiId = heroiId;
        SuperpoderId = superpoderId;
    }
}
