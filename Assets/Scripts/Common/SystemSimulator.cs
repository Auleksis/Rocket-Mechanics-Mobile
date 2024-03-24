using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemSimulator : MonoBehaviour
{
    LayerMask originalLayer;
    LayerMask simulatedLayer;

    [SerializeField] int simulationIterations = 100;

    //KINEMATIC ORIGINAL
    [SerializeField] KinematicSpaceObject[] originalKinematicObjects;
    //KINEMATIC COPY
    private List<KinematicSpaceObject> copiedKinematicObjects;

    //SIMULATED ORIGINAL
    [SerializeField] SimulatedObject[] originalSimulatedObjects;
    //SIMULATED COPY
    private List<SimulatedObject> copiedSimulatedObjects;

    //STATIC ORIGINAL
    [SerializeField] SpaceObject[] originalStaticObjects;
    //STATIC COPY
    private List<SpaceObject> copiedStaticObjects;

    //
    private Scene originalScene;
    private Scene simulatedScene;

    private PhysicsScene2D originalPhysicsScene;
    private PhysicsScene2D simulatedPhysicsScene;

    void Start()
    {
        originalScene = SceneManager.GetActiveScene();
        originalPhysicsScene = originalScene.GetPhysicsScene2D();

        CreateSceneParameters simulatedSceneParams = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        simulatedScene = SceneManager.CreateScene("Simulated Scene", simulatedSceneParams);
        simulatedPhysicsScene = simulatedScene.GetPhysicsScene2D();

        originalLayer = LayerMask.NameToLayer("Default");
        simulatedLayer = LayerMask.NameToLayer("Simulated");

        InitSimulateScene();
    }

    // Update is called once per frame
    void Update()
    {
        Simulate();
    }

    private void FixedUpdate()
    {
        //Simulate();
    }

    private void InitSimulateScene()
    {
        //INIT KINEMATIC
        copiedKinematicObjects = new List<KinematicSpaceObject>();

        foreach (KinematicSpaceObject kinematicObj in originalKinematicObjects)
        {
            kinematicObj.gameObject.layer = originalLayer.value;

            kinematicObj.SetKinematic(true);

            GameObject copiedKenematic = Instantiate(kinematicObj.gameObject, kinematicObj.transform.position, kinematicObj.transform.rotation);

            copiedKenematic.transform.localScale = new Vector3(copiedKenematic.transform.localScale.x * 100, copiedKenematic.transform.localScale.y * 100);

            SceneManager.MoveGameObjectToScene(copiedKenematic, simulatedScene);

            copiedKenematic.layer = simulatedLayer.value;

            copiedKenematic.GetComponent<SpriteRenderer>().enabled = false;

            DestroyPlayerObject destroyPlayerObject = copiedKenematic.GetComponent<DestroyPlayerObject>();

            if (destroyPlayerObject != null)
                destroyPlayerObject.enabled = false;

            SpriteRenderer[] spriteRenderers = copiedKenematic.transform.GetComponentsInChildren<SpriteRenderer>();
            foreach (var s in spriteRenderers)
            {
                s.enabled = false;
            }

            ParticleSystem[] particleSystem = copiedKenematic.transform.GetComponentsInChildren<ParticleSystem>();
            foreach (var p in particleSystem)
            {
                p.Pause();
            }

            KinematicSpaceObject copiedKinematicSpaceObject = copiedKenematic.GetComponent<KinematicSpaceObject>();

            copiedKinematicSpaceObject.SetKinematic(true);

            copiedKinematicObjects.Add(copiedKinematicSpaceObject);
        }

        //INIT SIMULATED
        copiedSimulatedObjects = new List<SimulatedObject>();

        foreach (SimulatedObject simulatedObj in originalSimulatedObjects)
        {
            simulatedObj.physicsLayer = originalLayer;
            simulatedObj.gameObject.layer = originalLayer.value;

            GameObject copiedSimulated = Instantiate(simulatedObj.gameObject, simulatedObj.transform.position, simulatedObj.transform.rotation);

            copiedSimulated.transform.localScale = new Vector3(copiedSimulated.transform.localScale.x * 100, copiedSimulated.transform.localScale.y * 100);

            SceneManager.MoveGameObjectToScene(copiedSimulated, simulatedScene);

            copiedSimulated.layer = simulatedLayer.value;

            copiedSimulated.GetComponent<SpriteRenderer>().enabled = false;

            DestroyPlayerObject destroyPlayerObject = copiedSimulated.GetComponent<DestroyPlayerObject>();

            if (destroyPlayerObject != null)
                destroyPlayerObject.enabled = false;

            SpriteRenderer[] spriteRenderers = copiedSimulated.transform.GetComponentsInChildren<SpriteRenderer>();
            foreach(var s in spriteRenderers)
            {
                s.enabled = false;
            }

            ParticleSystem[] particleSystem = copiedSimulated.transform.GetComponentsInChildren<ParticleSystem>();
            foreach (var p in particleSystem)
            {
                p.Pause();
            }


            SimulatedObject copiedSimulatedObject = copiedSimulated.GetComponent<SimulatedObject>();

            copiedSimulatedObject.isSimulatedOnAnotherScene = true;

            copiedSimulatedObject.physicsLayer = simulatedLayer.value;

            copiedSimulatedObjects.Add(copiedSimulatedObject);
        }

        //INIT STATIC
        copiedStaticObjects = new List<SpaceObject>();

        foreach (SpaceObject spaceObj in originalStaticObjects)
        {
            spaceObj.gameObject.layer = originalLayer.value;

            spaceObj.SetKinematic(true);

            GameObject copiedStatic = Instantiate(spaceObj.gameObject, spaceObj.transform.position, spaceObj.transform.rotation);

            copiedStatic.transform.localScale = new Vector3(copiedStatic.transform.localScale.x * 100, copiedStatic.transform.localScale.y * 100);

            SceneManager.MoveGameObjectToScene(copiedStatic, simulatedScene);

            copiedStatic.layer = simulatedLayer.value;

            copiedStatic.GetComponent<SpriteRenderer>().enabled = false;

            DestroyPlayerObject destroyPlayerObject = copiedStatic.GetComponent<DestroyPlayerObject>();

            if (destroyPlayerObject != null)
                destroyPlayerObject.enabled = false;

            SpriteRenderer[] spriteRenderers = copiedStatic.transform.GetComponentsInChildren<SpriteRenderer>();
            foreach (var s in spriteRenderers)
            {
                s.enabled = false;
            }

            ParticleSystem[] particleSystem = copiedStatic.transform.GetComponentsInChildren<ParticleSystem>();
            foreach (var p in particleSystem)
            {
                p.Pause();
            }

            SpaceObject copiedStaticSpaceObject = copiedStatic.GetComponent<SpaceObject>();

            copiedStaticSpaceObject.SetKinematic(true);

            copiedStaticObjects.Add(copiedStaticSpaceObject);
        }
    }

    private void RestoreOriginalState()
    {
        for(int i = 0; i <  originalKinematicObjects.Length; i++)
        {
            KinematicSpaceObject originalObject = originalKinematicObjects[i];
            KinematicSpaceObject copiedObject = copiedKinematicObjects[i];

            copiedObject.SetsystemAngle(originalObject.GetSystemAngle());
            copiedObject.SetRotation(originalObject.GetRotation());

            /*copiedBody.transform.position = originalBody.transform.position;
            copiedBody.transform.rotation = originalBody.transform.rotation;*/
        }

        for(int i = 0; i < originalSimulatedObjects.Length; i++)
        {
            SimulatedObject originalObject = originalSimulatedObjects[i];
            SimulatedObject copiedObject = copiedSimulatedObjects[i];

            copiedObject.CopyRigidbodyInfo(originalObject);
        }

        for(int i = 0; i < originalStaticObjects.Length; i++)
        {
            SpaceObject originalObject = originalStaticObjects[i];
            SpaceObject copiedObject = copiedStaticObjects[i];

            copiedObject.SetPosition(originalObject.GetPosition());
            copiedObject.SetRotation(originalObject.GetRotation());
        }
    }

    private void Simulate()
    {
        if (simulatedScene.IsValid())
        {
            //SET UP
            SceneManager.SetActiveScene(simulatedScene);
            Physics2D.simulationMode = SimulationMode2D.Script;

            RestoreOriginalState();

            Array.ForEach(originalSimulatedObjects, obj => obj.RefreshTrajectoryLine());

            //SIMULATE
            for (int i = 0; i < simulationIterations; i++)
            {
                copiedKinematicObjects.ForEach(obj => obj.MoveObject());

                copiedSimulatedObjects.ForEach(obj => obj.RecalculateForce());

                simulatedPhysicsScene.Simulate(Time.fixedDeltaTime);

                for (int j = 0; j < originalSimulatedObjects.Length; j++)
                {
                    originalSimulatedObjects[j].AddPositionToTrajectoryLine(copiedSimulatedObjects[j].GetRigidbodyPosition());
                }
            }

            Array.ForEach(originalSimulatedObjects, obj => obj.DrawTrajectoryLine());

            //FINISH SIMULATION
            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
            SceneManager.SetActiveScene(originalScene);
        }
    }
}