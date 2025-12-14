using Unity.Entities;
using UnityEngine;
/// <summary>
/// Authoring component for spawning bricks in the ECS system, holds configuration data
/// </summary>
public class BrickSpawnerAuthoring : MonoBehaviour {
    public GameObject BrickPrefab;
    public int SpawnCount = 12;
    public float SpawnBound = 10f;
    private class Baker : Baker<BrickSpawnerAuthoring> {
        public override void Bake(BrickSpawnerAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnBrickConfig {
                BrickPrefab = GetEntity(authoring.BrickPrefab, TransformUsageFlags.Dynamic),
                SpawnCount = authoring.SpawnCount,
                SpawnBound = authoring.SpawnBound,
            });
        }
    }
}

public struct SpawnBrickConfig : IComponentData {
    public Entity BrickPrefab;
    public int SpawnCount;
    public float SpawnBound;
}