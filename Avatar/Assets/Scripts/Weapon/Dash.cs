using UnityEngine;
using PigeonCoopToolkit.Effects.Trails;

namespace Assets.Scripts.Actor
{
    class Dash : MonoBehaviour, IWeapon
    {

        // Configuraable parameteters
        public GameObject trailEffect;
        public GameObject targetingArrow;
        public bool slowTime = false;
        public float dashDistance = 7;
        public float dashTime = 0.5f;        
        
        private bool dashing;
        private float dashedDist;
        private float dashedTime;
        private Vector2 lastPosition;

        // Instance objects
        private Rigidbody2D actor;
        private IRotateable rotateComponenet;
        private TargetingArrow arrow;
        private GameObject trail;
        private float drag = 0.0f;

        void Start()
        {
            // Find the physics body that we're attached to (if any)
            actor = this.gameObject.GetComponent<Rigidbody2D>();
            rotateComponenet = this.gameObject.GetComponent<IRotateable>();

            dashing = false;
            dashedDist = 0.0f;
            dashedTime = 0.0f;

            createArrow();
            createTrail();
        }
        
        void Update()
        {
            
        }
        
        void FixedUpdate()
        {
            /* Dash will end after the dash distance is covered, or
               the expected amount of time that should be spent 
               dashing has elapsed (calculated based on dash speed and
               dash distance). 
              */
            if (dashing && actor != null)
            {
                Vector2 position = actor.position;
                float diff = (position - lastPosition).magnitude;
                lastPosition = position;

                dashedDist += diff;
                dashedTime += Time.fixedDeltaTime;

                if (dashedTime > dashTime || dashedDist > dashDistance)
                    endDash();
            }
        }
        

        public void Begin(Vector2 point)
        {
            if (dashing)
                return;

            Time.timeScale = 0.25f;
            
            arrow.Show();
        }

        public void Execute(Vector2 direction)
        {
            Time.timeScale = 1.0f;
            arrow.Hide();
            dash(direction);
        }

        public void Target(Vector2 direction)
        {
            arrow.SetDirection(direction);
        }

        public void Reset()
        {
            Time.timeScale = 1.0f;
            arrow.Hide();
        }

        private void endDash()
        {
            dashing = false;
            dashedDist = 0.0f;
            dashedTime = 0.0f;
            
            actor.velocity = Vector2.zero;
            actor.drag = drag;
            Invoke("disableTrail", 0.25f);
        }

        private void dash(Vector2 dashDirection)
        {
            if (actor == null || dashing || dashTime <= 0.0f)
                return;

            drag = actor.drag;
            dashing = true;
            dashedDist = 0.0f;
            dashedTime = 0.0f;
            
            enableTrail();

            float dashSpeed = (dashDistance / dashTime);
            lastPosition = actor.position;
            actor.velocity = (dashDirection.normalized * dashSpeed);
            actor.drag = 0.0f;

            if (rotateComponenet != null)
                rotateComponenet.Rotate(dashDirection);
        }

        private void createArrow()
        {
            GameObject arrowObject = GameObject.Instantiate(targetingArrow);
            arrowObject.transform.parent = this.transform;
            arrowObject.transform.position = Vector3.zero;
            arrowObject.transform.localScale = Vector3.one;
            this.arrow = arrowObject.AddComponent<TargetingArrow>();
            this.arrow.Hide();
        }

        private void createTrail()
        {
            trail = GameObject.Instantiate(trailEffect);
            trail.transform.parent = this.transform;
            trail.transform.localScale = Vector3.one;
            trail.transform.localPosition = Vector3.zero;
            disableTrail();
        }

        public void enableTrail()
        {
            trail.GetComponent<SmoothTrail>().Emit = true;
            trail.GetComponent<SmokeTrail>().Emit = true;
        }

        public void disableTrail()
        {
            if (!dashing)
            {
                trail.GetComponent<SmoothTrail>().Emit = false;
                trail.GetComponent<SmokeTrail>().Emit = false;
            }
        }

    }
}
