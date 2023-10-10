using Models;

namespace Client.Containers
{
    public class BookingStateContainer
    {
        public Booking Value { get; set; }
        public uint Price { get; set; }
        public event Action OnStateChange;

        public void SetValue(Booking value)
        {
            Value = value;
            NotifyStateChanged();
        }

        public void SetPrice(uint value)
        {
            Price = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}