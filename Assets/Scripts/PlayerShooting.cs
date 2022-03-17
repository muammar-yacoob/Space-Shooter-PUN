using UnityEngine;
using UnityEngine.InputSystem;
using PanettoneGames;

public class PlayerShooting : ShootingBehaviour
{
    [SerializeField] private InputActionReference fireButton;
    [SerializeField] private bool continousShooting;
    [SerializeField] private GameObjectPool pool;
    private void Awake() => pool.Prewarm();
    private void OnEnable() => fireButton.action.performed += ctx => Fire();
    private void OnDisable() => fireButton.action.performed -= ctx => Fire();

    private void Update()
    {
        if (CanFire)
        {
            timer = 0;
            Fire();
        }
        timer += Time.deltaTime;
    }

    private bool CanFire =>
            fireButton.action.IsPressed() && timer >= fireDelay;
    protected void Fire()
    {
        for (int i = 0; i < firePoints.Count; i++)
        {
            var bullet = pool.Get();
            bullet.transform.position = firePoints[i].position;
            bullet.transform.rotation = firePoints[i].rotation;
        }
        //OnFire?.Invoke(sFX);
    }
}

