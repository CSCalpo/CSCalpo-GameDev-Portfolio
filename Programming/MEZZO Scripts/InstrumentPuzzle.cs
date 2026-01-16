public class InstrumentPuzzle : MonoBehaviour 
{ 
    private GameObject heldInstru = null; 
    private Rigidbody heldInstruRb; private 
    Camera playerCamera; 
    private Quaternion heldInstruOriginalRotation; 
  
  private void Start() 
  { 
    playerCamera = Camera.main; 
  } 

  private void Update() 
  { 
    if(Input.GetKeyDown(KeyCode.F)) 
      {
        if(heldInstru == null) 
          { 
              RaycastHit hit; 

              if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f)) 
                { 
                  if(hit.collider.CompareTag("Instrument")) 
                    { 
                      heldInstru = hit.collider.gameObject; 
                      heldInstruRb = heldInstru.GetComponent<Rigidbody>(); 
                      heldInstruRb.isKinematic = true; 
    
                      heldInstru.transform.SetParent(playerCamera.transform); 
                      heldInstru.transform.localPosition = new Vector3(0, -0.5f, 1); 
                    } 
                } 
          } 
    
    else 
    {
      if(!PlacingInstru()) 
        { 
          if(heldInstru != null) 
            { 
              heldInstru.transform.SetParent(null); 

              if(heldInstruRb != null) 
                { 
                  heldInstruRb.isKinematic = false; 
                } 
  
                  heldInstru = null; 
                  heldInstruRb = null; 

            }  
          } 
        } 
    } 
} 



  public bool PlacingInstru() 
  { 
    RaycastHit hit; 

    if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f)) 
      { 
      if(hit.collider.CompareTag("InstrumentSlot")) 
        {   
          InstrumentSlot slot = hit.collider.GetComponent<InstrumentSlot>(); 
          InstrumentID instrumentID = heldInstru.GetComponent<InstrumentID>();

      if(slot.InstrumentID == instrumentID.InstruID) 
        { 
          slot.PlaceInstru(heldInstru); heldInstru.transform.SetParent(slot.transform, true); 
    
          heldInstru.transform.position = slot.transform.position; 
          heldInstru.transform.rotation = slot.transform.rotation; 
    
          heldInstru.transform.localScale = Vector3.one; 
    
          heldInstru.GetComponent<Rigidbody>().isKinematic = true; 
    
          heldInstru = null; heldInstruRb = null; return true; 
      } 
    } 
} 

          return false; 

  } 
}




