using Unity.Entities;
using Unity.Physics;
using UnityEngine.Events;

public partial struct BrickCollisionSystem : ISystem
{
    public static UnityEvent OnBrickHit = new();
    /// <summary>
    /// Check for collision events between Bricks and Balls. If one occurs, reduce Brick hitpoints or destroy them if hitpoints reach zero.
    /// </summary>
    /// <param name="state"></param>
    public void OnUpdate(ref SystemState state)
    {
        var sim = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation();

        sim.FinalJobHandle.Complete(); 

        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

        foreach (var collisionEvent in sim.CollisionEvents)
        {
            Entity brickEntity;

            if (SystemAPI.HasComponent<Brick>(collisionEvent.EntityA) && SystemAPI.HasComponent<Ball>(collisionEvent.EntityB))
                brickEntity = collisionEvent.EntityA;
            else if (SystemAPI.HasComponent<Brick>(collisionEvent.EntityB) && SystemAPI.HasComponent<Ball>(collisionEvent.EntityA))
                brickEntity = collisionEvent.EntityB;
            else
                continue;

            GameManager.Score++;
            OnBrickHit.Invoke();

            var brick = SystemAPI.GetComponentRW<Brick>(brickEntity);
            if (--brick.ValueRW.Hitpoints <= 0)
                ecb.DestroyEntity(brickEntity);
        }

        ecb.Playback(state.EntityManager);
    }
}