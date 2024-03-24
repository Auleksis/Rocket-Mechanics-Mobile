using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField] int simulationIterations = 100;

    //The parent of all simulated objects in the scene
    [SerializeField] Transform simulatedObjectsParent;
    [SerializeField] GameObject[] kinematicSpaceObjects;
    [SerializeField] GameObject[] staticSpaceObjects;

    [SerializeField] GameObject triangle;

    private Scene currentScene;
    private Scene simulatedScene;

    private PhysicsScene2D currentPhysicsScene;
    private PhysicsScene2D simulatedPhysicsScene;

    private List<TrajectoryRenderer> simulatedObjectsOriginalScene;
    private List<Rigidbody2D> simulatedOriginalRigidbodies;
    private List<TrajectoryRenderer> simulatedObjectCopiedScene;
    private List<Rigidbody2D> simulatedCopiedRigidbodies;


    private List<KinematicSpaceObject> kinematicObjectsCopied;
    private List<Rigidbody2D> kinematicOriginalRigidbodies;
    private List<Rigidbody2D> kinematicCopiedRigidbodies;

    private List<GameObject> staticObjectsCopied;


    private float timer = 0f;

    void Start()
    {
        //Physics.autoSimulation is obsolete!

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene2D();

        CreateSceneParameters simulatedSceneParams = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        simulatedScene = SceneManager.CreateScene("Simulated scene", simulatedSceneParams);
        simulatedPhysicsScene = simulatedScene.GetPhysicsScene2D();

        CopyObjects();
    }

    void Update()
    {
        Simulate();
    }

    private void CopyObjects()
    {
        simulatedObjectsOriginalScene = new List<TrajectoryRenderer>();

        simulatedOriginalRigidbodies = new List<Rigidbody2D>();

        for (int i = 0; i < simulatedObjectsParent.childCount; i++)
        {
            TrajectoryRenderer simulatedObject = simulatedObjectsParent.GetChild(i).GetComponent<TrajectoryRenderer>();

            if(simulatedObject != null)
            {
                simulatedObjectsOriginalScene.Add(simulatedObject);
                simulatedOriginalRigidbodies.Add(simulatedObject.gameObject.GetComponent<Rigidbody2D>());
            }
        }


        //COPYING


        //Движущиеся объекты со статичным правилом движения (без сил)

        kinematicObjectsCopied = new List<KinematicSpaceObject>();
        kinematicCopiedRigidbodies = new List<Rigidbody2D>();
        kinematicOriginalRigidbodies = new List<Rigidbody2D>();

        foreach (GameObject kinematicObject in kinematicSpaceObjects)
        {
            kinematicOriginalRigidbodies.Add(kinematicObject.GetComponent<Rigidbody2D>());

            GameObject copy = Instantiate(kinematicObject, kinematicObject.transform.position, kinematicObject.transform.rotation);

            copy.transform.localScale = new Vector3(copy.transform.localScale.x * 100, copy.transform.localScale.y * 100);

            //copy.GetComponent<SpriteRenderer>().enabled = false;

            SceneManager.MoveGameObjectToScene(copy, simulatedScene);

            kinematicObjectsCopied.Add(copy.GetComponent<KinematicSpaceObject>());
            kinematicCopiedRigidbodies.Add(copy.GetComponent<Rigidbody2D>());
        }


        //Динамические объекты с построением траектории и динамичной гравитацией

        simulatedObjectCopiedScene = new List<TrajectoryRenderer>();
        simulatedCopiedRigidbodies = new List<Rigidbody2D>();

        foreach (TrajectoryRenderer simulatedObject in simulatedObjectsOriginalScene)
        {
            GameObject copy = Instantiate(simulatedObject.gameObject, simulatedObject.transform.position, simulatedObject.transform.rotation);

            copy.transform.localScale = new Vector3(copy.transform.localScale.x * 100, copy.transform.localScale.y * 100);

            Rigidbody2D copiedBody = copy.GetComponent<Rigidbody2D>();
            Rigidbody2D originalBody = simulatedObject.gameObject.GetComponent<Rigidbody2D>();

            //copiedBody.

            //copy.GetComponent<SpriteRenderer>().enabled = false;

            SceneManager.MoveGameObjectToScene(copy, simulatedScene);

            copiedBody.velocity = originalBody.velocity;
            copiedBody.angularVelocity = originalBody.angularVelocity;
            copiedBody.totalForce = originalBody.totalForce;

            simulatedObjectCopiedScene.Add(copy.GetComponent<TrajectoryRenderer>());
            simulatedCopiedRigidbodies.Add(copiedBody);
        }





        //Неподвижные объекты

      /*staticObjectsCopied = new List<GameObject>();
        foreach (GameObject staticObject in staticSpaceObjects)
        {
            GameObject copy = Instantiate(staticObject, staticObject.transform.position, staticObject.transform.rotation);

            copy.GetComponent<SpriteRenderer>().enabled = false;

            SceneManager.MoveGameObjectToScene(copy, simulatedScene);

            staticObjectsCopied.Add(copy);
        }*/
    }


    /**
     * <summary>
     * This method should be called every time a simulation is needed.
     * It copies all the simulated objects to the physical scene.
     * </summary>
    */
    private void PasteObjectsToSimulatedScene()
    {
        
    }

    private void BackToOriginal()
    {
        //simulated
        for(int i = 0; i < simulatedObjectsOriginalScene.Count; i++)
        {
            Rigidbody2D originalBody = simulatedOriginalRigidbodies[i];
            Rigidbody2D copiedBody = simulatedCopiedRigidbodies[i];

            copiedBody.position = originalBody.position;
            copiedBody.rotation = originalBody.rotation;

            copiedBody.velocity = originalBody.velocity;
            copiedBody.angularVelocity = originalBody.angularVelocity;
            copiedBody.totalForce = originalBody.totalForce;
        }

        //kinematic
        for (int i = 0; i < kinematicOriginalRigidbodies.Count; i++)
        {
            Rigidbody2D originalBody = kinematicOriginalRigidbodies[i];
            Rigidbody2D copiedBody = kinematicCopiedRigidbodies[i];

            copiedBody.position = originalBody.position;
            copiedBody.rotation = originalBody.rotation;
        }
    }

    private void Simulate()
    {
        if (simulatedScene.IsValid()) {

            SceneManager.SetActiveScene(simulatedScene);

            Physics2D.simulationMode = SimulationMode2D.Script;

            //PasteObjectsToSimulatedScene();

            List<SimulatedObject> objects = new List<SimulatedObject>();

            simulatedObjectCopiedScene.ForEach(obj => objects.Add(obj.GetComponent<SimulatedObject>()));

            simulatedObjectsOriginalScene.ForEach(obj => obj.Refresh());

            //Debug.Log("Simulation start!");

            for(int i = 0; i < simulationIterations; i++)
            {

                objects.ForEach(obj => obj.RecalculateForce());

                simulatedPhysicsScene.Simulate(Time.fixedDeltaTime);

                for (int j = 0; j < simulatedObjectCopiedScene.Count; j++)
                {
                    simulatedObjectsOriginalScene[j].AddPoint(simulatedCopiedRigidbodies[j].position);
                    //Debug.Log(simulatedObjectCopiedScene[j].transform.position);
                }

                kinematicObjectsCopied.ForEach(obj => obj.MoveObject());

                //triangle.transform.position = kinematicObjectsCopied[0].transform.position;
            }

            simulatedObjectsOriginalScene.ForEach(obj => obj.DrawLine());

            BackToOriginal();

            SceneManager.SetActiveScene(currentScene);

            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        }
    }

    private void OnDestroy()
    {
        /*for (int i = 0; i < simulatedObjectCopiedScene.Count; i++)
        {
            Destroy(simulatedObjectCopiedScene[i]);
        }

        for (int i = 0; i < kinematicObjectsCopied.Count; i++)
        {
            Destroy(kinematicObjectsCopied[i].gameObject);
        }

        for (int i = 0; i < staticObjectsCopied.Count; i++)
        {
            Destroy(staticObjectsCopied[i]);
        }

        simulatedObjectCopiedScene.Clear();
        simulatedObjectsOriginalScene.Clear();
        kinematicObjectsCopied.Clear();
        staticObjectsCopied.Clear();*/
    }
}
