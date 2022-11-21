using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTut.Scripts.Aspects{
    public readonly partial struct CannonBallAspect : IAspect{
        public readonly Entity E;

        public readonly TransformAspect Transform;

        public readonly RefRW<CannonBall> Ball;

        public float3 Position{
            get => Transform.Position;
            set => Transform.Position = value;
        }

        public float3 Speed{
            get => Ball.ValueRO.Speed;
            set => Ball.ValueRW.Speed = value;
        }

    }
}