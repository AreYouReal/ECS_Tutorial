using Unity.Entities;
using Unity.Mathematics;

namespace ECSTut{
    public struct CannonBall : IComponentData{
        public float3 Speed;
    }
}