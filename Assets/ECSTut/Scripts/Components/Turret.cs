using Unity.Entities;

namespace ECSTut {
    
    public struct Turret : IComponentData{
        public Entity CannonBallSpawn;
        public Entity CannonBallPrefab;
    }
    
}