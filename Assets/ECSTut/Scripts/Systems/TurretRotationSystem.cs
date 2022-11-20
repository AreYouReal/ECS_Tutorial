using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTut {
    
    [BurstCompile]
    public partial struct TurretRotationSystem : ISystem {
        
        [BurstCompile]
        public void OnCreate(ref SystemState state) {
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state) {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
           quaternion NewRotation = quaternion.RotateY(SystemAPI.Time.DeltaTime * math.PI);
           foreach (var TAspect in SystemAPI.Query<TransformAspect>().WithAll<Turret>()) {
               TAspect.RotateWorld(NewRotation);
           }
        }
    }
}