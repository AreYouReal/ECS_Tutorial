using ECSTut.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace ECSTut{
    
    [BurstCompile]
    public partial struct CannonBallSystem : ISystem{
        
        [BurstCompile]
        public void OnCreate(ref SystemState state){
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state){
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state){
            var ECBSingletom = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ECB = ECBSingletom.CreateCommandBuffer(state.WorldUnmanaged);
            var CBJob = new CannonBallJob{
                ECB = ECB.AsParallelWriter(),
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            CBJob.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct CannonBallJob : IJobEntity{
        public EntityCommandBuffer.ParallelWriter ECB;
        public float DeltaTime;

        private void Execute([ChunkIndexInQuery] int InChunkIndex, ref CannonBallAspect InBallAspect){
            float3 Gravity = new float3(0.0f, -9.82f, 0.0f);
            float3 InvertY = new float3(1.0f, -1.0f, 1.0f);

            InBallAspect.Position += InBallAspect.Speed * DeltaTime;
            if (InBallAspect.Position.y < 0.0f){
                InBallAspect.Position *= InvertY;
                InBallAspect.Speed *= InvertY * 0.8f;
            }

            InBallAspect.Speed += Gravity * DeltaTime;

            float Speed = math.lengthsq(InBallAspect.Speed);
            if (Speed < 0.1f){
                ECB.DestroyEntity(InChunkIndex, InBallAspect.E);
            }
        }
    }
}