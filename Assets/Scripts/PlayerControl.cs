using Mirror;
using UnityEngine;

public class PlayerControl : NetworkBehaviour {

  //[SerializeField]
  Camera viewCamera;

  [SerializeField]
  private float movementSpeed = 5f;
  [SerializeField]
  private CharacterController controller = null;

  private Vector2 previousInput;

  private Controls controls;
  private Controls Controls{
      get{
          if(controls != null){
              return controls;
          }
          return controls = new Controls();
      }
  }

  public override void OnStartAuthority(){
    enabled = true;
    
    viewCamera = GameObject.FindGameObjectWithTag("cam_TP").GetComponent<Camera>();
    viewCamera.enabled = true; //GetComponentInChildren<Camera>().transform;
    
    Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
    Controls.Player.Move.canceled += ctx => ResetMovement();
  }

  [ClientCallback]
  private void OnEnable() {
    Controls.Enable();
  }
  [ClientCallback]
  private void OnDisable() {
      Controls.Disable();
  }

  // Update is called once per frame
  [ClientCallback]
  private void Update() {
    Move();
    Look();
  }

  [Client]
  private void SetMovement(Vector2 movement){
    previousInput = movement;
  }

  [Client]
  private void ResetMovement(){
    previousInput = Vector2.zero;
  }

  [Client]
  private void Look(){
    float height = 0.05f;
    //Look Input
    Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
    Plane groundPlane = new Plane(Vector3.up, Vector3.up * height);
    float rayDistance;

    if(groundPlane.Raycast(ray, out rayDistance)){
      Vector3 pointOfIntersection = ray.GetPoint(rayDistance);
      Debug.DrawLine(ray.origin, pointOfIntersection, Color.red);
      LookAtPoint(pointOfIntersection, 1f);
    }
  }

  [Client]
  private void LookAtPoint(Vector3 lookPoint, float mouseLookSpeed){
    Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
    transform.LookAt(heightCorrectedPoint);
  }

  [Client]
  private void Move(){
    Vector3 right = controller.transform.right;
    Vector3 forward = controller.transform.forward;
    right.y = 0f;
    forward.y = 0f;

    Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y; 

    controller.Move(movement * movementSpeed * Time.deltaTime);
  }
}