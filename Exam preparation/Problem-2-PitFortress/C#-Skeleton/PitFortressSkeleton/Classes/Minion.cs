namespace Classes
{
    using Interfaces;
    using System;

    public class Minion : IMinion
    {
        private const int MaxXCoordinate = 1000000;
        private const int MinXCoordinate = 0;
        private const int InitialHealth = 100;

        private int xCoordinate;

        public Minion(int xCoordinate, int id)
        {
            this.XCoordinate = xCoordinate;
            this.Health = InitialHealth;
            this.Id = id;
        }

        public int Id { get; private set; }

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

        public int Health { get; set; }

        public int CompareTo(Minion other)
        {
            int cmp = this.XCoordinate.CompareTo(other.XCoordinate);

            if (cmp == 0)
            {
                cmp = this.Id.CompareTo(other.Id);
            }

            return cmp;
        }
    }
}
