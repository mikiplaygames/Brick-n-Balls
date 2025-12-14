using Unity.Entities;
using UnityEngine;
/// <summary>
/// Simple ball spawner authoring component, holds reference to ball prefab
/// </summary>
public class BallSpawnerAuthoring : MonoBehaviour {
    public GameObject BallPrefab;
    private class Baker : Baker<BallSpawnerAuthoring> {
        public override void Bake(BallSpawnerAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnBallsConfig {
                BallPrefab = GetEntity(authoring.BallPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct SpawnBallsConfig : IComponentData {
    public Entity BallPrefab;
}