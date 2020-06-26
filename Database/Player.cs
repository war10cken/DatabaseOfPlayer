namespace Database
{
    public class Player
    {
        public int SerialNumber { get; }
        public string NickName { get; }
        public int Level { get; }
        public bool IsBanned { get; private set; }
        
        public Player(int serialNumber, string nickName, int level, bool isBanned)
        {
            SerialNumber = serialNumber;
            NickName = nickName;
            Level = level;
            IsBanned = isBanned;
        }

        public void Ban() => IsBanned = true;
        public void UnBan() => IsBanned = false;
    }
}