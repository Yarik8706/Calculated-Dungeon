using UnityEngine;

namespace Inputs
{
    public class InputControl : MonoBehaviour
    {
        [SerializeField] private GameObject mobileInputs;
        
        public float moveX;
        public bool jump;
        public bool shift;
        public bool shoot;
        public Vector2 shootVector;
        public bool nextLvl;
        public bool restartLvl;
        public bool sunVectorLight;
    
        public static InputControl Instance { get; private set; }

        private void Awake()
        {
            mobileInputs.SetActive(!Constants.isPC);

            Instance = this;
        }

        private void Update()
        {
            nextLvl = Input.GetMouseButtonDown(0) || Input.touchCount != 0;
            restartLvl = Input.GetMouseButtonDown(0) || Input.touchCount != 0;
            if(!Constants.isPC) return;
            sunVectorLight = Input.GetMouseButton(1);
            var clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0;
            shootVector = (clickPosition - PlayerControl.Instance.transform.position).normalized;
            shoot = Input.GetMouseButtonDown(0);
            moveX = Input.GetAxisRaw("Horizontal");
            jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
            shift = Input.GetKeyDown(KeyCode.LeftShift);
        }
    }
}