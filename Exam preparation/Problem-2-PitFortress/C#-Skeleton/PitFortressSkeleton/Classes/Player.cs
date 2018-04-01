namespace Classes
{
    using Interfaces;
    using System;

    public class Player : IPlayer
    {
        private int radius;

        public Player(string name,int radius)
        {
            this.Radius = radius;
            this.Name = name;
        }

        public string Name { get; private set; }

        public int Radius
        {
            get => this.radius;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                this.radius = value;
            }
        }

        public int Score { get; set; }

        public int CompareTo(Player other)
        {
            int cmp = other.Score.CompareTo(this.Score);

            if (cmp == 0)
            {
                cmp = other.Name.CompareTo(this.Name);
            }

            return cmp;
        }
    }
}
