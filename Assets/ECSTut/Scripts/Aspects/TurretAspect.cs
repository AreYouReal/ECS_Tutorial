using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace ECSTut.Scripts.Aspects{
    
    public readonly partial struct TurretAspect : IAspect{
        private readonly RefRO<Turret> MyTurret;
        private readonly RefRO<URPMaterialPropertyBaseColor> MyBaseColor;

        public Entity CannonBallSpawn => MyTurret.ValueRO.CannonBallSpawn;
        public Entity CannonBallPrefab => MyTurret.ValueRO.CannonBallPrefab;
        
        public float4 Color => MyBaseColor.ValueRO.Value;
    }
    
}