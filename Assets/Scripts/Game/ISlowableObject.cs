namespace Game
{
    public interface ISlowableObject
    {
        public void Slow(float slowModifier);
        public void Unslow(float slowModifier);
    }
}