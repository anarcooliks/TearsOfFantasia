using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    public Camera cam;
    public float speed = 1f;
    public Canvas playerCanvas;
    [SerializeField] Vector3 rotOffset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCanvas.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
		{
			Move();
		}
        
        if(!Input.GetMouseButton(1)){
            Rotate();
        } else
		{
			ShowSpellPanel(true);
		} 

        if(Input.GetMouseButtonUp(1)){
             ShowSpellPanel(false);
        }
    }

    private void Move(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
             float distBetweenMouseAndPlayer = Vector3.Distance(transform.position, hit.point);
             if(distBetweenMouseAndPlayer > 1.5f){
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime);
            }               
        }
    }

    private void Rotate(){
        Vector3 playerPos = cam.WorldToScreenPoint(transform.position);
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - playerPos.x;
        mousePos.y = mousePos.y - playerPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, cam.transform.rotation.z, 0) * Quaternion.Euler(new Vector3(0, -angle, 0)) * Quaternion.Euler(rotOffset);
    }

    private void ShowSpellPanel(bool visible){
        playerCanvas.gameObject.SetActive(visible);
    }
}
