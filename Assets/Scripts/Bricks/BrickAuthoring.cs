using Unity.Entities;
using UnityEngine;
/// <summary>
/// Authoring component for Brick entity, holds max hitpoints configuration
/// </summary>
public class BrickAuthoring : MonoBehaviour {
    [SerializeField] private int MaxHitpoints = 3;
    private class Baker : Baker<BrickAuthoring> {
        public override void Bake(BrickAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Brick {
                Hitpoints = Random.Range(1, authoring.MaxHitpoints + 1)
            });
        }
    }
}

public struct Brick : IComponentData {
    public int Hitpoints;
}