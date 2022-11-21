using ECSTut.Scripts.MonoBehaviours;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Windows;

namespace ECSTut{
    
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial class CameraSystem : SystemBase{

        private Entity Target;
        private Random Rnd;
        private EntityQuery TanksQuery;


        protected override void OnCreate(){
            Rnd = Random.CreateFromIndex(1234);
            TanksQuery = GetEntityQuery(typeof(Tank));
            RequireForUpdate(TanksQuery);
        }

        protected override void OnUpdate(){
            if (Target == Entity.Null || UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space)){
                var Tanks = TanksQuery.ToEntityArray(Allocator.Temp);
                Target = Tanks[Rnd.NextInt(Tanks.Length)];
            }

            var CameraTransform = CameraSingleton.I.transform;
            var TankTransform = GetComponent<LocalToWorld>(Target);
            CameraTransform.position =
                TankTransform.Position - 10.0f * TankTransform.Forward + new float3(0.0f, 5.0f, 0.0f);
            CameraTransform.LookAt(TankTransform.Position, new float3(0.0f, 1.0f, 0.0f));
        }
        
    }
}