using Unity.Entities;

namespace ECSTut{
    public struct Config : IComponentData{
        public Entity TankPrefab;
        public int TankCount;
        public float SafeZoneRadius;
    }
}