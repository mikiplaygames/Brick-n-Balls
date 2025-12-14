using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine.Events;

public partial struct PlayerShootingSystem : ISystem
{
    const float ShootForce = 25f;
    public static UnityEvent OnShoot = new();
    bool CanShoot() => GameManager.GameInProgress && GameManager.BallsLeft > 0 && InputWrapper.control.Player.Shoot.WasPressedThisFrame();
    public void OnUpdate(ref SystemState state)
    {
        if (!CanShoot())
            return;

        SpawnBallsConfig spawnBallsConfig = SystemAPI.GetSingleton<SpawnBallsConfig>();
        EntityCommandBuffer ecb = new(state.WorldUpdateAllocator);

        Entity ballEntity = ecb.Instantiate(spawnBallsConfig.BallPrefab);
        
        LocalTransform spawnTransform = new()
        {
            Position = PlayerCameraControl.Position,
            Rotation = quaternion.identity,
            Scale = 1f
        };
        PhysicsVelocity velocity = new()
        {
            Linear = PlayerCameraControl.Forward * ShootForce,
            Angular = float3.zero
        };

        ecb.SetComponent(ballEntity, spawnTransform);
        ecb.SetComponent(ballEntity, velocity);
        ecb.SetComponent(ballEntity, new Ball { });

        ecb.Playback(state.EntityManager);
        ecb.Dispose();

        GameManager.BallsLeft--;
        OnShoot.Invoke();
    }
}
