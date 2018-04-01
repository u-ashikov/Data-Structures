namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private const int Damage = 30;

        private const int MinTeamId = 0;

        private const int MaxTeamId = 4;

        private Dictionary<int, Bag<Bunny>> rooms;

        private Dictionary<int, OrderedSet<Bunny>> bunniesByTeam;

        private Dictionary<string, Bunny> bunniesByName;

        public BunnyWarsStructure()
        {
            this.rooms = new Dictionary<int, Bag<Bunny>>();
            this.bunniesByTeam = new Dictionary<int, OrderedSet<Bunny>>();
            this.bunniesByName = new Dictionary<string, Bunny>();
        }

        public int BunnyCount => this.bunniesByTeam.Sum(l => l.Value.Count);

        public int RoomCount => this.rooms.Count;

        public void AddRoom(int roomId)
        {
            if (this.rooms.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            this.rooms.Add(roomId, new Bag<Bunny>());
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (!this.rooms.ContainsKey(roomId) || this.bunniesByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            if (team < MinTeamId || team > MaxTeamId)
            {
                throw new IndexOutOfRangeException();
            }

            var bunny = new Bunny(name, team, roomId);

            this.rooms[roomId].Add(bunny);
            
            if (!this.bunniesByTeam.ContainsKey(team))
            {
                this.bunniesByTeam.Add(team, new OrderedSet<Bunny>());
            }

            this.bunniesByTeam[team].Add(bunny);

            this.bunniesByName.Add(name, bunny);
        }

        public void Remove(int roomId)
        {
            if (!this.rooms.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            var removedBunnies = this.rooms[roomId];

            this.rooms.Remove(roomId);

            foreach (var bunny in removedBunnies)
            {
                this.bunniesByTeam[bunny.Team].Remove(bunny);
                this.bunniesByName.Remove(bunny.Name);
            }
        }

        public void Next(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var bunny = this.bunniesByName[bunnyName];
            var nextRoomIndex = Array.IndexOf(this.rooms.Keys.ToArray(), bunny.RoomId) + 1;

            if (nextRoomIndex >= this.rooms.Keys.Count)
            {
                nextRoomIndex = 0;
            }

            var nextRoomId = this.rooms.Keys.ElementAt(nextRoomIndex);

            this.rooms[bunny.RoomId].Remove(bunny);
            bunny.RoomId = nextRoomId;
            this.rooms[nextRoomId].Add(bunny);
        }

        public void Previous(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var bunny = this.bunniesByName[bunnyName];
            var nextRoomIndex = Array.IndexOf(this.rooms.Keys.ToArray(), bunny.RoomId) -1;

            if (nextRoomIndex < 0)
            {
                nextRoomIndex = this.RoomCount-1;
            }

            var nextRoomId = this.rooms.Keys.ElementAt(nextRoomIndex);

            this.rooms[bunny.RoomId].Remove(bunny);
            bunny.RoomId = nextRoomId;
            this.rooms[nextRoomId].Add(bunny);
        }

        public void Detonate(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var bunny = this.bunniesByName[bunnyName];

            var bunniesInRoom = this.rooms[bunny.RoomId].Where(b=>b.Team != bunny.Team && b.Name != bunny.Name);

            foreach (var b in bunniesInRoom.ToList())
            {
                b.Health -= Damage;

                if (b.Health <= 0)
                {
                    bunny.Score++;
                    this.bunniesByName.Remove(b.Name);
                    this.bunniesByTeam[b.Team].Remove(b);
                    this.rooms[b.RoomId].Remove(b);
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if (!this.bunniesByTeam.ContainsKey(team))
            {
                throw new IndexOutOfRangeException();
            }

            return this.bunniesByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var bunnies = this.bunniesByName.Keys
                .Where(k => k.EndsWith(suffix))
                .OrderBy(b => b.Reverse().ToString())
                .ThenBy(b=>b.Length)
                .ToList();

            foreach (var b in bunnies)
            {
                yield return this.bunniesByName[b];
            }
        }
    }
}
