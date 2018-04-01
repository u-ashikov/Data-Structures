public class Invader : IInvader
{
    public Invader(int damage, int distance)
    {
        this.Damage = damage;
        this.Distance = distance;
    }
    
    public int Damage { get; set; }

    public int Distance { get; set; }

    public int CompareTo(IInvader other)
    {
        int compareResult = this.Distance.CompareTo(other.Distance);

        if (compareResult == 0)
        {
            compareResult = this.Damage.CompareTo(other.Damage);
        }

        return compareResult;
    }
}
