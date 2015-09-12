﻿using System;

namespace Eco.DevView.DummyServer
{
    class Animal : IAnimal, IUpdateable
    {
        public int Id { get; }

        private float _health = 1f;

        public float Health
        {
            get { return _health; }
            set
            {
                if (value < 0 || value > 1f)
                    throw new ArgumentOutOfRangeException();

                _health = value;
            }
        }


        public string Name { get; set; }

        public float X { get; set; }
        public float Z { get; set; }

        public Animal(int id, string name, float x, float z)
        {
            Id = id;
            Name = name;
            X = x;
            Z = z;
        }

        //private void MarkChanged()
        //{
        //    AnimalProvider.Instance.MarkChanged(this);
        //}

        public override string ToString() => $"#{Id} {Name} ({Health * 100:0.00}%)";

        public UpdateOutcome Update(System.Random random)
        {
            if (random.NextDouble() > 0.5)
            {
                Health = Math.Max(0f, Math.Min(Health + (float)(random.NextDouble() - 0.5) / 10f, 1f)); // chance of varying up to 10% of our health

                // Dead?
                if (Health <= 0f)
                    return UpdateOutcome.Removed;
 
                X += (float)(random.NextDouble() - 0.5) * 2f;
                Z += (float)(random.NextDouble() - 0.5) * 2f;
                return UpdateOutcome.Changed;
            }

            return UpdateOutcome.None;
        }
    }
}
