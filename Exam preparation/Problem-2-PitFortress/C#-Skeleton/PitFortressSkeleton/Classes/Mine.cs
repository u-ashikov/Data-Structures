namespace Classes
{
    using Interfaces;
    using System;

    public class Mine : IMine
    {
        private const int MinXCoordinate = 0;
        private const int MaxXCoordinate = 1000000;
        private const int MinDamage = 0;
        private const int MaxDamage = 100;

        private int delay;

        private int damage;

        private int xCoordinate;

        public Mine(int id, int delay, int damage, int xCoordinate, Player player)
        {
            this.Id = id;
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = xCoordinate;
            this.Player = player;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage
        {
            get => this.damage;
            private set
            {
                if (value < MinDamage || value > MaxDamage)
                {
                    throw new ArgumentException();
                }

                this.damage = value;
            }
        }

        public int XCoordinate
        {
            get => this.xCoordinate;
            private set
            {
                if (value < MinXCoordinate || value > MaxXCoordinate)
                {
                    throw new ArgumentException();
                }

                this.xCoordinate = value;
            }
        }

        public Player Player { get; private set; }

        public int CompareTo(Mine other)
        {
            int cmp = this.Delay.CompareTo(other.Delay);

            if (cmp == 0)
            {
                cmp = this.Id.CompareTo(other.Id);
            }

            return cmp;
        }
    }
}
