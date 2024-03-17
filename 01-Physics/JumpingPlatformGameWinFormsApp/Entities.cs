namespace JumpingPlatformGame {

	// TODO: Provide your implementation of the following classes using your types from the PhysicsUnitsLib project.
	
	class Entity {
		public virtual Color Color => Color.Black;

        public WorldPoint Location { get; set; }

        public Entity()
        {
        }

        public virtual void Update(Seconds updatePeriod) { }
    }

    /// <summary>
    /// struct WorldPoint save and remember position 
    /// </summary>
    public struct WorldPoint
    {
        public Meters X { get; set; }
        public Meters Y { get; set; }

        public WorldPoint(Meters x, Meters y)
        {
            X = x;
            Y = y;
        }
    }


    /// <summary>
    /// represent allowed movement (move) and its parts
    /// such as speed and bounds (boundaries)
    /// </summary>
    class Movement
    {
        public MeterPerSeconds Speed { get; set; }
        public Meters LowerBound { get; set; }
        public Meters UpperBound { get; set; }

        public Movement()
        {
            Speed = new MeterPerSeconds(0);
            LowerBound = new Meters(double.MinValue);
            UpperBound = new Meters(double.MaxValue);
        }
    }

    //TODO: run
    class MovableEntity : Entity
    {
        public Movement Horizontal { get; private set; }

        public MovableEntity()
        {
            Horizontal = new Movement();
        }

        public override void Update(Seconds updatePeriod)
        {
            //calculate change of hor pos = m/s * s = m
            var horizontalPositionX = Horizontal.Speed.Value * updatePeriod.Value;

            //position update
            Location = new WorldPoint(Location.X + horizontalPositionX, Location.Y);

            //if entity is out off bound => correct new Location
            if (Location.X > Horizontal.UpperBound.Value)
            {
                Location = new WorldPoint(Horizontal.UpperBound, Location.Y);
                Horizontal.Speed = new MeterPerSeconds(-Math.Abs(Horizontal.Speed.Value));
            }
            else if (Location.X < Horizontal.LowerBound.Value)
            {
                Location = new WorldPoint(Horizontal.LowerBound, Location.Y);
                Horizontal.Speed = new MeterPerSeconds(Math.Abs(Horizontal.Speed.Value));
            }
        }
    }

    class MovableJumpingEntity : MovableEntity {
	}

	class Joe : MovableEntity {
		public override string ToString() => "Joe";
		public override Color Color => Color.Blue;
	}

	class Jack : MovableEntity {
		public override string ToString() => "Jack";
		public override Color Color => Color.LightBlue;
	}

	class Jane : MovableJumpingEntity {
		public override string ToString() => "Jane";
		public override Color Color => Color.Red;
	}

	class Jill : MovableJumpingEntity {
		public override string ToString() => "Jill";
		public override Color Color => Color.Pink;
	}
}