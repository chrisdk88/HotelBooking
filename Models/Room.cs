namespace Models
{
    public class Room : Common
    {
        public int roomNum { get; set; }
        public uint typeId { get; set; }
        public RoomType? type { get; set; }
    }
}