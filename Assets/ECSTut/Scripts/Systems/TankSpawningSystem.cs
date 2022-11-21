using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;

namespace ECSTut{
    
    [BurstCompile]
    public partial struct TankSpawningSystem : ISystem{
        
        private EntityQuery MyBaseColorQuery;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state){
            state.RequireForUpdate<Config>();

            MyBaseColorQuery = state.GetEntityQuery(ComponentType.ReadOnly<URPMaterialPropertyBaseColor>());
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state){
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state){
            var Config = SystemAPI.GetSingleton<Config>();

            var Random = Unity.Mathematics.Random.CreateFromIndex(1234);
            var Hue = Random.NextFloat();


            URPMaterialPropertyBaseColor RndColor(){
                Hue = (Hue + 0.618034005f) % 1;
                var Color = UnityEngine.Color.HSVToRGB(Hue, 1.0f, 1.0f);
                return(new URPMaterialPropertyBaseColor{Value = (UnityEngine.Vector4)Color});
            }

            var ECBSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ECB = ECBSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            var Vehicles = CollectionHelper.CreateNativeArray<Entity>(Config.TankCount, Allocator.Temp);
            ECB.Instantiate(Config.TankPrefab, Vehicles);

            var QueryMask = MyBaseColorQuery.GetEntityQueryMask();

            foreach (var V in Vehicles){
                ECB.SetComponentForLinkedEntityGroup(V, QueryMask, RndColor());
            }
            
            state.Enabled = false;
        }
        
    }
}