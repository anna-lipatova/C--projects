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

    //TODO: inherit run + implement jump
    class MovableJumpingEntity : MovableEntity
    {
        public Movement Vertical { get; private set; }
        private bool isJumping = false;

        public MovableJumpingEntity() : base()
        {
            Vertical = new Movement();
        }

        public override void Update(Seconds updatePeriod)
        {
            //update for horizontal movement
            base.Update(updatePeriod);

            //initually if vertical speed is negative the entity must run.
            //as speed has already been changed to positive value <= JumpButton was clicked
            //entity is not still jumping (as it is running)
            if (!isJumping && Vertical.Speed.Value > 0)
            {
                //it must start to jump
                isJumping = true;
            }

            if (isJumping)
            {
                //calculate change of hor pos = m/s * s = m
                var verticalPositionY = Vertical.Speed.Value * updatePeriod.Value;
                //position update
                Location = new WorldPoint(Location.X, Location.Y + verticalPositionY);

                //if entity is out off bound => correct new Location
                if (Location.Y > Vertical.UpperBound.Value)
                {
                    Vertical.Speed = new MeterPerSeconds(-Math.Abs(Vertical.Speed.Value));
                }
                else if (Location.Y < Vertical.LowerBound.Value)
                {
                    Location = new WorldPoint(Location.X, Vertical.LowerBound);
                    Vertical.Speed = new MeterPerSeconds(-Math.Abs(Vertical.Speed.Value)); //baseground
                }
            }
        }

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