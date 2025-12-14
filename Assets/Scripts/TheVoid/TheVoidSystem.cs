using Unity.Entities;
using Unity.Physics;
/// <summary>
/// Handles the behavior of The Void, destroying balls that enter it and ending the game if no balls are left.
/// </summary>
public partial struct TheVoidSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }
    /// <summary>
    /// Destroys balls that enter The Void and ends the game if no balls are left.
    /// </summary>
    /// <param name="state"></param>
    public void OnUpdate(ref SystemState state)
    {
        var sim = SystemAPI.GetSingleton<SimulationSingleton>().AsSimulation();
        sim.FinalJobHandle.Complete();

        EntityCommandBuffer ecb = new(state.WorldUpdateAllocator);
        // Check trigger events
        foreach (var triggerEvent in sim.TriggerEvents)
        {
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;

            // Check if entityA is a Ball
            if (SystemAPI.HasComponent<Ball>(entityA) && SystemAPI.HasComponent<TheVoid>(entityB))
                ecb.DestroyEntity(entityA);
            else if (SystemAPI.HasComponent<Ball>(entityB) && SystemAPI.HasComponent<TheVoid>(entityA))
                ecb.DestroyEntity(entityB);
            else
                continue;
            if (GameManager.BallsLeft <= 0 && SystemAPI.QueryBuilder().WithAll<Ball>().Build().CalculateEntityCount() <= 1)
                GameManager.EndGame();
        }
        ecb.Playback(state.EntityManager);
    }
}