using UnityEngine;
using System.Collections;

public class DroneMovementScript : MonoBehaviour {

    Rigidbody ourDrone;

    void Awake(){
        ourDrone = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        MovemenetUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();

        ourDrone.AddRelativeForce(Vector3.up * upForce);
        ourDrone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, ourDrone.rotation.z)
        );
    }

    void MovemenetUpDown(){
        if(Input.GetKey(KeyCode.I)){
            upForce = 450;
        }
        else if(Input.GetKey(KeyCode.K)){
            upForce = -200;
        }
        else if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K)){
            upForce = 98.1f;
        }

    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float titltVelocityForward; //Not needed
    void MovementForward(){
            if(Input.GetAxis("Vertical") !=0){
                ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
                tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltAmountForward, 0.1f);
            }
        }
    
    private float wantedYRotation;
    private float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotaionYVelocity;
    void Rotation(){
        if(Input.GetKey(KeyCode.J)){
            wantedYRotation -= rotateAmountByKeys;
        }
        if(Input.GetKey(KeyCode.L)){
            wantedYRotation += rotateAmountByKeys;
        }

        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotaionYVelocity, 0.25f);
    }

    private Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues(){
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f{
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f){
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.DeltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f){
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 5.0f, Time.DeltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) <0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f){
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }
}
}