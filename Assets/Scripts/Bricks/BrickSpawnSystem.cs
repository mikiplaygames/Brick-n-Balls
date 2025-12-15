using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
/// <summary>
/// System responsible for spawning bricks at the start of the game
/// </summary>
public partial struct BrickSpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnBrickConfig>();
    }
    public void OnUpdate(ref SystemState state)
    {
        if (!GameManager.ShouldSpawnBalls)
            return;

        GameManager.ShouldSpawnBalls = false;

        SpawnBrickConfig spawnBrickConfig = SystemAPI.GetSingleton<SpawnBrickConfig>();
        EntityCommandBuffer ecb = new(state.WorldUpdateAllocator);

        // Spawn new bricks
        for (int i = 0; i < spawnBrickConfig.SpawnCount; i++)
        {
            Entity brickEntity = ecb.Instantiate(spawnBrickConfig.BrickPrefab);

            LocalTransform spawnTransform = new()
            {
                Position = new float3(UnityEngine.Random.Range(-spawnBrickConfig.SpawnBound, spawnBrickConfig.SpawnBound), UnityEngine.Random.Range(-spawnBrickConfig.SpawnBound, spawnBrickConfig.SpawnBound), 0), 
                Rotation = quaternion.identity,
                Scale = 1f
            };

            ecb.SetComponent(brickEntity, spawnTransform);
        }

        ecb.Playback(state.EntityManager);
    }
}