using Unity.Entities;
using UnityEngine;

public class BrickSpawnerAuthoring : MonoBehaviour {
    public GameObject BrickPrefab;
    public int BrickMaxHitpoints = 3;
    public int SpawnCount = 12;
    public float SpawnBound = 10f;
    private class Baker : Baker<BrickSpawnerAuthoring> {
        public override void Bake(BrickSpawnerAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnBrickConfig {
                BrickPrefab = GetEntity(authoring.BrickPrefab, TransformUsageFlags.Dynamic),
                SpawnCount = authoring.SpawnCount,
                SpawnBound = authoring.SpawnBound,
                BrickHitpoints = authoring.BrickMaxHitpoints
            });
        }
    }
}

public struct SpawnBrickConfig : IComponentData {
    public Entity BrickPrefab;
    public int SpawnCount;
    public float SpawnBound;
    public int BrickHitpoints;
}