using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] float engineForce = 1.0f;
    [SerializeField] float angularVelocity = 20.0f;

    [SerializeField] UIManager uiManager;

    [SerializeField] ParticleSystem particleSystem;


    //[SerializeField] LineRenderer velocityLine;

    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    //private LineRenderer lineRenderer;

    private bool addForce = false;

    private SimulatedObject simulatedObject;

    public bool isEngineAvailable = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        simulatedObject = GetComponent<SimulatedObject>();
        //lineRenderer = GetComponent<LineRenderer>();

        //lineRenderer.positionCount = 2;
        //velocityLine.positionCount = 2;
    }

    private void Update()
    {
        

    }

    private void FixedUpdate()
    {
        if (addForce)
        {
            UseRocketEngine();
        }
        else
        {
            var emission = particleSystem.emission;
            emission.enabled = false;
        }

        Vector2 forceVector = rigidbody.totalForce.normalized * Mathf.Clamp(rigidbody.totalForce.magnitude, -10, 10);

        //lineRenderer.SetPosition(0, transform.position);
        //lineRenderer.SetPosition(1, (Vector2)transform.position + forceVector);

        Vector2 velocity = rigidbody.velocity.normalized * Mathf.Clamp(rigidbody.velocity.magnitude, -10, 10);

        if (!simulatedObject.isSimulatedOnAnotherScene)
        {
            uiManager.SetVelocityInfo("Скорость: " + rigidbody.velocity.ToString());
            uiManager.SetAllVelocityInfo("Общая Скорость: " + rigidbody.velocity.magnitude.ToString());
        }

        //velocityLine.SetPosition(0, transform.position);
        //velocityLine.SetPosition(1, (Vector2)transform.position + velocity);

        //Debug.Log(rigidbody.totalForce);
    }

    public void UseRocketEngine()
    {
        Vector3 direction = transform.rotation * Vector3.up;

        rigidbody.AddForce(direction * engineForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

        var emission = particleSystem.emission;
        emission.enabled = true;
    }

    public void Turn(Vector3 input)
    {
        if (input.sqrMagnitude == 0)
            return;

        Quaternion toRotation = Quaternion.LookRotation(transform.forward, input);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, angularVelocity * Time.deltaTime);

        //transform.Rotate(Vector3.back, angularVelocity * input.x * Time.fixedDeltaTime);
    }

    public void HandleInput(Vector3 input, bool force)
    {
        addForce = force && isEngineAvailable;


        if (addForce)
        {
            uiManager.SetGasInfo("Двигатель:" + " Включён");

            uiManager.SetGasInfoColor(Color.green);
        }
        else
        {
            uiManager.SetGasInfo("Двигатель:" + " Выключен");

            uiManager.SetGasInfoColor(Color.red);
        }

        Turn(input);
    }
}
