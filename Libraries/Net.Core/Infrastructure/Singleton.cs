namespace Net.Core.Infrastructure
{
    public class Singleton<T>
    {
        public static T Instance { get; set; }
    }
}
