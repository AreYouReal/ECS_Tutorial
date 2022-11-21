using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECSTut{
    
    [BurstCompile]
    public partial struct SafeZoneSystem : ISystem{
        
        private ComponentLookup<Shooting> MyTurretActiveFromEntity;

        [BurstCompile]
        public void OnCreate(ref SystemState state){
            state.RequireForUpdate<Config>();
            MyTurretActiveFromEntity = state.GetComponentLookup<Shooting>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state){
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state){
            float Radius = SystemAPI.GetSingleton<Config>().SafeZoneRadius;
            const float DebugRenderStepInDegrees = 20.0f;

            for (float angle = 0; angle < 360; angle += DebugRenderStepInDegrees){
                var A = float3.zero;
                var B = float3.zero;
                math.sincos(math.radians(angle), out A.x, out A.z);
                math.sincos(math.radians(angle + DebugRenderStepInDegrees), out B.x, out B.z);
                UnityEngine.Debug.DrawLine(A * Radius, B * Radius);
            }
            
            MyTurretActiveFromEntity.Update(ref state);
            var SZJob = new SafeZoneJob{
                TurretActiveFromEntity = MyTurretActiveFromEntity,
                SquareRadius = Radius * Radius
            };
            SZJob.ScheduleParallel();
        }
    }

    [WithAll(typeof(Turret))]
    [BurstCompile]
    partial struct SafeZoneJob : IJobEntity{
        
        [NativeDisableParallelForRestriction]
        public ComponentLookup<Shooting> TurretActiveFromEntity;

        public float SquareRadius;

        private void Execute(Entity InEntity, TransformAspect InTransform){
            bool bEnableComponent = math.lengthsq(InTransform.Position) > SquareRadius;
            TurretActiveFromEntity.SetComponentEnabled(InEntity, bEnableComponent);
        }

    }

}