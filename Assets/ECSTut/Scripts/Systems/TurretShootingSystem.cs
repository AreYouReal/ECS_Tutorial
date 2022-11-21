using ECSTut.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;

namespace ECSTut{
    
    [BurstCompile]
    public partial struct TurretShootingSystem : ISystem{
        
        private ComponentLookup<LocalToWorldTransform> MyLocalToWorldTransformFromEntity;

        [BurstCompile]
        public void OnCreate(ref SystemState state){
            MyLocalToWorldTransformFromEntity = state.GetComponentLookup<LocalToWorldTransform>(true);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state){
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state){
            MyLocalToWorldTransformFromEntity.Update(ref state);

            var ECBSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ECB = ECBSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var TSJ = new TurretShootJob(){
                LocalToWorldTransformFromEntity = MyLocalToWorldTransformFromEntity,
                ECB = ECB
            };

            TSJ.Schedule();
        }
        
    }

    [WithAll(typeof(Shooting))]
    [BurstCompile]
    public partial struct TurretShootJob : IJobEntity{

        [Unity.Collections.ReadOnly] public ComponentLookup<LocalToWorldTransform> LocalToWorldTransformFromEntity;
        public EntityCommandBuffer ECB;


        void Execute(in TurretAspect InTurret){
            var Instance = ECB.Instantiate(InTurret.CannonBallPrefab);
            var SpawnLocalToWorld = LocalToWorldTransformFromEntity[InTurret.CannonBallSpawn];
            var CannonBallTransform = UniformScaleTransform.FromPosition(SpawnLocalToWorld.Value.Position);

            CannonBallTransform.Scale = LocalToWorldTransformFromEntity[InTurret.CannonBallPrefab].Value.Scale;
            
            ECB.SetComponent(Instance, new LocalToWorldTransform{
                Value = CannonBallTransform
            });
            ECB.SetComponent(Instance, new CannonBall{
                Speed = SpawnLocalToWorld.Value.Forward() * 20.0f
            });
            
            ECB.SetComponent(Instance, new URPMaterialPropertyBaseColor{ Value = InTurret.Color });
        }


    }

}