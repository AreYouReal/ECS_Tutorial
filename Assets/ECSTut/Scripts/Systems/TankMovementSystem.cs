using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTut {
    
    partial class TankMovementSystem : SystemBase {
        
        protected override void OnUpdate()
        {
            float DeltaTime = SystemAPI.Time.DeltaTime;

            Entities.WithAll<Tank>().ForEach((TransformAspect InTransformAspect) =>
            {
                float3 Position = InTransformAspect.Position;
                float Angle = (0.5f + noise.cnoise(Position / 10f)) * 4.0f * math.PI;
                
                float3 Direction = float3.zero;
                math.sincos(Angle, out Direction.x, out Direction.z);
                InTransformAspect.Position += Direction * DeltaTime * 5.0f;
                InTransformAspect.Rotation = quaternion.RotateY(Angle);
            }).ScheduleParallel();
        }
        
    }
    
}