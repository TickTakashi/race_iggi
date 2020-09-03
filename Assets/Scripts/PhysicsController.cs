using Cinemachine;
using Mirror;
using UnityEngine;

public class PhysicsController : NetworkBehaviour
{
	public static float WorldYLimit = -2f;

	public ConfigurableJoint Hips;
	public float Speed = 5f;
	public float TurningForce = 30f;
	public float JumpForce = 2f;
	public float GroundDistance = 0.2f;
	public float AirControlRatio = 0.3f;
	public float BackwardRatio = 0.5f;
	public LayerMask Ground; 
	public LayerMask Lava;
	public Transform GroundCheckOrigin;
	public Rigidbody Body;

	public Renderer[] ColoredComponents;

	[Tooltip("The transform that input is relative to, usually the 3rd person camera.")]
	public CinemachineFreeLook ForwardGuidePrefab;

	private CinemachineFreeLook _forwardGuide;
	private Vector3 _inputs = Vector3.zero;
	private Quaternion _cachedRotation;
	private Vector3 _cachedSpawnPosition;
	
	[SyncVar]
	public Color SyncColor;

	private bool isGrounded => Physics.CheckSphere(GroundCheckOrigin.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
	private bool isLava => Physics.CheckSphere(GroundCheckOrigin.position, GroundDistance, Lava, QueryTriggerInteraction.Ignore);
	private bool isOOB => transform.position.y < WorldYLimit || isLava;

	public override void OnStartClient() {
		base.OnStartClient();

		if (isLocalPlayer) {
			CinemachineFreeLook cfl = Instantiate(ForwardGuidePrefab);
			cfl.Follow = transform;
			cfl.LookAt = transform;

			_forwardGuide = cfl;
		}

		SetColor(SyncColor);
	}


	[Client]
	void Update() {
		if (isLocalPlayer) {
			_inputs = Vector3.zero;
			_inputs.x = Input.GetAxis("Horizontal");
			_inputs.z = Input.GetAxis("Vertical");

			if (Input.GetButtonDown("Jump")) {
				CmdJump();
			}

			if (isOOB || Input.GetButtonDown("Cancel")) {
				CmdRespawn();
			}
		}
	}

	[Client]
	void FixedUpdate() {
		if (isLocalPlayer) {
			Vector3 relativeInputs = Quaternion.Euler(0, _forwardGuide.State.CorrectedOrientation.eulerAngles.y, 0) * _inputs;

			if (relativeInputs.magnitude > 0.01f) {
				CmdMove(relativeInputs);
			}
		}
	}

	public override void OnStartServer() {
		base.OnStartServer();
		Body.SetKinematicWithChildren(false);
		_cachedRotation = Hips.transform.rotation;
		_cachedSpawnPosition = transform.position;
		SyncColor = Random.ColorHSV(0, 1f, 0.7f, 0.8f, 0.8f, 1.0f);
		SetColor(SyncColor);

	}

	void SetColor(Color color) {
		foreach (Renderer r in ColoredComponents) {
			r.material.color = color;
		}
	}

	[Command]
	void CmdRespawn() {
		// TODO: Respawn the player.
		transform.position = _cachedSpawnPosition;
		Body.velocity = Vector3.zero;
	}

	[Command]
	void CmdJump() {
		if (isGrounded) {
			Body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
		}
	}

	[Command]
	void CmdMove(Vector3 relativeInputs) {

		if (isGrounded) {
			// Step 1: Find our Current velocty vector V
			// Step 2: Calculate our Desired velocity vector based on input D, and Speed S.
			// Step 3: Find Vector X such that V + X = D * S
			// Step 4: Apply X.normalized * Turning Force

			Vector3 deltaV = relativeInputs * Speed - Body.velocity;
			Body.AddForce(deltaV.normalized * TurningForce * relativeInputs.magnitude, ForceMode.Force);
			Hips.SetTargetRotation(Quaternion.LookRotation(relativeInputs, Vector3.up), _cachedRotation);
		} else {

			// Step 1: Get the component of input in the direction of our current velocity.
			//	b.normalized * a . b / |b| = t
			// Step 2: Compute the perpendicular component by subtracting this from our input.
			//	a - t = P
			// Step 3: Compute the component of the resulting vector P that is in-line with the ground plane (to prevent counter-jump forces)
			//	Step 3a: Get the component of P in the Upward direction, U
			//		U = Up * P . Up / |Up|
			//	Step 3b: Compute the perpendicular component, G, by subtracting this from P. 
			//		G = P - U
			// Step 3: Apply force in this perpendicular direction.
			//	AddForce(p * modifiers)

			float inlineLength = (Vector3.Dot(relativeInputs, Body.velocity) / Body.velocity.magnitude);
			Vector3 inlineVector = Body.velocity.normalized * inlineLength;
			Vector3 perpendicularVector = relativeInputs - inlineVector;
			Vector3 upwardComponent = Vector3.up * Vector3.Dot(perpendicularVector, Vector3.up);
			Vector3 inGroundPerpendicularComponent = perpendicularVector - upwardComponent;
			Vector3 inGroundParallelComponent = inlineVector -  Vector3.up * Vector3.Dot(inlineVector, Vector3.up);
			
			if (inlineLength > 0) {
				inGroundParallelComponent = Vector3.zero;
			}

			Vector3 result = inGroundParallelComponent * BackwardRatio + inGroundPerpendicularComponent;
			if (result.magnitude > 0) {
				Body.AddForce(result * TurningForce * AirControlRatio, ForceMode.Force);
			}


			//Debug.DrawRay(transform.position, perpendicularVector, Color.red, 5f);
			//Debug.DrawRay(transform.position, Body.velocity, Color.cyan, 5f);
			//Debug.DrawRay(transform.position, inlineVector, Color.blue, 5f);
			//Debug.DrawRay(transform.position, relativeInputs, Color.green, 5f);
			//Debug.DrawRay(transform.position, upwardComponent, Color.yellow, 5f);
			//Debug.DrawRay(transform.position, inGroundPerpendicularComponent, Color.magenta, 5f);
			//Debug.DrawRay(transform.position, inGroundParallelComponent, Color.magenta, 5f);
			//Debug.DrawRay(transform.position, result, Color.yellow, 5f);
		}
	}


}